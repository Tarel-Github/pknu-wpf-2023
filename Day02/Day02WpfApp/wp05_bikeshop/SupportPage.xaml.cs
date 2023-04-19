using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wp05_bikeshop.Logics;

namespace wp05_bikeshop
{
    /// <summary>
    /// SupportPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SupportPage : Page
    {
        Car myCar = null;

        public SupportPage()
        {
            InitializeComponent();
            InitCar();
        }

        private void InitCar()
        {
            // 일반적인 C#에서 클래스 객체 인스턴스 사용방법 동일
            Car myCar = new Car();
            myCar.Names = "아이오닉";
            myCar.Colors = Colors.White;
            myCar.Speed = 220;
        }

        //{Txt.Sample.Text= myCar.Speed.ToString();} // 전통적인 윈폼 방식, 하지만 이렇게 하면 WPF 최신기능을 못쓴다.
    }
}
