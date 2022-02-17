using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopEngine.DataModels
{
    public class AppsDataModel
    {
        public int AppID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string AppShortcut { get; set; }

        public string Banner250x250URL { get; set; }

        public string Banner100x100URL { get; set; }

        public string ScreenshotsURL { get; set; }

        public string ActualFileName { get; set; }

        public string Icon { get; set; }

        public int counterInstall { get; set; }

        public int ID_category { get; set; }

        public string CategoryName { get; set; }

        public decimal Rate { get; set; }

        public int ScreenshotsCount { get; set; }

        public double ActualVersion { get; set; }

        // эти поля заполняются только в GetAppInstallationsInfoForCustomUser, GetInstalledApplicationsInfoForCustomUser, GetShopApplicationsInfoForCustomUser

        public Boolean IsSetupRequired { get; set; }

        public Boolean IsUpdateRequired { get; set; }

        public Boolean IsProperlyInstalled
        {
            get
            {
                return (IsSetupRequired == false) && (IsUpdateRequired == false);
            }
        }

        public double InstalledVersion { get; set; }

        public decimal CurrUserRate { get; set; }

    }
}
