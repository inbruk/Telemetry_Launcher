using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Windows.Media.Imaging;

using FirstFloor.ModernUI.Presentation;

using WS = M2BCenter.LauncherWebService;
using M2BCenter.Engine;
using M2BCenter.Model;

namespace M2BCenter.ViewModel
{
    // внимание ! если программа уже устанавливалась и лежит на винте, и при этом не захвачена (не запущена), то она нормально устанавливается поверх
    public class SetupOrUpdateViewModel : NotifyPropertyChanged
    {
        private Boolean _isCancelDowlnloadEnabled;
        public Boolean IsCancelDowlnloadEnabled
        {
            get { return _isCancelDowlnloadEnabled; }
            set
            {
                _isCancelDowlnloadEnabled = value;
                this.OnPropertyChanged("IsCancelDowlnloadEnabled");
            }
        }

        private Boolean _isStartDowlnloadEnabled;
        public Boolean IsStartDowlnloadEnabled
        {
            get { return _isStartDowlnloadEnabled; }
            set
            {
                _isStartDowlnloadEnabled = value;
                this.OnPropertyChanged("IsStartDowlnloadEnabled");
            }
        }

        public SetupOrUpdateModel CurrAppInfo { set; get; }

        private WS.Token _currentServiceToken;
        private Int32 _currUserId;
        private Int32 _currAppId;
        private WS.AppsDataModel _currAppMdl;
        private FileDownloadingEngine _currFDE;
        private WS.tmtLauncherWebService _webServiceProxyHandler;

        public RelayCommand StartInstallationCommand { get; set; }
        public RelayCommand CancelInstallationCommand { get; set; }
        public SetupOrUpdateViewModel(Int32 currAppId)
        {
            _webServiceProxyHandler = WebServiceProxyHandler.Get();
            _currentServiceToken = GlobalSettingsStorageHandler.Get().CurrentServiceToken;
            _currUserId = GlobalSettingsStorageHandler.Get().CurrUserId;

            _currAppId = currAppId;

            // получим данные устанавливаемого приложения для заполнения формы
            _currAppMdl = _webServiceProxyHandler.GetAppInfo(_currentServiceToken, _currAppId);

            // заполним форму
            CurrAppInfo = new SetupOrUpdateModel();

            Uri currImgUri = new Uri(_currAppMdl.Banner250x250URL);
            BitmapImage currBI = new BitmapImage(currImgUri); // такое решение быстрее и проще чем тащить потом конвертер
            CurrAppInfo.BannerBitmap = currBI;

            CurrAppInfo.Description = _currAppMdl.Description;
            CurrAppInfo.CategoryName = _currAppMdl.CategoryName;
            CurrAppInfo.ApplicationName = _currAppMdl.Name;
            CurrAppInfo.Version = _currAppMdl.ActualVersion.ToString();
            CurrAppInfo.Rating = _currAppMdl.Rate.ToString();
            CurrAppInfo.InstallationsCount = _currAppMdl.counterInstall.ToString();
            CurrAppInfo.InstallationProgress = 0.0;

            IsStartDowlnloadEnabled = true;
            IsCancelDowlnloadEnabled = false;

            // повесим обработчики событий
            StartInstallationCommand = new RelayCommand(StartInstallation);
            CancelInstallationCommand = new RelayCommand(CancelInstallation);

            // создаем инфраструктуру для загрузки файлов
            _currFDE = new FileDownloadingEngine(_currentServiceToken, _currAppMdl);
        }

        public void StartInstallation(object parameter)
        {
            // перед новой установкой проверяем была ли уже установлена такая программа (возможно другой версии) и если была удаляем ее из БД
            Boolean IsApplicationHasBeenInstalled = _webServiceProxyHandler.IsInstalledApplicationForUser(_currentServiceToken, _currUserId, _currAppId);
            if (IsApplicationHasBeenInstalled)
            {
                _webServiceProxyHandler.UninstallApplicationForUser(_currentServiceToken, _currUserId, _currAppId);
            }

            IsStartDowlnloadEnabled  = false;
            IsCancelDowlnloadEnabled = true;

            _currFDE.partOfWorkCompleteHandler += new partOfWorkComplete(PartOfWorkCompleteHandler);
            _currFDE.allWorkCompleteHandler += new allWorkComplete(AllWorkCompleteHandler);

            _currFDE.Start();
        }
        
        public void CancelInstallation(object parameter)
        {
            _currFDE.Cancel();
            CompleteInstallation();
        }

        private void PartOfWorkCompleteHandler(object o, EventArgs e)
        {
            CurrAppInfo.InstallationProgress = (double)_currFDE.ProgressInPercents;
        }
        private void AllWorkCompleteHandler(object o, EventArgs e)
        {
            CompleteInstallation();
        }                    

        private void CompleteInstallation()
        {           
            IsStartDowlnloadEnabled = false;
            IsCancelDowlnloadEnabled = false;

            // сохраним информацию об установке данной программы для этого пользователя в БД сервера
            _webServiceProxyHandler.InstallApplicationForUser(_currentServiceToken, _currUserId, _currAppId);
        }
    }
}
