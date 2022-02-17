using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DTO = AuthEngine.DataTransferObjects;
using AuthEngine.CommonTools;
using AuthEngine.DBAccess;
using ShopEngine;

namespace AuthEngine
{
    public static class UsersTools
    {
        /// <summary>
        /// Users registration, registration canceling when user with such login exists in DB 
        /// In other cases exceptions throwed. Auto login when registred not happened.
        /// </summary>
        /// <param name="userRegInfo"> all registration info (about user's company too) </param>
        /// <returns> true - registration complete succesfuly, false - shuch user exists in DB already </returns>
        public static bool RegisterNewUser(DTO.UserRegistrationInfo userRegInfo)
        {
            // check parameters
            if (userRegInfo.user == null) throw new Exception("User data not filled.");
            if (userRegInfo.user.Login == null) throw new Exception("User login not filled.");
            if (userRegInfo.user.Name == null) throw new Exception("User name not filled.");
            if (userRegInfo.user.Password == null) throw new Exception("User password not filled.");

            if (userRegInfo.usersCompany == null) throw new Exception("Company data not filled.");
            if (userRegInfo.usersCompany.Name == null) throw new Exception("Company name not filled.");

            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                tblUsersBOBUsers currUser = db.tblUsersBOBUsers.SingleOrDefault(x => x.Login == userRegInfo.user.Login);

                // check user existance
                if (currUser != null)
                {
                    return false;
                }

                // check company existance
                tblCompanyBOBCompany currCompany = db.tblCompanyBOBCompany.SingleOrDefault(x => x.Name == userRegInfo.usersCompany.Name);

                // if we have no company, that we create it, fill it and save it
                if( currCompany==null )
                {
                    userRegInfo.usersCompany.CompanyUID = Guid.NewGuid();

                    currCompany = DTO2Proxy.Convert( userRegInfo.usersCompany );
                    db.tblCompanyBOBCompany.AddObject(currCompany);
                    db.SaveChanges();
                }

                // dont forget update company id in user entity
                userRegInfo.user.id_Company = currCompany.id;

                // generate hash in Password1 (in AuthEngine.AuthWork.ChangePassword style)
                SecurityWorker sw = new SecurityWorker();
                string encryptedPassword = sw.Encrypt(userRegInfo.user.Password);
                userRegInfo.user.Password1 = encryptedPassword;

                // additional not filled data 
                userRegInfo.user.GUID = Guid.NewGuid();
                userRegInfo.user.Solt = Guid.NewGuid().ToString();
                userRegInfo.user.isSubmit = true;

                currUser = DTO2Proxy.Convert(userRegInfo.user);

                // create new user                
                db.tblUsersBOBUsers.AddObject(currUser);
                db.SaveChanges();

                // create same user in Shop Database
                UserApps shopUA = new UserApps();
                shopUA.CreateCustomUser(currUser.id, currUser.GUID);
            }

            return true;
        }

        public static DTO.UserRegistrationInfo GetRegistrationInfoByUserId(Int32 userId)
        {
            DTO.UserRegistrationInfo currInfo = new DTO.UserRegistrationInfo();
            using (DB_Telemetry_UsersEntities db = new DB_Telemetry_UsersEntities())
            {
                currInfo.user = Proxy2DTO.Convert(db.tblUsersBOBUsers.Single(x => x.id == userId)); // если не существует, то exception
                currInfo.usersCompany = Proxy2DTO.Convert(db.tblCompanyBOBCompany.Single(x => x.id == currInfo.user.id_Company)); // отсутствие компании - ошибка в БД              
            }
            return currInfo;
        }
    }
}
