using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DTO = AuthEngine.DataTransferObjects;
using AuthEngine.DBAccess;

namespace AuthEngine.CommonTools
{
    public static class Proxy2DTO
    {
        public static DTO.User Convert(tblUsersBOBUsers proxy)
        {
            DTO.User dto = new DTO.User()
            {
                id = proxy.id,
                Name = proxy.Name,
                Login = proxy.Login,
                Password = proxy.Password,
                Solt = proxy.Solt,
                Mail = proxy.Mail,
                Phone = proxy.Phone,
                id_Company = proxy.id_Company,
                Password1 = proxy.Password1,
                GUID = Guid.Parse(proxy.GUID),
                isAdmin = proxy.isAdmin,
                isSubmit = proxy.isSubmit,
                isCompanyAdmin = proxy.isCompanyAdmin
            };
            return dto;
        }
        public static DTO.Company Convert(tblCompanyBOBCompany proxy)
        {
            DTO.Company dto = new DTO.Company()
            {
                id = proxy.id,
                Name = proxy.Name,
                Adress = proxy.Adress,
                CurrentBalance = proxy.CurrentBalance,
                CompanyUID = Guid.Parse(proxy.CompanyUID)
            };
            return dto;
        }
    }
}
