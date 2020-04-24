using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using wpf_demo_phonebook.ViewModels;

namespace wpf_demo_phonebook
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindow _wnd;

        public App()
        {
            _wnd = new MainWindow();
            _wnd.DataContext = new MainViewModel();
            _wnd.Show();
        }
    }
}
