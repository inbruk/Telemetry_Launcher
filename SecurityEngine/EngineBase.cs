using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityCommunicationEngine
{
    /// <summary>
    /// делегат внешнего метода восстанавливающего токен безопасности
    /// внимание ! если хранилище пустое то нужно возвращать null
    /// </summary>
    public delegate Token RestoreSecurityToken();

    /// <summary>
    /// делегат внешнего метода сохраняющий токен безопасности
    /// внимание ! если хранилище нужно очистить, то в параметрах приходит null
    /// </summary>
    /// <param name="st"> токен безопасности </param>
    public delegate void StoreSecurityToken(Token st);

    /// <summary>
    /// часть механизма защищенной передачи данных используется как внутри сервера, так и внутри клиента
    /// </summary>
    public abstract class EngineBase
    {
        private StoreSecurityToken _currSecurityTokenSaver;
        private RestoreSecurityToken _currSecurityTokenRestorer;

        /// <summary>
        /// внутри скрыты механизмы внешнего сохранения, они используются как внутри сервера, так и внутри клиента
        /// </summary>
        //protected 
        public Token _currSecurityToken 
        {
            set 
            { 
                if( _currSecurityTokenSaver==null )
                {
                    throw new Exception("Can't save security token in SecurityEngine without properly setted SaveSecurityToken delegate.");
                }
                _currSecurityTokenSaver( value );
            }
            get 
            { 
                if( _currSecurityTokenRestorer==null )
                {
                    throw new Exception("Can't restore security token in SecurityEngine without properly setted RestoreSecurityToken delegate.");
                }
                return _currSecurityTokenRestorer();
            }
        }

        /// <summary>
        /// тут только настраивается внешнее хранилище текущего токена безопасности
        /// </summary>
        /// <param name="currSaver"></param>
        /// <param name="currRestorer"></param>
        public EngineBase(StoreSecurityToken currSaver, RestoreSecurityToken currRestorer)
        {
            _currSecurityTokenSaver    = currSaver;
            _currSecurityTokenRestorer = currRestorer;
        }
    }
}
