using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthEngine
{
    public class CompanyWorker
    {
        

        public string GetCompanyName(string companyUID)
        {
            DBAccess.DB_Telemetry_UsersEntities db = new DBAccess.DB_Telemetry_UsersEntities();
            DBAccess.tblCompanyBOBCompany company = (from d in db.tblCompanyBOBCompany where d.CompanyUID == companyUID select d).FirstOrDefault();
            if (company != null)
                return company.Name;
            return string.Empty;
        }

        public string GetCompanyUID(string companyName)
        {
            DBAccess.DB_Telemetry_UsersEntities db = new DBAccess.DB_Telemetry_UsersEntities();
            DBAccess.tblCompanyBOBCompany company = (from d in db.tblCompanyBOBCompany where d.Name == companyName select d).FirstOrDefault();
            if (company != null)
            {
                if (company.CompanyUID == null)
                {
                    company.CompanyUID = Guid.NewGuid().ToString();
                    db.SaveChanges();
                }
                return company.CompanyUID;
            }
            else
            {
                DBAccess.tblCompanyBOBCompany comp = new DBAccess.tblCompanyBOBCompany();
                comp.Name = companyName;
                comp.CompanyUID = Guid.NewGuid().ToString();
                db.tblCompanyBOBCompany.AddObject(comp);
                db.SaveChanges();
                return comp.CompanyUID;
            }
        }
    }
}