using SteamAPI.Modules;
using System;
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
        
        public MainPage()
        {
            InitializeComponent();
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

            Btn_FavoritePlus.IsEnabled = true;  // 검색 성공시 활성화
        }

        private void Btn_FavoritePlus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_FavoriteView_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
