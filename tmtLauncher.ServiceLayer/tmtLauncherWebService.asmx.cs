using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Web.Services.Protocols;

using ADTO = AuthEngine.DataTransferObjects;
using FDTO = FileDownloadingEngine.DataTransferObjects;
using SDTO = ShopEngine.DataModels;

using AuthEngine;
using ShopEngine;
using FileDownloadingEngine;
using SCE = SecurityCommunicationEngine;

namespace tmtLauncher.ServiceLayer
{
    /// <summary>
    /// Summary description for tmtLauncherWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class tmtLauncherWebService : System.Web.Services.WebService 
    {
        private const String _securityTokenSessionItemName = "currentSecurityToken";
        private const String _currentUserIdSessionItemName = "currentUserId";
        private SCE.Server _currSecServ = null;
        private SCE.Token LoadSecurityToken()
        {
            return HttpContext.Current.Session[_securityTokenSessionItemName] as SCE.Token;  
        }
        private void SaveSecurityToken(SCE.Token st)
        {
            HttpContext.Current.Session[_securityTokenSessionItemName] = st;
        }
        public Int32? GetCurrentUserId()
        {
            return HttpContext.Current.Session[_currentUserIdSessionItemName] as Int32?;
        }

        /// <summary>
        /// инициализирует SCE для текущего запроса к сервису и проверяет токен, если он был передан
        /// </summary>
        /// <param name="st"> если передан, то проверяет, иначе (если null) игнорирует</param>
        private void InitSecurityCommunicationEngine(SCE.Token st)
        {
            if( _currSecServ==null )
            {
                _currSecServ = new SCE.Server(SaveSecurityToken, LoadSecurityToken);
            }

            if ( st!=null && _currSecServ.CheckSecurityToken(st)!=true )
            {
                throw new Exception("User connection with such security token is not exists or connection timed out. Try to login once again.");
            }
        }

        /// <summary>
        /// Users registration, registration canceling when user with such login exists in DB 
        /// In other cases exceptions throwed. Auto login when registred not happened.
        /// SecurityCommunicationEngine is not required in this method
        /// </summary>
        /// <param name="userRegInfo"> all registration info (about user's company too) </param>
        /// <returns> true - registration complete succesfuly, false - shuch user exists in DB already </returns>
        [WebMethod (EnableSession = true)]
        public bool RegisterNewUser( ADTO.UserRegistrationInfo regInfo )
        {
            try
            {             
                Boolean successFlag = UsersTools.RegisterNewUser(regInfo);                
                return successFlag;
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri + " .RegisterNewUser()", ex);
            }
        }

