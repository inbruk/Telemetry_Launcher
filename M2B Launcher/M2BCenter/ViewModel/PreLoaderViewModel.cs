using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

using FirstFloor.ModernUI.Presentation;

using WS = M2BCenter.LauncherWebService;
using M2BCenter.Engine;
using M2BCenter.Model;

namespace M2BCenter.ViewModel
{
    public class PreLoaderViewModel : NotifyPropertyChanged
    {
        public ObservableCollection<Model.PreLoadModel> LoadedData { get; set; }
        public LoginModel CurrLoginModel { set; get; }


        private Visibility _loadVisibility;
        public Visibility Loadvisibility
        {
            get { return _loadVisibility; }
            set
            {
                _loadVisibility = value;
                this.OnPropertyChanged("Loadvisibility");
            }
        }
        public RelayCommand LogonCommand { get; set; }
        public RelayCommand ForgetPasswordCommand { get; set; }
        public PreLoaderViewModel()
        {
            LogonCommand = new RelayCommand(Logon_CommandHandler);
            ForgetPasswordCommand = new RelayCommand(ForgetPassword_CommandHandler);

            CurrLoginModel = new LoginModel();
            CurrLoginModel.LoginText = "";
            CurrLoginModel.PasswordText = "";
            CurrLoginModel.ControlsEnabled = false;

            Loadvisibility = Visibility.Collapsed;

            if (LoadedData == null)
            {
                LoadedData = new ObservableCollection<Model.PreLoadModel>();
            }

            FillData();

            StepLoaded += PreLoaderViewModel_StepLoaded;
            Thread tr = new Thread(StartLoad);
            tr.Start();
        }

        private void FillData()
        {
            LoadedData.Add(new Model.PreLoadModel
            {
                Checked = false,
                Error = "",
                Name = "Проверка директорий"
            });
            LoadedData.Add(new Model.PreLoadModel
            {
                Checked = false,
                Error = "",
                Name = "Загрузка настроек"
            });
            LoadedData.Add(new Model.PreLoadModel
            {
                Checked = false,
                Error = "",
                Name = "Проверка обновлений приложений"
            });
        }

        private void StartLoad()
        {
            Loadvisibility = Visibility.Visible;

            Thread.Sleep(1000);
            try
            {
                new Engine.DirectoryChecker().CheckDirs();
                StepLoaded(0, "");
            }
            catch (Exception ex)
            {
                StepLoaded(0, ex.Message);
            }
            Thread.Sleep(500);
            try
            {
                Engine.SettingsLoader.Load();
                StepLoaded(1, "");
            }
            catch (Exception ex)
            {
                StepLoaded(1, ex.Message);
            }
            Thread.Sleep(500);
            StepLoaded(2, "");

            Loadvisibility = Visibility.Collapsed;
            CurrLoginModel.ControlsEnabled = true;
        }

        private delegate void stepLoaded(int stepID, string Error);
        private event stepLoaded StepLoaded;
        
        void PreLoaderViewModel_StepLoaded(int stepID, string Error)
        {            
            int i = 0;
            foreach (var item in LoadedData)
            {
                if (i == stepID)
                {
                    if (Error == "")
                        item.Checked = true;
                    else
                        item.Error = Error;

                    break;
                }
                i++;
            }
        }
        
        void Logon_CommandHandler(object obj)
        {
            PasswordBox pwBox = obj as PasswordBox;

            if (CurrLoginModel.LoginText == String.Empty || pwBox.Password == String.Empty)
            {
                return ; // валидаторы пока везде отсутствуют
            }
            else
            {
                WS.UserRegistrationInfo currRegInfo;
                WS.Token servToken = WebServiceProxyHandler.Get().LogOn(CurrLoginModel.LoginText, pwBox.Password, out currRegInfo);
                GlobalSettingsStorageHandler.UpdateInternalSettings(servToken, currRegInfo);                
            }
        }

        void ForgetPassword_CommandHandler(object parameter)
        {
            throw new Exception("Not supported yet.");
        }
    }
}
