﻿using MahApps.Metro.Controls;
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
using wp08_personalInfoApp.Models;
using wp08_personalInfoApp.ViewModels;  // 우리가 만든 뷰 모델 위치(폴더)

namespace wp08_personalInfoApp.Views
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainView : MetroWindow
    {
        public MainView()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel(); // 뷰 모델 바인딩 외 어떠한 이벤트 핸들러 코드도 없음
            // 핵심은 디자인과 코드를 분리하는 것에 있음
            // 어쩌면 이것도 벡엔드와 프론트엔드를 분리하는 것과 비슷한 늬양스 아닐까
        }
    }
}
