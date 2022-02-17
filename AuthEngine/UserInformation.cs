using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuthEngine.DBAccess;

namespace AuthEngine
{
    public class UserInformation
    {
        public List<string> GetUserName(List<string> GUIDs)
        {
            List<string> _answer = new List<string>();
            
            foreach (var item in GUIDs)
            {
                string name = GetUserName(item);
                _answer.Add(name);
            }

            return _answer;
        }

        public string GetUserName(string GUID)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                var user = db.tblUsersBOBUsers.SingleOrDefault(d => d.GUID.Equals(GUID));
                if (user == null)
                    return "Пользователь не существует";
                return user.Name;
            }
        }

        public string GetUserLogin(string GUID)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                var user = db.tblUsersBOBUsers.SingleOrDefault(d => d.GUID.Equals(GUID));
                if (user == null)
                    return "Пользователь не существует";
                return user.Login;
            }
        }

        public void SetUserLogin(string GUID, string login)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                var user = db.tblUsersBOBUsers.SingleOrDefault(d => d.GUID.Equals(GUID));
                if (user == null)
                    return;
                else
                {
                    user.Login = login;
                    db.SaveChanges();
                }
            }
        }

        public void SetUserName(string GUID, string name)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                var user = db.tblUsersBOBUsers.SingleOrDefault(d => d.GUID.Equals(GUID));
                if (user == null)
                    return;
                else
                {
                    user.Name = name;
                    db.SaveChanges();
                }
            }
        }
    }
}
