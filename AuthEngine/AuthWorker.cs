using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using AuthEngine.DBAccess;

namespace AuthEngine
{

    public class AuthWorker
    {
        /// <summary>
        /// постоянный гуид пользователя
        /// </summary>
        private Guid guid;

        /// <summary>
        /// email пользователя
        /// </summary>
        private string email;

        public string UserEmail
        {
            get { return email; }
        }

        /// <summary>
        /// Временный пароль пользователя
        /// </summary>
        private string tempHash = String.Empty;

        /// <summary>
        /// постоянный гуид пользователя
        /// </summary>
        public Guid UserGuid
        {
            get { return guid; }
        }

        /// <summary>
        /// Временный пароль пользователя
        /// </summary>
        public string TempHash
        {
            get { return tempHash; }
        }

        public string Login { get; set; }

        public void ChangeUserSettings(ChangeInfo change)
        {
            switch (change.Change)
            {
                case ChangeInfo.ChangeType.EMail:
                    ChangeUserEmail(change.UserLogin, change.NewValue);
                    break;
                case ChangeInfo.ChangeType.Phone:
                    ChangeUserPhone(change.UserLogin, change.NewValue);
                    break;
                case ChangeInfo.ChangeType.Password:
                    ChangeUserPassword(change.UserLogin, change.NewValue);
                    break;
            }
        }

        private void ChangeUserPassword(string login, string newPassword)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                SecurityWorker sw = new SecurityWorker();
                string encrypted = sw.Encrypt(newPassword);

