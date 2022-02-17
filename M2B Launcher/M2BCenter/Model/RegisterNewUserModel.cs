using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FirstFloor.ModernUI.Presentation;
using WS = M2BCenter.LauncherWebService;

namespace M2BCenter.Model
{
    public class RegisterNewUserModel : NotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                this.OnPropertyChanged("Name");
            }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                this.OnPropertyChanged("Login");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                this.OnPropertyChanged("Password");
            }
        }

        private string _mail;
        public string Mail
        {
            get { return _mail; }
            set
            {
                _mail = value;
                this.OnPropertyChanged("Mail");
            }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                this.OnPropertyChanged("Phone");
            }
        }
        
        private string _companyName;
        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                _companyName = value;
                this.OnPropertyChanged("CompanyName");
            }
        }

        private string _companyAdress;
        public string CompanyAdress
        {
            get { return _companyAdress; }
            set
            {
                _companyAdress = value;
                this.OnPropertyChanged("CompanyAdress");
            }
        }

        public WS.UserRegistrationInfo GetDataInSLFormat()
        {
            WS.UserRegistrationInfo saveRegInfo = new WS.UserRegistrationInfo();
            saveRegInfo.user = new WS.User();
            saveRegInfo.usersCompany = new WS.Company();

            saveRegInfo.user.Name = Name;
            saveRegInfo.user.Login = Login;
            saveRegInfo.user.Password = Password;
            saveRegInfo.user.Mail = Mail;
            saveRegInfo.user.Phone = Phone;
            saveRegInfo.usersCompany.Name = CompanyName;
            saveRegInfo.usersCompany.Adress = CompanyAdress;

            return saveRegInfo;
        }
    }
}
