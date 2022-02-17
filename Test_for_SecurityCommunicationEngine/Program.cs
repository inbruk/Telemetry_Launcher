using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using SCE = SecurityCommunicationEngine;
using WS = Test_tmtLauncher.ServiceLayer.LauncherWebService;

namespace Test_tmtLauncher.ServiceLayer
{
    public class tmtLauncherServiceLayer_Test
    {
        private static SCE.Token _currSecurityToken = null; 
        private static SCE.Token LoadSecurityToken()
        {
            return _currSecurityToken;
        }
        private static void SaveSecurityToken(SCE.Token st)  
        {
            _currSecurityToken = st;
        }
        public static SCE.Token ServerToken2ClientToken(WS.Token servToken)
        {
            return new SCE.Token() { internalData = servToken.internalData };
        }
        public static WS.Token ClientToken2ServerToken(SCE.Token clientToken)
        {
            return new WS.Token() { internalData = clientToken.internalData };
        }

        private static void ShowException(SoapException e)
        {
            String message = e.Message.Substring(0, e.Message.IndexOf("--->"));
            message = message.Substring(message.IndexOf(":") + 1);

            Console.Write(" cause exception in " + e.Actor + " => " + message);
        }
        private static void ShowComplete()
        {
            Console.Write(" Complete. Press any key to continue.");
            Console.WriteLine();
            Console.WriteLine();
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            WS.Token servToken = null;

            SCE.Client _currSecClient = new SCE.Client(SaveSecurityToken, LoadSecurityToken);
            WS.tmtLauncherWebService currServProxy = new WS.tmtLauncherWebService();
            // ATTENTION !!! THIS IS IMPORTANT FOR WEB SEVICE"S SESSIONS WORK PROPERLY !
            currServProxy.CookieContainer = new System.Net.CookieContainer(); // ИНАЧЕ СЕССИИ ИЗ ПРОГРАММЫ НЕ РАБОТАЮТ !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // THERE IS EXPLANATION WHY: http://msdn.microsoft.com/en-us/library/aa480509.aspx

            // check for register method -------------------------------------------------------------------------------------------------------------
            //WS.UserRegistrationInfo saveRegInfo = new WS.UserRegistrationInfo();

            //saveRegInfo.user = new WS.User();
            //saveRegInfo.usersCompany = new WS.Company();

            //saveRegInfo.user.Name = "Someuser"+ DateTime.Now;
            //saveRegInfo.user.Login = "login"+ DateTime.Now;
            //saveRegInfo.user.Password = "pwd";
            //saveRegInfo.user.Mail = "aa@aa.aa";
            //saveRegInfo.user.Phone = "555 5555";
            //saveRegInfo.user.GUID = Guid.Empty;
            //saveRegInfo.usersCompany.Name = "Some company" + DateTime.Now;
            //saveRegInfo.usersCompany.Adress = "Company Adress" + DateTime.Now;

            //try
            //{ 
            //    currServProxy.RegisterNewUser(saveRegInfo);
            //}
            //catch (SoapException se)
            //{
            //    ShowException(se);
            //}
            //ShowComplete();

            //try // not valid login --------------------------------------------------------------------------------------------------------------------
            //{
            //    Console.Write("Invalid Logon ");
            //    servToken = currServProxy.LogOn("alivub", "rqeiuerqui");
            //    Console.Write(" servToken==null ? {0} ", (servToken==null).ToString() );
            //}
            //catch (SoapException se)
            //{
            //    ShowException(se);
            //}
            //ShowComplete();

            try // valid login ---------------------------------------------------------------------------------------------------
            {
                Console.Write("Valid Logon ");
                Int32 currUserId; // не используется в тестах, но нужен в реальных приложениях
                servToken = currServProxy.LogOn("user333", "pwd333", out currUserId);
                Console.Write(" servToken={0} ", servToken.internalData.ToString() );
            }
            catch (SoapException se)
            {
                ShowException(se);
            }
            ShowComplete();

            // tell SCE about new connection ---------------------------------------------------------------------------------------------------
            _currSecClient.OpenConnection( ServerToken2ClientToken( servToken ) );

            //try // try to make request with random token ---------------------------------------------------------------------------------------------------
            //{
            //    Console.Write("MakeSomeAction make some action with random token ");
            //    String res = currServProxy.MakeSomeAction( new WS.Token(), 2);
            //    Console.Write("result ={0} ", res.ToString());
            //}
            //catch (SoapException se)
            //{
            //    ShowException(se);
            //}
            //ShowComplete();

            WS.AppsDataModel[] userApps;
            // check for GetAllApplicationsInfoForCustomUser (ВСЕ ПРИЛОЖЕНИЯ) ---------------------------------------------------------------------------------------------------
            try 
            {
                Console.WriteLine();
                Console.Write("check for GetAllApplicationsInfoForCustomUser => ");
                //userApps = currServProxy.GetAllApplicationsInfoForCustomUser( servToken, 1 );
                userApps = currServProxy.GetAllApplicationsInfoForCustomUser(servToken, 2);
                userApps = currServProxy.GetAllApplicationsInfoForCustomUser(servToken, 3);
                userApps = currServProxy.GetAllApplicationsInfoForCustomUser(servToken, 4);
                userApps = currServProxy.GetAllApplicationsInfoForCustomUser(servToken, 5);
            }
            catch (SoapException se)
            {
                ShowException(se);
            }
            ShowComplete();

            try // check for GetShopApplicationsInfoForCustomUser (ПРИЛОЖЕНИЯ ВИДИМЫЕ В МАГАЗИНЕ) -----------------------------------------------------------------------------------
            {
                Console.WriteLine();
                Console.Write("check for GetShopApplicationsInfoForCustomUser => ");
                userApps = currServProxy.GetShopApplicationsInfoForCustomUser(servToken, 1);
                userApps = currServProxy.GetShopApplicationsInfoForCustomUser(servToken, 2);
                userApps = currServProxy.GetShopApplicationsInfoForCustomUser(servToken, 3);
                userApps = currServProxy.GetShopApplicationsInfoForCustomUser(servToken, 4);
                userApps = currServProxy.GetShopApplicationsInfoForCustomUser(servToken, 5);
            }
            catch (SoapException se)
            {
                ShowException(se);
            }
            ShowComplete();

            try // check for GetInstalledApplicationsInfoForCustomUser (ПРИЛОЖЕНИЯ ВИДИМЫЕ НА ВКЛАДКЕ МОИ ПРИЛОЖЕНИЯ)----------------------------------------------------------------
            {
                Console.WriteLine();
                Console.Write("check for GetInstalledApplicationsInfoForCustomUser => ");
                userApps = currServProxy.GetInstalledApplicationsInfoForCustomUser(servToken, 1);
                userApps = currServProxy.GetInstalledApplicationsInfoForCustomUser(servToken, 2);
                userApps = currServProxy.GetInstalledApplicationsInfoForCustomUser(servToken, 3);
                userApps = currServProxy.GetInstalledApplicationsInfoForCustomUser(servToken, 4);
                userApps = currServProxy.GetInstalledApplicationsInfoForCustomUser(servToken, 5);
            }
            catch (SoapException se)
            {
                ShowException(se);
            }
            ShowComplete();

            //try // make request with valid token  ---------------------------------------------------------------------------------------------------
            //{
            //    Console.Write("MakeSomeAction make some action with valid token ");
            //    String res = currServProxy.MakeSomeAction
            //    (
            //        ClientToken2ServerToken(_currSecClient.GetCurrentSecurityToken()),
            //        2
            //    );
            //    Console.Write("result ={0} ", res);
            //}
            //catch (SoapException se)
            //{
            //    ShowException(se);
            //}
            //ShowComplete();

            //try // make log off ---------------------------------------------------------------------------------------------------
            //{
            //    currServProxy.LogOff();
            //    Console.Write("Valid LogOff ");
            //}
            //catch (SoapException se)
            //{
            //    ShowException(se);
            //}
            //ShowComplete();

            //// tell SCE about connection closing
            //_currSecClient.CloseConnection();

            //try // make request with closed token --------------------------------------------------------
            //{
            //    Console.Write("MakeSomeAction make some action with valid token ");
            //    String res = currServProxy.MakeSomeAction
            //    (
            //        servToken,
            //        2
            //    );
            //    Console.Write("result ={0} ", res.ToString());
            //}
            //catch (SoapException se)
            //{
            //    ShowException(se);
            //}
            //ShowComplete();
        }
    }
}
