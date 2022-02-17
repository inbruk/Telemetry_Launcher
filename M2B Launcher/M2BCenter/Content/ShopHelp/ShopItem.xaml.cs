using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace M2BCenter.Content.ShopHelp
{
    /// <summary>
    /// Interaction logic for ShopItem.xaml
    /// </summary>
    public partial class ShopItem : UserControl
    {
        public ShopItem()
        {
            InitializeComponent();
        }

        public LauncherWebService.AppsDataModel DisplayedApp
        {
            get { return (LauncherWebService.AppsDataModel)GetValue(DisplayedAppProperty); }
            set 
            { 
                SetValue(DisplayedAppProperty, value);
            }
        }

        private static DependencyProperty DisplayedAppProperty = DependencyProperty.Register("DisplayedApp", 
            typeof(LauncherWebService.AppsDataModel),
            typeof(ShopItem),
            new PropertyMetadata(new LauncherWebService.AppsDataModel(), OnDisplayedAppChanged));

        private static void OnDisplayedAppChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            LauncherWebService.AppsDataModel model = (LauncherWebService.AppsDataModel)e.NewValue;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DisplayedApp == null)
                return;
            else
                Engine.InstallHandler.Instance.InstallApp(DisplayedApp);
        }
            
    }
}
