using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuthEngine.DBAccess;

namespace AuthEngine
{
    public class UserDatabaseChecker
    {
        public void CheckDB()
        {
            DBAccess.DB_Telemetry_UsersEntities db = new DBAccess.DB_Telemetry_UsersEntities();
            if (db.DatabaseExists() == false)
            {
                db.CreateDatabase();
            }
        }

        public tblUsersBOBUsers GetUserInfo(string uid)
        {
            try
            {
                using (DBAccess.DB_Telemetry_UsersEntities db = new DBAccess.DB_Telemetry_UsersEntities())
                {
                    return db.tblUsersBOBUsers.Where(o => uid == o.GUID).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
