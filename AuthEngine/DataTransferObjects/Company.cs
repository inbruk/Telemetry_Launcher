using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthEngine.DataTransferObjects
{
    public class Company
    {
        public Int32 id { set; get; }
        public String Name { set; get; }
        public String Adress { set; get; }
        public Decimal CurrentBalance { set; get; }
        public Guid CompanyUID { set; get; }
    }
}
