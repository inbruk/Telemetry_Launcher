using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using tmtLauncher.ClientWPFApplication.View;

namespace tmtLauncher.ClientWPFApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void miRegisterNewUser_Click(object sender, RoutedEventArgs e)
        {
            var win = new RegisterNewUser();// { DataContext = new ViewModelWindow1(tb1.Text) };
            win.Show();
        }
    }
}
