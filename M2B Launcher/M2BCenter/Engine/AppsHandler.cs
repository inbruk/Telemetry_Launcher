using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using M2BCenter.LauncherWebService;

namespace M2BCenter.Engine
{
    class AppsHandler
    {
        private static AppsHandler _instance;

        public static AppsHandler Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AppsHandler();
                return _instance;
            }
        }

        public AppsHandler()
        {
            _instance = this;
        }



        private List<AppsDataModel> _apps = new List<AppsDataModel>();

        public List<AppsDataModel> AppsList
        {
            get { return _apps; }
        }





        public event EventHandler Refreshed;



        public void Update()
        {
            StartUpdateApps();
        }

        private void StartUpdateApps()
        {
            LauncherWebService.tmtLauncherWebService client = new tmtLauncherWebService();
            client.APPS_GetAppsListCompleted += client_APPS_GetAppsListCompleted;
            //TODO: Add stored token
            client.APPS_GetAppsListAsync(new Token());
        }

        void client_APPS_GetAppsListCompleted(object sender, APPS_GetAppsListCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                _apps.Clear();
                foreach (var item in e.Result)
                {
                    _apps.Add(item);
                }
                if (Refreshed != null)
                    Refreshed(this, new EventArgs());
            }
        }

        private void StartUpdateMyApps()
        {

        }
    }
}
