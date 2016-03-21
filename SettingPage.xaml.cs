using ATP_Lab.Model;
using ATP_Lab.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace ATP_Lab
{
    /// <summary>
    /// Логика взаимодействия для SettingPage.xaml
    /// </summary>
    /// 
    
    public partial class SettingPage : Window
    {
        SettingViewModel view = new SettingViewModel();
        public SettingPage()
        {
            InitializeComponent();
            this.DataContext = view;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }



    }
}
