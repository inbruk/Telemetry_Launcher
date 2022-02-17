using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthEngine
{
    public class AuthInfo
    {
        public int id { get; set; }

        public string Login { get; set; }

        public string CompanyUID { get; set; }

        public bool isCompanyAdmin { get; set; }

        public string CompanyName { get; set; }
    }
}
