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

using FirstFloor.ModernUI.Windows;

namespace M2BCenter.Pages
{
    /// <summary>
    /// Interaction logic for PreLoader.xaml
    /// </summary>
    public partial class PreLoader : UserControl
    {
        ViewModel.PreLoaderViewModel model = new ViewModel.PreLoaderViewModel();

        public PreLoader()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.PreLoaderViewModel();
        }
    }
}
