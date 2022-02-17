using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

using FirstFloor.ModernUI.Presentation;

namespace M2BCenter.Model
{
    public class LoginModel : NotifyPropertyChanged
    {
        private string _login;
        public string LoginText
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                this.OnPropertyChanged("LoginText");
            }
        }

        private string _password;
        public string PasswordText
        {
            get { return _password; }
            set
            {
                _password = value;
                this.OnPropertyChanged("PasswordText");
            }
        }

        private Boolean _controlsEnabled;
        public Boolean ControlsEnabled
        {
            get { return _controlsEnabled; }
            set
            {
                _controlsEnabled = value;
                this.OnPropertyChanged("ControlsEnabled");
            }
        }
    }
}
