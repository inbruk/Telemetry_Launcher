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

using M2BCenter.Engine;
using WS = M2BCenter.LauncherWebService; // для логина, потом уйдет во вью модель логина

namespace M2BCenter.Pages
{
    /// <summary>
    /// Interaction logic for SetupOrUpdateView.xaml
    /// </summary>
    public partial class SetupOrUpdateView : UserControl
    {
        public SetupOrUpdateView()
        {
            InitializeComponent();
// ТЕСТ --------------------------------------------------------------------------------------
            Int32 currAppId = 1; // это должно приходить из магазина или моих программ
// ТЕСТ --------------------------------------------------------------------------------------            
            this.DataContext = new ViewModel.SetupOrUpdateViewModel(currAppId);
        }
    }
}
