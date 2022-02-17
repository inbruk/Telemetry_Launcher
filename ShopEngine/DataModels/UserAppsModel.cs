using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopEngine.DataModels
{
    public class UserAppsModel
    {
        public string AppName { get; set; }

        public string IconURL { get; set; }

        public string ActualFileName { get; set; }

        public string Banner250x250URL { get; set; }

        public double CurrentVersion { get; set; }
    }
}
