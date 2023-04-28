using SteamAPI.Modules;
using System.Windows;
using System.Windows.Controls;

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
            Lbx_Result.Items.Clear();
            getChart.Search(Lbx_Result, Tbx_Name);
            //getChart.getChart(Lbx_Result, Tbx_Name);
        }
    }
}
