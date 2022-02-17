using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthEngine.DataTransferObjects
{
    public class UserRegistrationInfo
    {
        public User user { set; get; }
        public Company usersCompany { set; get; }
    }
}
