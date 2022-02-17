using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using M2BCenter.Engine;
using M2BCenter.LauncherWebService;

namespace M2BCenter.ViewModel
{
    class ShopViewModel : ViewModelBase
    {
        public ShopViewModel()
        {
            if (AppsList == null)
                AppsList = new ObservableCollection<AppsDataModel>();
            AppsHandler.Instance.Refreshed -= Instance_Refreshed;
            AppsHandler.Instance.Refreshed += Instance_Refreshed;
            if (AppsHandler.Instance.AppsList.Count() < 1)
                AppsHandler.Instance.Update();
            CallUpdateApps();
            _bw = new BackgroundWorker();
            _bw.DoWork += _bw_DoWork;
            _bw.RunWorkerCompleted += _bw_RunWorkerCompleted;
        }


        public ObservableCollection<AppsDataModel> AppsList { get; set; }

        private bool _displayedInfo = false;
        public bool DisplayedInfo
        {
            get { return _displayedInfo; }
            set
            {
                _displayedInfo = value;
                this.RaisePropertyChanged("DisplayedInfo");
            }
        }

        private AppsDataModel _selectedModel;
        public AppsDataModel SelectedModel
        {
            get { return _selectedModel; }
            set
            {
                _selectedModel = value;
                this.RaisePropertyChanged("SelectedModel");
                CheckSelectedItemAnimation();
            }
        }

        private BitmapImage _displayedIamge;
        public BitmapImage DisplayedImage
        {
            get { return _displayedIamge; }
            set
            {
                _displayedIamge = value;
                this.RaisePropertyChanged("DisplayedImage");
            }
        }






        void Instance_Refreshed(object sender, EventArgs e)
        {
            CallUpdateApps();
        }

        private void CallUpdateApps()
        {
            AppsList.Clear();
            foreach (var item in AppsHandler.Instance.AppsList)
            {
                AppsList.Add(item);
            }
        }

        private void CheckSelectedItemAnimation()
        {
            if (SelectedModel == null)
                DisplayedInfo = false;
            else
            {
                if (DisplayedInfo == false)
                    DisplayedInfo = true;
                else
                {
                    DisplayedInfo = false;
                    DisplayedInfo = true;
                }
                StartChangeImage();
            }
        }

        BackgroundWorker _bw;

        private void StartChangeImage()
        {
            if (SelectedModel == null)
                return;
            MakeImage();
            if (_bw.IsBusy)
                _bw.CancelAsync();
            _bw.RunWorkerAsync();
        }

        private int currBWID = 0;

        void _bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MakeImage();
            _bw.RunWorkerAsync();
        }

        private void MakeImage()
        {
            if (SelectedModel == null)
                return;
            string way = "";
            switch (currBWID)
            {
                case 0:
                    way = SelectedModel.Banner250x250URL;
                    break;
                case 1:
                    way = SelectedModel.Banner100x100URL;
                    break;
                case 2:
                    way = SelectedModel.ScreenshotsURL;
                    break;
            }
            if (way == null)
                return;
            currBWID++;
            if (currBWID >= 3)
                currBWID = 0;
            BitmapImage bi = new BitmapImage(new Uri(way, UriKind.RelativeOrAbsolute));
            DisplayedImage = bi;
        }

        void _bw_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
        }
    }
}