                var user = db.tblUsersBOBUsers.SingleOrDefault(d => d.Login.Equals(login));
                if (user == null)
                    return;
                user.Password1 = encrypted;
                db.SaveChanges();
            }
        }

        public void ChangePassword(string UID, string newPassword)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                SecurityWorker sw = new SecurityWorker();
                string encrypted = sw.Encrypt(newPassword);

                var user = db.tblUsersBOBUsers.SingleOrDefault(d => d.GUID.Equals(UID));
                if (user == null)
                    return;
                user.Password1 = encrypted;
                db.SaveChanges();
            }
        }

        private void ChangeUserEmail(string login, string newEmail)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                var user = db.tblUsersBOBUsers.SingleOrDefault(d => d.Login.Equals(login));
                if (user == null)
                    return;

                user.Mail = newEmail;
                db.SaveChanges();
            }
        }

        private void ChangeUserPhone(string login, string newPhone)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                var user = db.tblUsersBOBUsers.SingleOrDefault(d => d.Login.Equals(login));
                if (user == null)
                    return;
                user.Phone = newPhone;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// авторизация пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool TryLoginUser(string login, string password)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                var user = db.tblUsersBOBUsers.FirstOrDefault(d => d.Login.Equals(login));
                if (user == null)
                    return false;

                SecurityWorker sw = new SecurityWorker();
                string encrypted = sw.Encrypt(password);
                if (user.Password1 == encrypted && user.isSubmit)
                {
                    //Если пользователь авторизовался верно - считываем его параметры
                    GenerateNewUserHash(password, db, user);
                    Login = login;
                    email = user.Mail;
                    if (user.tblCompanyBOBCompany.CompanyUID == string.Empty)
                        user.tblCompanyBOBCompany.CompanyUID = Guid.NewGuid().ToString();
                    db.SaveChanges();
                    return true;
                }
                return false;
            }

        }

        /// <summary>
        /// Авторизация пользователя без изменения подписи
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool TryLoginUserForASP(string login, string password)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                var user = db.tblUsersBOBUsers.SingleOrDefault(d => d.Login.Equals(login));
                if (user == null)
                    return false;

                SecurityWorker sw = new SecurityWorker();
                string encrypted = sw.Encrypt(password);
                if (user.Password1 == encrypted && user.isSubmit)
                {

                    //Парсим даные постоянного гуида пользователя
                    Guid.TryParse(user.GUID, out guid);
                    tempHash = user.Password;
                    Login = login;
                    email = user.Mail;
                    if (user.tblCompanyBOBCompany.CompanyUID == string.Empty)
                        user.tblCompanyBOBCompany.CompanyUID = Guid.NewGuid().ToString();
                    db.SaveChanges();
                    return true;
                }
                return false;
            }

        }

        /// <summary>
        /// авторизация пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int TryLoginUserID(string login, string password)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                var user = db.tblUsersBOBUsers.SingleOrDefault(d => d.Login.Equals(login));
                if (user == null)
                    return -1;

                SecurityWorker sw = new SecurityWorker();
                string encrypted = sw.Encrypt(password);
                if (user.Password1 == encrypted && user.isSubmit)
                {
                    //Если пользователь авторизовался верно - считываем его параметры
                    GenerateNewUserHash(password, db, user);
                    Login = login;
                    email = user.Mail;
                    if (user.tblCompanyBOBCompany.CompanyUID == string.Empty)
                        user.tblCompanyBOBCompany.CompanyUID = Guid.NewGuid().ToString();
                    db.SaveChanges();
                    return user.id;
                }
                return -1;
            }

        }


        public bool CheckUserPassword(string login, string password)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                var user = db.tblUsersBOBUsers.SingleOrDefault(d => d.Login.Equals(login));
                if (user == null)
                    return false;

                SecurityWorker sw = new SecurityWorker();
                string encrypted = sw.Encrypt(password);
                return user.Password1.Equals(encrypted);
            }
        }

        private void GenerateNewUserHash(string password, DB_Telemetry_UsersEntities db, tblUsersBOBUsers user)
        {
            //Парсим даные постоянного гуида пользователя
            Guid.TryParse(user.GUID, out guid);

            Random rnd = new Random();
            int vol = rnd.Next(1000, 1000000);
            vol = vol * vol;
            tempHash = vol.ToString();

            user.Password = tempHash;
            db.SaveChanges();
        }

        public bool CheckUserHash(string login, string hash)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                var user = db.tblUsersBOBUsers.SingleOrDefault(d => d.Login.Equals(login));
                if (user == null)
                    return false;

                string userHash = user.Password;
                if (hash != userHash)
                    return false;

                Login = login;
                if (user.tblCompanyBOBCompany.CompanyUID == string.Empty)
                    user.tblCompanyBOBCompany.CompanyUID = Guid.NewGuid().ToString();
                db.SaveChanges();
                Guid.TryParse(user.GUID, out guid);
                return true;
            }
        }

        public int CheckUserHashID(string login, string hash)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                var user = db.tblUsersBOBUsers.SingleOrDefault(d => d.Login.Equals(login));
                if (user == null)
                    return -1;

                string userHash = user.Password;
                if (hash != userHash)
                    return -1;

                Login = login;
                Guid.TryParse(user.GUID, out guid);
                return user.id;
            }
        }

        public AuthInfo CheckUserHashAI(string login, string hash)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                var user = db.tblUsersBOBUsers.SingleOrDefault(d => d.Login.Equals(login));
                if (user == null)
                    return null;

                string userHash = user.Password;
                if (hash != userHash)
                    return null;

                Login = login;
                Guid.TryParse(user.GUID, out guid);
                AuthInfo inf = new AuthInfo();
                inf.id = user.id;
                inf.Login = user.Login;
                if (user.isCompanyAdmin == null)
                {
                    user.isCompanyAdmin = false;
                    db.SaveChanges();
                    inf.isCompanyAdmin = false;
                }
                else
                    inf.isCompanyAdmin = (bool)user.isCompanyAdmin;

                if (user.tblCompanyBOBCompany.CompanyUID == null)
                {
                    user.tblCompanyBOBCompany.CompanyUID = Guid.NewGuid().ToString();
                    inf.CompanyUID = user.tblCompanyBOBCompany.CompanyUID;
                    db.SaveChanges();
                }
                else
                    inf.CompanyUID = user.tblCompanyBOBCompany.CompanyUID;

                inf.CompanyName = user.tblCompanyBOBCompany.Name;
                return inf;

            }
        }

        public Boolean CheckUserExists(string login)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
                return db.tblUsersBOBUsers.Any(u => u.Login.Equals(login));
        }

        public Boolean CheckEMailExists(string email)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
                return db.tblUsersBOBUsers.Any(u => u.Mail.Equals(email));
        }

        public Int32 GetUserID(string login)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                var user = db.tblUsersBOBUsers.SingleOrDefault(d => d.Login.Equals(login));
                if (user == null)
                    return -1;

                return user.id > 0 ? user.id : -1;
            }
        }

        public Int32 GetUserID(Guid uGuid)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                string req = uGuid.ToString();
                var user = db.tblUsersBOBUsers.SingleOrDefault(d => d.GUID.Equals(req));
                if (user == null)
                    return -1;

                return user.id > 0 ? user.id : -1;
            }
        }

        public string GetUserNameByUid(string uid)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                var user = db.tblUsersBOBUsers.SingleOrDefault(d => d.GUID.Equals(uid));
                return user == null ? String.Empty : user.Name;
            }
        }

        public string GetUserName(string login)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                Login = login;
                var user = db.tblUsersBOBUsers.SingleOrDefault(d => d.Login.Equals(login));
                return user == null ? String.Empty : user.Name;
            }
        }

        public void CreateUser(string name, string login, string password, string mail, string company)
        {
            PlaceUserToDB(name, login, password, mail, String.Empty, String.Empty, company);
            Login = login;
        }

        public void CreateUser(string name, string login, string password, string mail, string phone, string comment, string company)
        {
            PlaceUserToDB(name, login, password, mail, phone, comment, company);
        }

        public void CreateUser(string name, string login, string password, string companyUID)
        {
            DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities();
            DBAccess.tblCompanyBOBCompany company = (from d in db.tblCompanyBOBCompany where d.CompanyUID == companyUID select d).FirstOrDefault();
            if (company == null)
                return;
            PlaceUserToDB(name, login, password, "", "", "", company.Name);
        }

        /// <summary>
        /// Guid of just-created user
        /// </summary>
        public string CreatedUserGuid { get; set; }

        private void PlaceUserToDB(string name, string login, string password, string mail, string phone, string comment, string companyName)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                tblUsersBOBUsers user = new tblUsersBOBUsers();

                var company = db.tblCompanyBOBCompany.SingleOrDefault(d => d.Name.Equals(companyName));
                if (company == null)
                {
                    company = new tblCompanyBOBCompany { Name = companyName, Adress = String.Empty, CurrentBalance = 0 };
                    db.tblCompanyBOBCompany.AddObject(company);
                    db.SaveChanges();
                }

                user.id_Company = company.id;
                user.Login = login;
                user.Mail = mail;
                user.Name = name;
                user.Phone = phone;
                user.GUID = Guid.NewGuid().ToString();
                CreatedUserGuid = user.GUID;
                Guid g = Guid.NewGuid();
                user.Solt = g.ToString();
                SecurityWorker sw = new SecurityWorker();
                user.Password1 = sw.Encrypt(password);
                string hash = ComputeHash(password, Encoding.UTF8.GetBytes(g.ToString()));
                user.Password = hash;
                user.isAdmin = false;
                db.tblUsersBOBUsers.AddObject(user);
                db.SaveChanges();
                
            }

        }

        public bool IsCanCreateAdmin()
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
                return db.tblUsersBOBUsers.Any();
        }

        public void CreateAdmin(string name, string login, string password, string mail, string phone, string comment, string companyName)
        {
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                tblUsersBOBUsers user = new tblUsersBOBUsers();

                var company = db.tblCompanyBOBCompany.SingleOrDefault(d => d.Name.Equals(companyName));
                if (company == null)
                {
                    company = new tblCompanyBOBCompany { Name = companyName, Adress = String.Empty, CurrentBalance = 0 };
                    db.tblCompanyBOBCompany.AddObject(company);
                    db.SaveChanges();
                }

                user.id_Company = company.id;
                user.Login = login;
                user.Mail = mail;
                user.Name = name;
                user.Phone = phone;
                user.GUID = Guid.NewGuid().ToString();
                Guid g = Guid.NewGuid();
                user.Solt = g.ToString();
                SecurityWorker sw = new SecurityWorker();
                user.Password1 = sw.Encrypt(password);
                string hash = ComputeHash(password, Encoding.UTF8.GetBytes(g.ToString()));
                user.Password = hash;
                user.isAdmin = true; //!!!!!!!! true
                db.tblUsersBOBUsers.AddObject(user);
                db.SaveChanges();
            }
        }

        public string ComputeHash(string plainText, byte[] saltBytes)
        {
            // If salt is not specified, generate it on the fly.
            if (saltBytes == null)
            {
                // Define min and max salt sizes.
                int minSaltSize = 4;
                int maxSaltSize = 8;

                // Generate a random number for the size of the salt.
                Random random = new Random();
                int saltSize = random.Next(minSaltSize, maxSaltSize);

                // Allocate a byte array, which will hold the salt.
                saltBytes = new byte[saltSize];

                // Initialize a random number generator.
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                // Fill the salt with cryptographically strong byte values.
                rng.GetNonZeroBytes(saltBytes);
            }

            // Convert plain text into a byte array.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes =
                    new byte[plainTextBytes.Length + saltBytes.Length];

            // Copy plain text bytes into resulting array.
            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            // Append salt bytes to the resulting array.
            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            //object of md5 algo
            MD5 md5 = new MD5CryptoServiceProvider();

            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = md5.ComputeHash(plainTextWithSaltBytes);

            // Create array which will hold hash and original salt bytes.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                saltBytes.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            // Append salt bytes to the result.
            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            // Return the result.
            return hashValue;

        }
    }
}
