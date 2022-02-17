using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShopEngine.DataModels;
using ShopEngine.DBAccess;

namespace ShopEngine
{
    public class UserApps
    {
        DBAccess.DB_Telemetry_ShopAppsEntities db = null;

        public UserApps()
        {
            db = new DBAccess.DB_Telemetry_ShopAppsEntities();
        }

        public Int32 GetCoutUserMessages(Int32 UserID)
        {
            Int32 r = (from d in db.Messages
                     where d.id_User == UserID
                     select d).Count();

            if (r != null)
                return r;
            else
                return 0;

        }

        public Int32 GetCoutNews()
        {
            Int32 r = (from d in db.News                       
                       select d).Count();

            if (r != null)
                return r;
            else
                return 0;
        }

        public bool IsUserratedApplication(Int32 AppID, Int32 UserID)
        {
            bool rez = (from d in db.tblUsersM2MUserApps
                       where d.id_App == AppID && d.id_User == UserID
                        select d.IsUserRated).SingleOrDefault();

            return rez;
        }

        /// <summary>
        /// Метод голосования пользователя за конкретное приложение
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="UserID"></param>
        /// <param name="UserRate"></param>
        public void RateApp(Int32 AppID, Int32 UserID, Int32 UserRate)
        {
            ShopEngine.DBAccess.tblAppsBOBApps app = (from d in db.tblAppsBOBApps
                                                     where d.id == AppID
                                                     select d).SingleOrDefault();

            if (app != null)
            {
                Int32 NewSUMRate = app.SumAllRate + UserRate;                
                Int32 CoutRated = app.CoutRated + 1;

                app.CoutRated = CoutRated;
                app.SumAllRate = NewSUMRate;

                app.rate = NewSUMRate / CoutRated;

                

                ShopEngine.DBAccess.tblUsersM2MUserApps m2m = (from d in db.tblUsersM2MUserApps
                                                              where d.id_App == AppID && d.id_User == UserID
                                                              select d).SingleOrDefault();
                if (m2m != null)
                {
                    m2m.IsUserRated = true;
                }

                db.SaveChanges();
            }
        }

        public List<AppsCategoryModel> GetCategoryList()
        {
            var rez = from d in db.tblAppsBOBCategories
                      select d;
            List<AppsCategoryModel> answer = new List<AppsCategoryModel>();
            foreach (var item in rez)
            {
                answer.Add(new AppsCategoryModel
                {
                    id = item.id,
                    Name = item.Name
                });
            }
            return answer;
        }

        public List<AppsDataModel> GetAppsByCategory(Int32 CatID)
        {
            return db.tblAppsBOBApps.Select(item => new AppsDataModel
            {
                AppID = item.id,
                AppShortcut = item.ImageShortcut,
                Description = item.Description,
                Banner250x250URL = item.Banner250x250URL,
                Banner100x100URL = item.Banner100x100URL,
                ScreenshotsURL = item.ScreenshotsURL,
                ScreenshotsCount = item.ScreenshotsCount,
                ActualFileName = item.ActualFileName,
                ActualVersion = item.ActualVersion,
                Icon = item.Icon,
                Name = item.AppName,
                counterInstall = (int)item.counterInstall,
                ID_category = (int)item.id_Category
            }).Where(it => it.ID_category == CatID).ToList();
        }

        public List<AppsDataModel> GetAllApps()
        {
            return db.tblAppsBOBApps.Select(item => new AppsDataModel
                                           {
                                               AppID = item.id,
                                               AppShortcut = item.ImageShortcut,
                                               Description = item.Description,
                                               Banner250x250URL = item.Banner250x250URL,
                                               Banner100x100URL = item.Banner100x100URL,
                                               ScreenshotsURL = item.ScreenshotsURL,
                                               ScreenshotsCount = item.ScreenshotsCount,
                                               ActualFileName = item.ActualFileName,
                                               ActualVersion = item.ActualVersion,
                                               Icon = item.Icon,
                                               Name = item.AppName,
                                               counterInstall = (int)item.counterInstall,
                                               ID_category = (int)item.id_Category
                                           }).ToList();
            /*
            IQueryable<DBAccess.tblAppsBOBApp> apps = from d in db.tblAppsBOBApps select d;
            List<AppsDataModel> list = new List<AppsDataModel>();
            foreach (var item in apps)
            {
                AppsDataModel model = new AppsDataModel
                    {
                        AppID = item.id,
                        AppShortcut = item.ImageShortcut,
                        Description = item.Description,
                        Image1 = item.ImagePreview1,
                        Image2 = item.ImagePreview2,
                        Image3 = item.ImagePreview3,
                        Icon = item.Icon,
                        Name = item.AppName
                    };

                list.Add(model);
            }
            return list;
            */
        }

