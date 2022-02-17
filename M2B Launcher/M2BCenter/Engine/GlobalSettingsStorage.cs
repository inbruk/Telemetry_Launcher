using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WS = M2BCenter.LauncherWebService;

namespace M2BCenter.Engine
{
    public class GlobalSettingsStorage
    {
        private WS.Token _currentServiceToken;
        public WS.Token CurrentServiceToken
        {
            get
            {
                return _currentServiceToken;
            }
        }

        private WS.UserRegistrationInfo _currUserRegInfo;
        
        public Int32 CurrUserId 
        { 
            get
            {
                return _currUserRegInfo.user.id;
            }
        }

        public Int32 CurrCompanyId
        {
            get
            {
                return _currUserRegInfo.usersCompany.id;
            }
        }

        public String CurrCompanyName
        {
            get
            {
                return _currUserRegInfo.usersCompany.Name;
            }
        }

        public GlobalSettingsStorage(WS.Token currentServiceToken, WS.UserRegistrationInfo currUserRegInfo)
        {
            _currentServiceToken = currentServiceToken;
            _currUserRegInfo = currUserRegInfo;
        }
    }
}
