using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

using FirstFloor.ModernUI.Presentation;

namespace M2BCenter.Model
{
    public class SetupOrUpdateModel : NotifyPropertyChanged
    {
        private BitmapImage _bannerBitmap;
        public BitmapImage BannerBitmap
        {
            get { return _bannerBitmap; }
            set
            {
                _bannerBitmap = value;
                this.OnPropertyChanged("BannerBitmap");
            }
        }

        private string _applicationName;
        public string ApplicationName
        {
            get { return _applicationName; }
            set
            {
                _applicationName = value;
                this.OnPropertyChanged("ApplicationName");
            }
        }

        private string _categoryName;
        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                _categoryName = value;
                this.OnPropertyChanged("CategoryName");
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                this.OnPropertyChanged("Description");
            }
        }

        private string _version;
        public string Version
        {
            get { return _version; }
            set
            {
                _version = value;
                this.OnPropertyChanged("Version");
            }
        }

        private string _rating;
        public string Rating
        {
            get { return _rating; }
            set
            {
                _rating = value;
                this.OnPropertyChanged("Rating");
            }
        }

        private string _installationsCount;
        public string InstallationsCount
        {
            get { return _installationsCount; }
            set
            {
                _installationsCount = value;
                this.OnPropertyChanged("InstallationsCount");
            }
        }

        private double _installationProgress;
        public double InstallationProgress
        {
            get { return _installationProgress; }
            set
            {
                _installationProgress = value;
                this.OnPropertyChanged("InstallationProgress");
            }
        }
    }
}
