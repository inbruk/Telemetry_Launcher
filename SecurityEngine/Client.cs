using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityCommunicationEngine
{
    /// <summary>
    /// эта часть для использования на клиенте
    /// </summary>
    public class Client : EngineBase
    {
        public Client(StoreSecurityToken currSaver, RestoreSecurityToken currRestorer)      
            : base( currSaver, currRestorer )      
        {
        }

        public void OpenConnection( Token st)
        {
            _currSecurityToken = st;
        }

        public void CloseConnection()
        {
            _currSecurityToken = null; 
        }

        public Token GetCurrentSecurityToken()
        {
            Token result = _currSecurityToken;
            if( result==null )
            {
                throw new Exception("Can't return nonexistent security token.");
            }
            return result;
        }
    }
}