        public List<AppsDataModel> GetUserApps(Guid guid)
        {
            string req = guid.ToString();
            IQueryable<DBAccess.Users> users = from d in db.Users where d.UID == req select d;
            if (!users.Any())
                return new List<AppsDataModel>();
            DBAccess.Users user = users.First();

            return user.tblUsersM2MUserApps.Select(item => new AppsDataModel
                                          {
                                              AppID = item.tblAppsBOBApps.id,
                                              AppShortcut = item.tblAppsBOBApps.ImageShortcut,
                                              Description = item.tblAppsBOBApps.Description,
                                              Banner250x250URL = item.tblAppsBOBApps.Banner250x250URL,
                                              Banner100x100URL = item.tblAppsBOBApps.Banner100x100URL,
                                              ScreenshotsURL = item.tblAppsBOBApps.ScreenshotsURL,
                                              ScreenshotsCount = item.tblAppsBOBApps.ScreenshotsCount,
                                              ActualFileName = item.tblAppsBOBApps.ActualFileName,
                                              ActualVersion = item.tblAppsBOBApps.ActualVersion,
                                              Icon = item.tblAppsBOBApps.Icon,
                                              Name = item.tblAppsBOBApps.AppName
                                          }).ToList();
            /*
            List<AppsDataModel> list = new List<AppsDataModel>();
            List<DBAccess.tblUsersM2MUserApp> tempAppsLinks = user.tblUsersM2MUserApps.ToList();
            foreach (var item in tempAppsLinks)
            {
                AppsDataModel model = new AppsDataModel
                                          {
                                              AppID = item.tblAppsBOBApp.id,
                                              AppShortcut = item.tblAppsBOBApp.ImageShortcut,
                                              Description = item.tblAppsBOBApp.Description,
                                              Image1 = item.tblAppsBOBApp.ImagePreview1,
                                              Image2 = item.tblAppsBOBApp.ImagePreview2,
                                              Image3 = item.tblAppsBOBApp.ImagePreview3,
                                              Icon = item.tblAppsBOBApp.Icon,
                                              Name = item.tblAppsBOBApp.AppName,
                                              PostBackURL = item.tblAppsBOBApp.AppURL
                                          };

                list.Add(model);
            }

            return list;
            */
        }

        public void AddAppToUser(Guid guid, int appID)
        {
            string g = guid.ToString();
            IQueryable<DBAccess.tblUsersM2MUserApps> testRequest = from d in db.tblUsersM2MUserApps where d.Users.UID == g && d.id_App == appID select d;
            if (testRequest.Any())
                return;
            
            IQueryable<DBAccess.Users> users = from d in db.Users where d.UID == g select d;
            if (!users.Any())
            {
                //добавляем этого юзера как впервые покупающего
                DBAccess.Users newUser = new DBAccess.Users {UID = g};
                db.Users.Add(newUser);
                db.SaveChanges();

                //добавляем к списку его приложений
                DBAccess.tblUsersM2MUserApps app = new DBAccess.tblUsersM2MUserApps
                                                      {id_App = appID, id_User = newUser.id};
                db.tblUsersM2MUserApps.Add(app);
                db.SaveChanges();

                //увеличиваем счетчик установок на 1
                IncrementCounterInstalls(appID);
            }
            else
            {
                DBAccess.tblUsersM2MUserApps app = new DBAccess.tblUsersM2MUserApps
                                                      {id_App = appID, id_User = users.First().id};
                db.tblUsersM2MUserApps.Add(app);
                db.SaveChanges();

                IncrementCounterInstalls(appID);
            }


        }