        [WebMethod (EnableSession = true)]
        public SCE.Token LogOn(String login, String password, out ADTO.UserRegistrationInfo currUserInfo)
        {
            try
            {
                InitSecurityCommunicationEngine(null);

                SCE.Token currToken = null;

                AuthWorker currAW = new AuthWorker();
                Int32 currUserId = currAW.TryLoginUserID(login, password);

                if( currUserId!=-1 ) // user with login, password exists
                {
                    currUserInfo = UsersTools.GetRegistrationInfoByUserId(currUserId);
                    currToken = _currSecServ.OpenConnection();
                }
                else // user with login, password not found
                {
                    // close previous connectionin any way
                    _currSecServ.CloseConnection();
                    currUserInfo = null;
                    currToken = null;
                }
                return currToken;         
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri + " .LogOn()", ex);
            }
        }

        [WebMethod (EnableSession = true)]
        public void LogOff()
        {
            try
            {
                InitSecurityCommunicationEngine(null); // при закрывании соединения, не проверяем прокисло ли оно и существовало ли

                // close connection inside SCE
                _currSecServ.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri + " .LogOff()", ex);
            }
        }


        [WebMethod (EnableSession = true)]
        public String MakeSomeAction(SCE.Token st, int param1)
        {
            try
            {
                InitSecurityCommunicationEngine(st);
                return "normal result";
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri + " .MakeSomeAction()", ex);
            }
        }
        
        [WebMethod(EnableSession = true)]
        public ADTO.UserRegistrationInfo GetRegistrationInfoByUserId(SCE.Token st, Int32 userId)
        {
            try
            {
                InitSecurityCommunicationEngine(st);

                ADTO.UserRegistrationInfo currInfo = UsersTools.GetRegistrationInfoByUserId(userId);
                return currInfo;
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri + " .GetRegistrationInfoByUserId()", ex);
            }
        }

        [WebMethod(EnableSession = true)]
        public List<ShopEngine.DataModels.AppsDataModel> APPS_GetAppsList(SCE.Token st)
        {
            return new ShopEngine.UserApps().GetAllApps();
        }

        [WebMethod(EnableSession = true)]
        public List<ShopEngine.DataModels.AppsDataModel> APPS_GetUserApps(SCE.Token st)
        {
            return new ShopEngine.UserApps().GetUserApps(st.internalDataU);
        }

        [WebMethod(EnableSession = true)]
        public FDTO.FileInfo GetDownloadedFileInfo(SCE.Token st, String fileName)
        {
            try
            {
                InitSecurityCommunicationEngine(st);

                FDTO.FileInfo result = FileDownloader.GetFileInfo(fileName);
                return result;
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri + " .GetDownloadedFileInfo()", ex);
            }
        }

        [WebMethod(EnableSession = true)]
        public FDTO.ChunkInfo GetDownloadedFileChunk(SCE.Token st, String fileName, Int64 chunkNumber)
        {
            try
            {
                InitSecurityCommunicationEngine(st);

                FDTO.ChunkInfo result = FileDownloader.GetFileChunk(fileName, chunkNumber);
                return result;
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri + " .GetDownloadedFileChunk()", ex);
            }
        }

        [WebMethod(EnableSession = true)]
        public UInt32 GetDownloadedFileCRC32Hash(SCE.Token st, String fileName)
        {
            try
            {
                InitSecurityCommunicationEngine(st);

                UInt32 result = FileDownloader.GetRemoteFileCRC32Hash(fileName);
                return result;
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri + " .GetDownloadedFileCRC32Hash()", ex);
            }
        }

        /// <summary>
        /// получить общую информацию о программе (вообще в системе)
        /// </summary>
        [WebMethod(EnableSession = true)]
        public SDTO.AppsDataModel GetAppInfo(SCE.Token st, Int32 AppID)
        {
            try
            {
                InitSecurityCommunicationEngine(st);

                UserApps ua = new UserApps();
                SDTO.AppsDataModel result = ua.GetAppInfo(AppID);
                return result;
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri + " .GetAppInfo()", ex);
            }
        }

        [WebMethod(EnableSession = true)]
        public Boolean IsInstalledApplicationForUser(SCE.Token st, int userId, int appId)
        {
            try
            {
                InitSecurityCommunicationEngine(st);

                UserApps ua = new UserApps();
                Boolean resFlag = ua.IsInstalledApplicationForUser(userId, appId);
                return resFlag;
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri + " .IsInstalledApplicationForUser()", ex);
            }
        }
        
        /// <summary>
        /// помещает в БД информацию об устанавливаемом приложении (чисто установка, все проверки делать снаружи)
        /// </summary>
        [WebMethod(EnableSession = true)]        
        public void InstallApplicationForUser(SCE.Token st, int userId, int appId)
        {
            try
            {
                InitSecurityCommunicationEngine(st);

                UserApps ua = new UserApps();
                ua.InstallApplicationForUser(userId, appId);
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri + " .InstallApplicationForUser()", ex);
            }
        }

        [WebMethod(EnableSession = true)]
        public void UninstallApplicationForUser(SCE.Token st, int userId, int appId)
        {
            try
            {
                InitSecurityCommunicationEngine(st);

                UserApps ua = new UserApps();
                ua.UninstallApplicationForUser(userId, appId);
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri + " .UninstallApplicationForUser()", ex);
            }
        }

        [WebMethod(EnableSession = true)]
        /// <summary>
        /// вытаскивает ТОЛЬКО ПРИЛОЖЕНИЯ ТРЕБУЮЩИЕ УСТАНОВКИ ИЛИ ОБНОВЛЕНИЯ для данного пользователя (ДЛЯ ПОКАЗА НА ФОРМЕ МАГАЗИНА),
        /// также выставляет флаги отношений заданного пользователя с приложениями: надо ли устанавливать, надо ли менять, или все уже корректно установлено
        /// </summary>
        public List<SDTO.AppsDataModel> GetShopApplicationsInfoForCustomUser(SCE.Token st, int userId)
        {
            try
            {
                InitSecurityCommunicationEngine(st);

                UserApps ua = new UserApps();
                List<SDTO.AppsDataModel> resList = ua.GetShopApplicationsInfoForCustomUser(userId);
                return resList;
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri + " .GetShopApplicationsInfoForCustomUser()", ex);
            }
        }

        [WebMethod(EnableSession = true)]
        /// <summary>
        /// вытаскивает ТОЛЬКО УСТАНОВЛЕННЫЕ ПРИЛОЖЕНИЯ данного пользователя  (ДЛЯ ПОКАЗА НА ФОРМЕ МОИ ПРИЛОЖЕНИЯ), 
        /// также выставляет флаги отношений заданного пользователя с приложениями: надо ли устанавливать, надо ли менять, или все уже корректно установлено
        /// </summary>
        public List<SDTO.AppsDataModel> GetInstalledApplicationsInfoForCustomUser(SCE.Token st, int userId)
        {
            try
            {
                InitSecurityCommunicationEngine(st);

                UserApps ua = new UserApps();
                List<SDTO.AppsDataModel> resList = ua.GetInstalledApplicationsInfoForCustomUser(userId);
                return resList;
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri + " .GetInstalledApplicationsInfoForCustomUser()", ex);
            }
        }

        [WebMethod(EnableSession = true)]
        /// <summary>
        /// вытаскивает все приложения, 
        /// также выставляет флаги отношений для заданного пользователя с приложениями: надо ли устанавливать, надо ли менять, или все уже корректно установлено
        /// </summary>        
        public List<SDTO.AppsDataModel> GetAllApplicationsInfoForCustomUser(SCE.Token st, int userId)
        {
            try
            {
                InitSecurityCommunicationEngine(st);

                UserApps ua = new UserApps();
                List<SDTO.AppsDataModel> resList = ua.GetAllApplicationsInfoForCustomUser(userId);
                return resList;
            }
            catch (Exception ex)
            {
                throw new SoapException(ex.Message, SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri + " .GetAllApplicationsInfoForCustomUser()", ex);
            }
        }
    }
}
