using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WS = tmtLauncher.ClientWPFApplication.LauncherWebService;

namespace tmtLauncher.ClientWPFApplication.ViewModel
{
    public class RegisterNewUser : ViewModelBase
    {
        private WS.UserRegistrationInfo _currRegInfo;
        public  WS.UserRegistrationInfo CurrRegInfo 
        { 
            get
            {
                return _currRegInfo;
            }
            set
            {
                if( _currRegInfo!=value)
                {
                    _currRegInfo = value;
                    RaisePropertyChanged("CurrRegInfo");
                }
            }
        }

        public RegisterNewUser()
        {
            WS.UserRegistrationInfo _currRegInfo = new WS.UserRegistrationInfo();
            _currRegInfo.user = new WS.User();
            _currRegInfo.usersCompany = new WS.Company();


        }
    }
}
