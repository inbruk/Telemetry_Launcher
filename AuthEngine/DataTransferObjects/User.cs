using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthEngine.DataTransferObjects
{
    public class User
    {
        public Int32 id { set; get; }
        public String Name { set; get; }
        public String Login { set; get; }
        public String Password { set; get; }
        public String Solt { set; get; }
        public String Mail { set; get; }
        public String Phone { set; get; }
        public Int32 id_Company { set; get; }
        public String Password1 { set; get; }
        public Guid GUID { set; get; }        
        public Boolean isAdmin { set; get; }
        public Boolean isSubmit { set; get; }
        public Boolean? isCompanyAdmin { set; get; }
    }
}
