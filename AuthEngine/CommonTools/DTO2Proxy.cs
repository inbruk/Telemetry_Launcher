using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DTO = AuthEngine.DataTransferObjects;
using AuthEngine.DBAccess;

namespace AuthEngine.CommonTools
{
    public static class DTO2Proxy
    {        
        public static tblUsersBOBUsers Convert(DTO.User dto)
        {
            tblUsersBOBUsers proxy = new tblUsersBOBUsers();
            proxy.id = dto.id;
            proxy.Name = dto.Name;
            proxy.Login = dto.Login;
            proxy.Password = dto.Password;
            proxy.Solt = dto.Solt;              // при создании задавать
            proxy.Mail = dto.Mail;
            proxy.Phone = dto.Phone;
            proxy.id_Company = dto.id_Company;  // при создании задавать
            proxy.Password1 = dto.Password1;    // при создании задавать
            proxy.GUID = dto.GUID.ToString();   // при создании задавать
            proxy.isAdmin = dto.isAdmin;
            proxy.isSubmit = dto.isSubmit;
            proxy.isCompanyAdmin = dto.isCompanyAdmin;
            return proxy;            
        }
        public static tblCompanyBOBCompany Convert(DTO.Company dto)
        {
            tblCompanyBOBCompany proxy = new tblCompanyBOBCompany()
            {
                id = dto.id,
                Name = dto.Name,
                Adress = dto.Adress,
                CurrentBalance= dto.CurrentBalance,
                CompanyUID = dto.CompanyUID.ToString()
            };
            return proxy;
        }
    }
}
