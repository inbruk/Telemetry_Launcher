using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WS = M2BCenter.LauncherWebService;

namespace M2BCenter.Engine
{
    /// <summary>
    /// logon|logoff может происходить несколько раз, позже придумать более безопасный способ установки параметров
    /// </summary>
    public static class GlobalSettingsStorageHandler
    {
        private static GlobalSettingsStorage _globalSettingsStorage = null;
        public static GlobalSettingsStorage Get()
        {
            if (_globalSettingsStorage == null)
            {
                throw new Exception("Try to get global settings without previous logon.");
            }
            else
            {
                return _globalSettingsStorage;
            }
        }

        public static void UpdateInternalSettings(WS.Token currentServiceToken, WS.UserRegistrationInfo currUserRegInfo)
        {
            _globalSettingsStorage = new GlobalSettingsStorage(currentServiceToken, currUserRegInfo);
        }
    }
}
