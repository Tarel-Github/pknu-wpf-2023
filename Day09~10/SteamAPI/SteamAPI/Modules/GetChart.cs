using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;
using SteamAPI.Logics;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json.Linq;
using ControlzEx.Standard;
using Org.BouncyCastle.Ocsp;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Messaging;
using MySqlX.XDevAPI.Relational;
using System.Collections.Generic;
using SteamAPI.Models;
using System.Net.Http;

namespace SteamAPI.Modules
{
    public class GetChart
    {
        Converter converter = new Converter();

        public async void Search(ListBox target, TextBox input)
        {

            string ApiUrl = "https://ow-api.com/v1/stats/pc/kr/전원차단-31635/complete";
            string result = string.Empty;

            // WebRequest, WebResponse 객체
            WebRequest req = null;
            WebResponse res = null;
            StreamReader reader = null;

            try
            {
                req = WebRequest.Create(ApiUrl);
                res = await req.GetResponseAsync();
                reader = new StreamReader(res.GetResponseStream());
                result = reader.ReadToEnd();

                Debug.WriteLine(result);
            }
            catch (Exception ex)
            {
                await Commons.ShowMessageAsync("오류", $"OpenAPI 조회 오류 {ex.Message}");
                return;
            }

            var jsonResult = JObject.Parse(result);
            var status = Convert.ToInt32(jsonResult["status"]);

            Debug.WriteLine(jsonResult);
            Debug.WriteLine("==================aaaa");
            
            try
            {
                var PlayerName = jsonResult["name"];                            // 플레이어 이름
                var comp = jsonResult["competitiveStats"];                      // 경쟁전 전적 전체
                var comp_AllHero_Data = comp["topHeroes"];                      // 경쟁전 영웅 상세 전적

                // 모든 캐릭터 전적을 정리하기 위해 json을 배열로 변환
                string comp_AllHero_DataString = "{\"topHeroes\": " + $"{comp_AllHero_Data.ToString()}" + "}";
                JObject jo = JObject.Parse(comp_AllHero_DataString);        // JSON 문자열을 JObject로 파싱
                JArray heroArray = new JArray();
                foreach (JProperty prop in jo["topHeroes"])                 // 모든 프로퍼티를 배열로 변환하고 캐릭터 이름을 추가.
                {
                    string heroName = prop.Name;                // 이름 가져오기
                    JObject heroObject = (JObject)prop.Value;   // 객체를 가져오기
                    heroObject.Add("name", heroName);           // hero 객체에 이름을 추가.
                    heroArray.Add(heroObject);                  // hero 객체를 배열 추가.
                }


                Debug.WriteLine(heroArray[0]);
                Debug.WriteLine(heroArray[0]["timePlayed"]);

                var comp_Hero = new List<Hero>();
                foreach (var hero in heroArray)
                {
                    comp_Hero.Add(new Hero
                    {
                        name = Convert.ToString(hero["name"]),
                        timePlayed = Convert.ToString(hero["timePlayed"]),                  // 플레이 타임
                        gamesWon = Convert.ToInt32(hero["gamesWon"]),                       // 승리 횟수
                        weaponAccuracy = Convert.ToInt32(hero["weaponAccuracy"]),           // 무기 명중률
                        criticalHitAccuracy = Convert.ToInt32(hero["criticalHitAccuracy"]),      // 치명타 적중률
                        eliminationsPerLife = (float)Convert.ToDouble(hero["eliminationsPerLife "]),    // 킬뎃율
                        multiKillBest = Convert.ToInt32(hero["multiKillBest"]),             // 연속처치
                        objectiveKills = Convert.ToInt32(hero["objectiveKills"]),           // 물체파괴
                    });
                }

                
                Debug.WriteLine(heroArray[0]);
                foreach (var hero in comp_Hero)
                {
                    getChart(target, input,
                            hero.name, hero.timePlayed, hero.weaponAccuracy, hero.criticalHitAccuracy, hero.gamesWon);
                }



            }
            catch (Exception ex)
            {
                await Commons.ShowMessageAsync("오류", $"JSON 처리오류 {ex.Message}");
                return;
            }

        }

