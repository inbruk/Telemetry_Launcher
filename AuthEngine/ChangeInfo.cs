using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthEngine
{
    public class ChangeInfo
    {
        public enum ChangeType { EMail, Phone, Password }

        public string UserLogin { get; set; }
        public string Key { get; set; }
        public ChangeType Change { get; set; }
        public string NewValue { get; set; }
    }
}
