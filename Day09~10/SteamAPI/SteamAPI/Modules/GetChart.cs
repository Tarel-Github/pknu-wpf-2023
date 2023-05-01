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
using System.Windows.Media.Imaging;

namespace SteamAPI.Modules
{
    public class GetChart
    {
        Converter converter = new Converter();

        public async void Search(ListBox target, TextBox input ,TextBox tag, Image profileimg, TextBlock time, TextBlock rate, Label Lbl_Name, Label Lbl_Tag)
        {

            string Tag = tag.Text;
            string Name = input.Text;

            Debug.WriteLine(Tag, Name);

            string ApiUrl = $"https://ow-api.com/v1/stats/pc/kr/{Name}-{Tag}/complete";       // 공개 유저
            //string ApiUrl = "https://ow-api.com/v1/stats/pc/kr/네잎클로바-31564  31635/complete";       // 비공개 유저

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
                await Commons.ShowMessageAsync("오류", $"OpenAPI 조회 오류. \n없는 유저입니다. \n{ex.Message}");
                return;
            }

            var jsonResult = JObject.Parse(result);
            var status = Convert.ToInt32(jsonResult["status"]);

            Debug.WriteLine(jsonResult);
            
            try
            {
                var PlayerName = jsonResult["name"];                            // 플레이어 이름
                var comp = jsonResult["competitiveStats"];                      // 경쟁전 전적 전체
                var comp_AllHero_Data = comp["careerStats"];                      // 경쟁전 영웅 상세 전적

                if (Convert.ToString(PlayerName) == "")
                {
                    await Commons.ShowMessageAsync("오류", "비공개 프로필입니다.");
                    return;
                }

                // 모든 캐릭터 전적을 정리하기 위해 json을 배열로 변환
                string comp_AllHero_DataString = "{\"careerStats\": " + $"{comp_AllHero_Data.ToString()}" + "}";
                JObject jo = JObject.Parse(comp_AllHero_DataString);        // JSON 문자열을 JObject로 파싱
                JArray heroArray = new JArray();
                foreach (JProperty prop in jo["careerStats"])                 // 모든 프로퍼티를 배열로 변환하고 캐릭터 이름을 추가.
                {
                    string heroName = prop.Name;                // 이름 가져오기
                    JObject heroObject = (JObject)prop.Value;   // 객체를 가져오기
                    heroObject.Add("name", heroName);           // hero 객체에 이름을 추가.
                    heroArray.Add(heroObject);                  // hero 객체를 배열 추가.
                }

                var comp_Hero = new List<Hero>();
                Debug.WriteLine("1111111111111111111");
                Debug.WriteLine(Convert.ToString(heroArray));

                foreach (var hero in heroArray)
                {
                    try
                    {
                        comp_Hero.Add(new Hero
                        {
                            name = Convert.ToString(hero["name"]),
                            timePlayed = Convert.ToString(hero["game"]["timePlayed"]),                   // 플레이 타임
                            winrate = Convert.ToString(hero["game"]["winPercentage"]),                   // 승률
                            avg_dmg = Convert.ToInt32(hero["average"]["allDamageDoneAvgPer10Min"]),      // 평딜
                            avg_heal = Convert.ToInt32(hero["average"]["healingDoneAvgPer10Min"]),       // 평힐
                            KPL = Convert.ToString(hero["average"]["eliminationsPerLife"]),              // 킬뎃율
                        });
                    }
                    catch { }
                }
                foreach (var hero in comp_Hero)
                {
                    getChart(target, input,
                            hero.name, hero.timePlayed, hero.avg_dmg, hero.avg_heal, hero.winrate, hero.KPL);
                }

                Debug.WriteLine("asdasdasdasdsad");
                string usertimePlayed = Convert.ToString(jsonResult["competitiveStats"]["careerStats"]["allHeroes"]["game"]["timePlayed"]);
                var userWin = comp_AllHero_Data["allHeroes"]["game"]["gamesWon"];
                var userlose = comp_AllHero_Data["allHeroes"]["game"]["gamesLost"];
                double userWinrate= Convert.ToDouble(userWin) / (Convert.ToDouble(userlose) + Convert.ToDouble(userWin));
                string img = Convert.ToString(jsonResult["icon"]);
                string playTime = Convert.ToString(comp_AllHero_Data["allHeroes"]["game"]["timePlayed"]);
                // 유저 이름, 유저 테그, 전체 플레이타임, 유저 승률, 플레이어 아이콘
                getUserInfo(profileimg, time, rate, Lbl_Name, Lbl_Tag, Convert.ToString(PlayerName), Tag, userWinrate, img, playTime);

                Debug.WriteLine("asdasdasdasdsad");
            }
            catch (Exception ex)
            {
                await Commons.ShowMessageAsync("오류", $"JSON 처리오류 {ex.Message}");
                return;
            }
        }
        // 캐릭터별 승률
        public void getChart(ListBox target, TextBox input,
                            string name, string timePlayed, int weaponAccuracy, int criticalHitAccuracy, string gamesWon, string KPL)
        {
            string newName = converter.Lang(name);  // 영어를 한글로 변환
            string job = converter.Job(name);       // 영웅 이름을 바탕으로 직업 분류
            string Img_Path = converter.Image(name);// 영웅 이미지 가져오기

            // 새로운 ListBoxItem 객체 생성
            ListBoxItem newItem = new ListBoxItem();
            newItem.Width = target.Width;
            newItem.Margin = new Thickness(5, 2, 5, 2);
            newItem.Padding = new Thickness(10);

            // Grid를 생성하고 속성 설정
            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(75), MinWidth = 75, MaxWidth = 75 }); // 캐릭터 사진
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });


            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());


            // 캐릭터 이름, 플탐, 승리횟수, 명중률, 헤드샷, 이미지, 직업
            TextBlock nameTB = new TextBlock();
            //TextBlock clasTB = new TextBlock();     // 캐릭터 클래스, 이름과 통합함  
            TextBlock timeTB = new TextBlock();
            TextBlock weapTB = new TextBlock();
            TextBlock headTB = new TextBlock();
            TextBlock winsTB = new TextBlock();
            TextBlock killTB = new TextBlock();
            Image HeroImage = new Image();
            BitmapImage bitmap = new BitmapImage(new Uri(Img_Path, UriKind.Relative));

            Label timeLB = new Label();
            Label weapLB = new Label();
            Label headLB = new Label();
            Label winsLB = new Label();
            Label killLB = new Label();

            timeLB.Content = "플레이 타임";
            weapLB.Content = "평균 딜";
            headLB.Content = "평균 힐";
            winsLB.Content = "승률";
            killLB.Content = "킬뎃";

            nameTB.HorizontalAlignment = HorizontalAlignment.Left;
            //clasTB.HorizontalAlignment = HorizontalAlignment.Left;
            timeTB.HorizontalAlignment = HorizontalAlignment.Left;
            weapTB.HorizontalAlignment = HorizontalAlignment.Left;
            headTB.HorizontalAlignment = HorizontalAlignment.Left;
            winsTB.HorizontalAlignment = HorizontalAlignment.Left;
            killTB.HorizontalAlignment = HorizontalAlignment.Left;

            nameTB.Text = newName+"  "+job;              
            timeTB.Text = timePlayed;          
            weapTB.Text = Convert.ToString(weaponAccuracy);         // 테스트를 위한 덧붙임 글
            headTB.Text = Convert.ToString(criticalHitAccuracy);
            winsTB.Text = Convert.ToString(gamesWon);
            killTB.Text = Convert.ToString(KPL);
            HeroImage.Source = bitmap;

            nameTB.FontSize = 20;
            nameTB.FontWeight = FontWeights.ExtraBold;

            // 라벨 추가
            Grid.SetColumn(timeLB, 1);
            Grid.SetRow(timeLB, 1);

            Grid.SetColumn(winsLB, 1);
            Grid.SetRow(winsLB, 2);

            Grid.SetColumn(weapLB, 3);
            Grid.SetRow(weapLB, 1);

            Grid.SetColumn(headLB, 3);
            Grid.SetRow(headLB, 2);

            Grid.SetColumn(killLB, 3);
            Grid.SetRow(killLB, 0);

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

            Grid.SetColumn(killTB, 4);
            Grid.SetRow(killTB, 0);

            Grid.SetColumn(HeroImage, 0);
            Grid.SetRow(HeroImage, 0);
            Grid.SetRowSpan(HeroImage, 3);

            // Grid에 요소 추가
            grid.Children.Add(HeroImage);
            grid.Children.Add(nameTB);
            grid.Children.Add(timeTB);
            grid.Children.Add(winsTB);
            grid.Children.Add(weapTB);
            grid.Children.Add(headTB);
            grid.Children.Add(killTB);

            grid.Children.Add(timeLB);
            grid.Children.Add(winsLB);
            grid.Children.Add(weapLB);
            grid.Children.Add(headLB);
            grid.Children.Add(killLB);

            // ListBoxItem에 Grid 추가
            newItem.Content = grid;
            var rand = new Random();
            nameTB.Background = new SolidColorBrush(Color.FromRgb((byte)(rand.Next(4) * 15 + 70), (byte)(rand.Next(4) * 15 + 70), (byte)(rand.Next(4) * 15 + 70)));

            switch (job)
            {   
                // 딜러는 빨강, 탱커는 파랑, 힐러는 초록으로 표시, 신캐는 그냥 검은색
                case "딜러":
                    newItem.Background = new SolidColorBrush(Color.FromRgb((byte)(rand.Next(2) * 10 + 90), (byte)(30), (byte)(30)));   
                    break;
                case "탱커":
                    newItem.Background = new SolidColorBrush(Color.FromRgb((byte)(30), (byte)(30), (byte)(rand.Next(2) * 10 + 90)));
                    break;
                case "힐러":
                    newItem.Background = new SolidColorBrush(Color.FromRgb((byte)(30), (byte)(rand.Next(2) * 10 + 90), (byte)(30)));
                    break;
                default:
                    newItem.Background = new SolidColorBrush(Color.FromRgb((byte)(30), (byte)(30), (byte)(30)));
                    break;
            }
            target.Items.Add(newItem);
            target.ScrollIntoView(target.Items[target.Items.Count - 1]); // 정렬
        }

        // 프로필 정보
        public void getUserInfo(Image Img_img, TextBlock Tbx_time, TextBlock Tbx_rate, Label Lbl_Name, Label Lbl_Tag,
            string name, string tag, double winrate, string img, string playTime)
        {
            double newWinrate = winrate * 100f;
            string newWinrate2 = $"{newWinrate.ToString("F3")}%";

            BitmapImage bitmap = new BitmapImage(new Uri(img, UriKind.Absolute));
            Img_img.Source = bitmap;
            Tbx_rate.Text = newWinrate2;
            Tbx_time.Text = Convert.ToString(playTime);
            Lbl_Name.Content = name;
            Lbl_Tag.Content = tag;
        }


    }
}