        // 캐릭터별 승률
        public void getChart(ListBox target, TextBox input,
                            string name, string timePlayed, int weaponAccuracy, int criticalHitAccuracy, int gamesWon)
        {
            string newName = converter.Lang(name);  // 영어를 한글로 변환
            string job = converter.Job(name);       // 영웅 이름을 바탕으로 직업 분류

            // 새로운 ListBoxItem 객체 생성
            ListBoxItem newItem = new ListBoxItem();
            newItem.Width = target.Width;
            newItem.Margin = new Thickness(5, 2, 5, 2);
            newItem.Padding = new Thickness(10);

            // Grid를 생성하고 속성 설정
            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(75), MinWidth = 75, MaxWidth = 75 }); // 캐릭터 사진
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });                // 라벨
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });                // 라벨
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });


            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());


            // 캐릭터 이름, 플탐, 승리횟수, 명중률, 헤드샷, 이미지(추가 예정), 직업
            TextBlock nameTB = new TextBlock();
            //TextBlock clasTB = new TextBlock();     // 캐릭터 클래스, 당장은 이름과 통합함  
            TextBlock timeTB = new TextBlock();
            TextBlock weapTB = new TextBlock();
            TextBlock headTB = new TextBlock();
            TextBlock winsTB = new TextBlock();

            Label timeLB = new Label();
            Label weapLB = new Label();
            Label headLB = new Label();
            Label winsLB = new Label();
            timeLB.Content = "플레이 타임";
            weapLB.Content = "명중률";
            headLB.Content = "헤드샷 명중률";
            winsLB.Content = "승리 횟수";



            nameTB.HorizontalAlignment = HorizontalAlignment.Left;
            //clasTB.HorizontalAlignment = HorizontalAlignment.Left;
            timeTB.HorizontalAlignment = HorizontalAlignment.Left;
            weapTB.HorizontalAlignment = HorizontalAlignment.Left;
            headTB.HorizontalAlignment = HorizontalAlignment.Left;
            winsTB.HorizontalAlignment = HorizontalAlignment.Left;


            nameTB.Text = newName+"  "+job;              
            timeTB.Text = timePlayed;          
            weapTB.Text = Convert.ToString(weaponAccuracy);         // 테스트를 위한 덧붙임 글
            headTB.Text = Convert.ToString(criticalHitAccuracy);
            winsTB.Text = Convert.ToString(gamesWon);

            nameTB.FontSize = 20;
            nameTB.FontWeight = FontWeights.ExtraBold;


            Grid.SetColumn(timeLB, 1);
            Grid.SetRow(timeLB, 1);

            Grid.SetColumn(winsLB, 1);
            Grid.SetRow(winsLB, 2);

            Grid.SetColumn(weapLB, 3);
            Grid.SetRow(weapLB, 1);

            Grid.SetColumn(headLB, 3);
            Grid.SetRow(headLB, 2);

            // 테이블 추가
            Grid.SetColumn(nameTB, 1);
            Grid.SetRow(nameTB, 0);
            Grid.SetColumnSpan(nameTB, 3);

            Grid.SetColumn(timeTB, 2);
            Grid.SetRow(timeTB, 1);

            Grid.SetColumn(winsTB, 2);
            Grid.SetRow(winsTB, 2);

            Grid.SetColumn(weapTB, 4);
            Grid.SetRow(weapTB, 1);

            Grid.SetColumn(headTB, 4);
            Grid.SetRow(headTB, 2);


            // Grid에 요소 추가
            grid.Children.Add(nameTB);
            grid.Children.Add(timeTB);
            grid.Children.Add(winsTB);
            grid.Children.Add(weapTB);
            grid.Children.Add(headTB);

            grid.Children.Add(timeLB);
            grid.Children.Add(winsLB);
            grid.Children.Add(weapLB);
            grid.Children.Add(headLB);







            // ListBoxItem에 Grid 추가
            newItem.Content = grid;
            var rand = new Random();
            nameTB.Background = new SolidColorBrush(Color.FromRgb((byte)(rand.Next(4) * 15 + 70), (byte)(rand.Next(4) * 15 + 70), (byte)(rand.Next(4) * 15 + 70)));

            switch (job)
            {   
                // 딜러는 빨강, 탱커는 파랑, 힐러는 초록으로 표시, 신캐는 그냥 검은색
                case "딜러":
                    newItem.Background = new SolidColorBrush(Color.FromRgb((byte)(rand.Next(4) * 15 + 70), (byte)(30), (byte)(30)));   
                    break;
                case "탱커":
                    newItem.Background = new SolidColorBrush(Color.FromRgb((byte)(30), (byte)(30), (byte)(rand.Next(4) * 15 + 70)));
                    break;
                case "힐러":
                    newItem.Background = new SolidColorBrush(Color.FromRgb((byte)(30), (byte)(rand.Next(4) * 15 + 70), (byte)(30)));
                    break;
                default:
                    newItem.Background = new SolidColorBrush(Color.FromRgb((byte)(30), (byte)(30), (byte)(30)));
                    break;
            }


            target.Items.Add(newItem);
            target.ScrollIntoView(target.Items[target.Items.Count - 1]); // 정렬


        }

        // 프로필 정보
        public void ProfileInfo()
        {

        }

    }
}
