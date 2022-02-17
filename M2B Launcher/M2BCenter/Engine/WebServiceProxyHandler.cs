using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WS = M2BCenter.LauncherWebService;

namespace M2BCenter.Engine
{
    public static class WebServiceProxyHandler
    {
        private static WS.tmtLauncherWebService _currServProxy = null;
        public static WS.tmtLauncherWebService Get()
        {
            if (_currServProxy == null)
            {
                _currServProxy = new WS.tmtLauncherWebService();
                // ATTENTION !!! THIS IS IMPORTANT FOR WEB SEVICE"S SESSIONS WORK PROPERLY !
                _currServProxy.CookieContainer = new System.Net.CookieContainer(); // ИНАЧЕ СЕССИИ ИЗ ПРОГРАММЫ НЕ РАБОТАЮТ !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }
            return _currServProxy;
        }
    }
}
