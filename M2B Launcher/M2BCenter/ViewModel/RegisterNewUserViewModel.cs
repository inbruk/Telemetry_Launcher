using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FirstFloor.ModernUI.Presentation;
using WS = M2BCenter.LauncherWebService;
using M2BCenter.Model;

namespace M2BCenter.ViewModel
{
    public class RegisterNewUserViewModel : NotifyPropertyChanged
    {
        public RegisterNewUserModel CurrRegInfo { set; get; }
        public RelayCommand RegisterCommand { get; set; }
        
        public RegisterNewUserViewModel()
        {
            CurrRegInfo = new RegisterNewUserModel();
            RegisterCommand = new RelayCommand(Register);
        }

        void Register(object parameter)
        {
            WS.UserRegistrationInfo saveRegInfo = CurrRegInfo.GetDataInSLFormat();
            WS.tmtLauncherWebService ws = new WS.tmtLauncherWebService();
            ws.RegisterNewUser(saveRegInfo);
        }
    }
}
