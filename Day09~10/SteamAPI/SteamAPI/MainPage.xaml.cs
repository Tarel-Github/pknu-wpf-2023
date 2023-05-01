using MySqlX.XDevAPI.Common;
using SteamAPI.Logics;
using SteamAPI.Modules;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SteamAPI
{
    /// <summary>
    /// MainPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainPage : Page
    {
        GetChart getChart = new GetChart();
        Favorite favorite = new Favorite();

        public string playerName = "";
        public string BattleTag = "";

        public MainPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            favorite.FavoriteView(Dgd_FavoriteList);
        }

        private void Btn_Search_Click(object sender, RoutedEventArgs e)
        {
            Img_profile.Source = null;
            Tbx_playedTime.Text = "";
            Tbx_userWinrate.Text = "";
            Lbl_Name.Content = "";
            Lbl_Tag.Content = "";

            Btn_FavoritePlus.IsEnabled = false;  // 우선 비활성화

            Lbx_Result.Items.Clear();
            getChart.Search(Lbx_Result, Tbx_Name, Tbx_Tag,
                Img_profile, Tbx_playedTime, Tbx_userWinrate, Lbl_Name, Lbl_Tag);


            Lbl_viewName.Visibility = Visibility.Visible;
            Lbl_viewTime.Visibility = Visibility.Visible;
            Lbl_viewWinrate.Visibility = Visibility.Visible;
            Btn_FavoritePlus.IsEnabled = true;  // 검색 성공시 활성화
        }


        #region<즐겨찾기 관련 코드>
        // 즐겨찾기추가
        private void Btn_FavoritePlus_Click(object sender, RoutedEventArgs e)
        {
            string name = Convert.ToString(Lbl_Name.Content);
            string tag = Convert.ToString(Lbl_Tag.Content);

            favorite.FavoritePlus(name, tag);

            Dgd_FavoriteList.ItemsSource = null;
            favorite.FavoriteView(Dgd_FavoriteList);    // 다시 로드
        }

        // 즐겨찾기 목록 선택
        private void Dgd_FavoriteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Dgd_FavoriteList.SelectedItem != null)
            {
                DataRowView dataRow = (DataRowView)Dgd_FavoriteList.SelectedItem;
                BattleTag = dataRow["BattleTag"].ToString();
                playerName = dataRow["Name"].ToString();
            }
        }

        // 즐겨찾기 적용 클릭
        private void Btn_FavoriteUse_Click(object sender, RoutedEventArgs e)
        {
            if (Dgd_FavoriteList.SelectedItem != null)
            {
                DataRowView dataRow = (DataRowView)Dgd_FavoriteList.SelectedItem;
                BattleTag = dataRow["BattleTag"].ToString();
                playerName = dataRow["Name"].ToString();
            }

            Tbx_Name.Text = playerName;
            Tbx_Tag.Text = BattleTag;
        }

        // 즐겨찾기 목록 더블클릭(적용과 동일)
        private void Dgd_FavoriteList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Dgd_FavoriteList.SelectedItem != null)
            {
                DataRowView dataRow = (DataRowView)Dgd_FavoriteList.SelectedItem;
                BattleTag = dataRow["BattleTag"].ToString();
                playerName = dataRow["Name"].ToString();
            }

            Tbx_Name.Text = playerName;
            Tbx_Tag.Text = BattleTag;
        }

        // 즐겨찾기 삭제
        private async void Btn_FavoriteDelete_Click(object sender, RoutedEventArgs e)
        {
            if (BattleTag == "")
            {
                await Commons.ShowMessageAsync("오류", $"데이터를 선택해주세요");
            }
            favorite.FavoriteDelete(BattleTag);
            Dgd_FavoriteList.ItemsSource = null;
            favorite.FavoriteView(Dgd_FavoriteList);
        }
        private void reload_Click(object sender, RoutedEventArgs e)
        {
            Dgd_FavoriteList.ItemsSource = null;
            favorite.FavoriteView(Dgd_FavoriteList);
        }
        #endregion

    }
}
