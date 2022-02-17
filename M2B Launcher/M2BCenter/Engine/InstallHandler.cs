using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using M2BCenter.LauncherWebService;

namespace M2BCenter.Engine
{

    /// <summary>
    /// Закачка ппроизводится по одному файлу и по кускам, не представляю более неподходящего места, чтобы ее реализовать чем это, но попытаюсь.
    /// Делать параллельную закачку в несколько потоков уговора не было да и интерфейс на это не рассчитан (пока во всяком случае). Да и с точки зрения бизнеса 
    /// смысл закачивать в параллель 2-3 программы ? Или этот обработчик должен вызываться по завершении... Ладно попытаюсь совместить несовместимое.
    /// </summary>
    public class InstallHandler
    {
        private static InstallHandler _instance;

        public static InstallHandler Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new InstallHandler();
                return _instance;
            }
        }

        public InstallHandler()
        {
            _instance = this;
        }


        private List<AppsDataModel> _installingApps = new List<AppsDataModel>();

        /// <summary>
        /// Current installing apps
        /// </summary>
        public List<AppsDataModel> InstallingApps
        {
            get
            {
                return _installingApps;
            }
        }

        /// <summary>
        /// Method for start install some app to user machine
        /// </summary>
        /// <param name="app">app model</param>
        public void InstallApp(AppsDataModel app)
        {
            _installingApps.Add(app);
            //TODO: start install app
        }
    }
}
