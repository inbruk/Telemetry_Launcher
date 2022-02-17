using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace M2BCenter.Engine
{
    class LoginHandler
    {
        private static LoginHandler _instance;

        public static LoginHandler Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LoginHandler();
                return _instance;
            }
        }

        public LoginHandler()
        {
            _instance = this;
        }
    }
}