        /// <summary>
        /// Увеличение счетчика установок
        /// </summary>
        /// <param name="AppID"></param>
        private void IncrementCounterInstalls(Int32 AppID)
        { 
            DBAccess.tblAppsBOBApps app = (from d in db.tblAppsBOBApps
                                         where d.id == AppID
                                          select d).SingleOrDefault();
            if (app != null)
            {                
                app.counterInstall += 1;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Уменьшение счетчика установок
        /// </summary>
        /// <param name="AppID"></param>
        private void DecrimentCounterInstalls(Int32 AppID)
        {
            DBAccess.tblAppsBOBApps app = (from d in db.tblAppsBOBApps
                                          where d.id == AppID
                                          select d).SingleOrDefault();
            if (app != null)
            {
                app.counterInstall -= 1;
                db.SaveChanges();
            }
        }

        public void DeleteAppFromUser(Guid guid, int appID)
        {
            IQueryable<DBAccess.tblUsersM2MUserApps> apps =
                db.tblUsersM2MUserApps.Where(d => d.id_App == appID && d.Users.UID.Equals(guid.ToString()));
                    //from d in db.tblUsersM2MUserApps where d.id_App == appID && d.User.UID == guid.ToString() select d;
            if (!apps.Any())
                return;
            db.tblUsersM2MUserApps.Remove(apps.First());
            db.SaveChanges();

            //уменьшение счетчика установок приложения
            DecrimentCounterInstalls(appID);
        }

        public void AddNewApp(AppsDataModel model)
        {
            DBAccess.tblAppsBOBApps app = new DBAccess.tblAppsBOBApps
                {
                    AppName = model.Name,
                    Description = model.Description,
                    Icon = model.Icon,
                    ImageShortcut = model.AppShortcut,
                    Banner250x250URL = model.Banner250x250URL,
                    Banner100x100URL = model.Banner100x100URL,
                    ScreenshotsURL = model.ScreenshotsURL,
                    ScreenshotsCount = model.ScreenshotsCount,
                    ActualFileName = model.ActualFileName,
                    ActualVersion = model.ActualVersion,
                    counterInstall = 0
                };

            db.tblAppsBOBApps.Add(app);
            db.SaveChanges();
        }

        public void EditNewApp(AppsDataModel model)
        {

            IQueryable<DBAccess.tblAppsBOBApps> apps = db.tblAppsBOBApps.Where(d => d.id == model.AppID); 
                                                      //from d in db.tblAppsBOBApps where d.id == model.AppID select d;

            if (!apps.Any())
                return;

            DBAccess.tblAppsBOBApps app = apps.First();

            app.AppName = model.Name;
            app.ImageShortcut = model.AppShortcut;
            app.Description = model.Description;
            app.Icon = model.Icon;
            app.Banner250x250URL = model.Banner250x250URL;
            app.Banner100x100URL = model.Banner100x100URL;
            app.ScreenshotsURL = model.ScreenshotsURL;
            app.ScreenshotsCount = model.ScreenshotsCount;
            app.ActualFileName = model.ActualFileName;
            app.ActualVersion = model.ActualVersion;

            db.SaveChanges();
        }

        public AppsDataModel GetAppInfo(Int32 AppID)
        {
            AppsDataModel app = new AppsDataModel();

            var rez = (from d in db.tblAppsBOBApps
                       join c in db.tblAppsBOBCategories on d.id_Category equals c.id
                       where d.id == AppID
                       select new { 
                            d,
                            c.Name
                       }).SingleOrDefault();

            if (rez != null)
            {
                app.AppID = rez.d.id;
                app.AppShortcut = rez.d.ImageShortcut;
                app.Description = rez.d.Description;
                app.Icon = rez.d.Icon;
                app.Banner250x250URL = rez.d.Banner250x250URL;
                app.Banner100x100URL = rez.d.Banner100x100URL;
                app.ScreenshotsURL = rez.d.ScreenshotsURL;
                app.ScreenshotsCount = rez.d.ScreenshotsCount;
                app.ActualFileName = rez.d.ActualFileName;
                app.ActualVersion = rez.d.ActualVersion;
                app.Name = rez.d.AppName;
                app.counterInstall = (int)rez.d.counterInstall;
                app.CategoryName = rez.Name;
                app.Rate = rez.d.rate;
            }

            return app;
        }


        public void AddReview(string text, int app_id, string login)
        {
            Review rw = new Review();
            rw.id_App = app_id;
            rw.bylogin = login;
            rw.Date = DateTime.Now;
            rw.Message = text;

            db.Review.Add(rw);
            db.SaveChanges();
        }

        public List<ReviewModel> GetReviews(int app_id)
        {
            List<ReviewModel> answer = new List<ReviewModel>();

            IQueryable<Review> rw = from d in db.Review where d.id_App == app_id select d;
            foreach (var item in rw)
            {
                answer.Add(new ReviewModel
                {
                    idApp = item.id_App,
                    RevID = item.RevID,
                    bylogin = item.bylogin,
                    Date = item.Date,
                    Message = item.Message
                });
            }
            return answer;
        }

        private Users GetUserProxyWithChecking(int userId)
        {
            Users currUser = db.Users.Single(x => x.id == userId); // если не найден автоматически иксепшн сгенерится
            return currUser;
        }

        private tblAppsBOBApps GetApplicationProxyWithChecking(int appId)
        {
            tblAppsBOBApps currApp = db.tblAppsBOBApps.Single(x => x.id == appId); // если не найден автоматически иксепшн сгенерится
            return currApp;
        }

        public Boolean IsInstalledApplicationForUser(int userId, int appId)
        {
            GetUserProxyWithChecking(userId);
            GetApplicationProxyWithChecking(appId);
            Boolean res = db.tblUsersM2MUserApps.Any(x => (x.id_User == userId) && (x.id_App == appId));
            return res;
        }

        public void InstallApplicationForUser(int userId, int appId)
        {
            if( IsInstalledApplicationForUser( userId, appId )==true )
            {
                throw new Exception("Can't install application for user twice without uninstallation.");
                // если захочется другого поведения, то можно здесь uninstall вызывать
            }

            /*Users currUser = */ GetUserProxyWithChecking(userId);
            tblAppsBOBApps currApp = GetApplicationProxyWithChecking(appId);

            tblUsersM2MUserApps currUser2App = new tblUsersM2MUserApps()
            {
                id_App = appId,
                id_User = userId,
                CurrentVersion = currApp.ActualVersion,
                DateTime = DateTime.Now,
                CurrentRate = 0
            };

            db.tblUsersM2MUserApps.Add(currUser2App);
            db.SaveChanges();
        }

        public void UninstallApplicationForUser(int userId, int appId)
        {
            if (IsInstalledApplicationForUser(userId, appId) == false)
            {
                throw new Exception("Can't uninstall application that is not installed.");
                // если захочется другого поведения, то можно здесь uninstall вызывать
            }

            /*Users currUser = */ GetUserProxyWithChecking(userId);
            tblAppsBOBApps currApp = GetApplicationProxyWithChecking(appId);

            tblUsersM2MUserApps currUser2App = db.tblUsersM2MUserApps.Single(x => x.id_App == appId && x.id_User == userId);
            db.tblUsersM2MUserApps.Remove(currUser2App);
            db.SaveChanges();
        }

        /// <summary>
        /// вытаскивает ТОЛЬКО ПРИЛОЖЕНИЯ ТРЕБУЮЩИЕ УСТАНОВКИ ИЛИ ОБНОВЛЕНИЯ для данного пользователя (ДЛЯ ПОКАЗА НА ФОРМЕ МАГАЗИНА),
        /// также выставляет флаги отношений заданного пользователя с приложениями: надо ли устанавливать, надо ли менять, или все уже корректно установлено
        /// </summary>
        public List<AppsDataModel> GetShopApplicationsInfoForCustomUser(int userId)
        {
            List<AppsDataModel> allList = GetAllApplicationsInfoForCustomUser(userId);

            // теперь оставим только уже установленные
            List<AppsDataModel> resultList = new List<AppsDataModel>();
            foreach (AppsDataModel currItem in allList)
            {
                if (currItem.IsProperlyInstalled == false)
                {
                    resultList.Add(currItem);
                }
            }
            return resultList;
        }

        /// <summary>
        /// вытаскивает ТОЛЬКО УСТАНОВЛЕННЫЕ ПРИЛОЖЕНИЯ данного пользователя  (ДЛЯ ПОКАЗА НА ФОРМЕ МОИ ПРИЛОЖЕНИЯ), 
        /// также выставляет флаги отношений заданного пользователя с приложениями: надо ли устанавливать, надо ли менять, или все уже корректно установлено
        /// </summary>
        public List<AppsDataModel> GetInstalledApplicationsInfoForCustomUser(int userId)
        {
            List<AppsDataModel> allList = GetAllApplicationsInfoForCustomUser(userId);

            // теперь оставим только уже установленные
            List<AppsDataModel> resultList = new List<AppsDataModel>();
            foreach(AppsDataModel currItem in allList)
            {
                if( currItem.IsSetupRequired==false )
                {
                    resultList.Add(currItem);
                }
            }
            return resultList;
        }

        /// <summary>
        /// вытаскивает все приложения, 
        /// также выставляет флаги отношений для заданного пользователя с приложениями: надо ли устанавливать, надо ли менять, или все уже корректно установлено
        /// </summary>        
        public List<AppsDataModel> GetAllApplicationsInfoForCustomUser(int userId)
        {
            // сначала перенесем все приложения из магазина (по умолчанию выставим флаги всем на установку)
            List<AppsDataModel> result = 
            (
                from x in db.tblAppsBOBApps
                join c in db.tblAppsBOBCategories 
                on   x.id_Category equals c.id
                select  new AppsDataModel() 
                        { 
                            AppID = x.id,
                            Name = x.AppName,
                            Description = x.Description,
                            AppShortcut = "",
                            Banner250x250URL = x.Banner250x250URL,
                            Banner100x100URL = x.Banner100x100URL,
                            ScreenshotsURL = x.ScreenshotsURL,
                            ScreenshotsCount = x.ScreenshotsCount,
                            ActualFileName = x.ActualFileName,
                            Icon = x.Icon,
                            counterInstall = x.counterInstall != null ? (int) x.counterInstall : 0,
                            ID_category = x.id_Category == null ? (int) x.id_Category : 1,
                            CategoryName = (c.Name == null) ? "Телеметрия" : c.Name,
                            Rate = x.rate,
                            ActualVersion = x.ActualVersion,
                            InstalledVersion = 0.0,
                            IsSetupRequired = true,    // по умолчанию программа нуждается в установке
                            IsUpdateRequired = false,
                            CurrUserRate = 0
                        }
            ).ToList();
                        
            // теперь установим флаги в соответствие с наличием в списке установленных программ
            List<tblUsersM2MUserApps> installedAppsOfUser = db.tblUsersM2MUserApps.Where(x => x.id_User == userId).ToList();

            // внимание эдесь меняем данные только в памяти к это БД не относится (заполняем дополнительные поля)
            foreach(AppsDataModel currAppMdl in result)
            {
                int currAppid = currAppMdl.AppID;
                tblUsersM2MUserApps currProxyA2U = installedAppsOfUser.SingleOrDefault(x => x.id_App == currAppid);
                if( currProxyA2U!=null ) // значит это приложение установлено
                {
                    currAppMdl.IsSetupRequired = false;
                    if( currAppMdl.ActualVersion > currProxyA2U.CurrentVersion ) // программу надо обновлять
                    {
                        currAppMdl.IsSetupRequired  = false;
                        currAppMdl.IsUpdateRequired = true;
                    }
                    if ( currAppMdl.ActualVersion == currProxyA2U.CurrentVersion ) // программа нормально установлена
                    {
                        currAppMdl.IsSetupRequired  = false;
                        currAppMdl.IsUpdateRequired = false;
                    }
                    currAppMdl.InstalledVersion = currProxyA2U.CurrentVersion; // вирт. поле нужно обязательно заполнять
                    currAppMdl.CurrUserRate = currProxyA2U.CurrentRate;
                }
            }

            return result;
        }

        public void CreateCustomUser(int Id, string strUID)
        {
            //Users currUser = new Users()
            //{
            //    id = Id,
            //    UID = strUID,
            //    LastReadedMessageID = null,
            //    LastReadedNewsID = null
            //};

            db.Database.ExecuteSqlCommand(" EXEC dbo.CreateCustomUser @userID=\'" + Id.ToString() + "\', @strUID=\'" + strUID + "\' ");
        }
    }
}
