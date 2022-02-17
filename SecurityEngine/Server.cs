using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityCommunicationEngine
{
    /// <summary>
    /// эта часть для использования на сервере
    /// </summary>
    public class Server : EngineBase
    {
        public Server(StoreSecurityToken currSaver, RestoreSecurityToken currRestorer)      
        : base( currSaver, currRestorer )      
        {
        }

        public Token OpenConnection()
        {
            Token st = new Token();
            _currSecurityToken = st;
            return st;
        }

        public void CloseConnection()
        {
            _currSecurityToken = null; 
        }

        public Boolean CheckSecurityToken(Token checkSt)
        {
            Token currSt = _currSecurityToken;

            if (currSt == null) return false;

            return currSt.Equals(checkSt);
        }
    }
}
