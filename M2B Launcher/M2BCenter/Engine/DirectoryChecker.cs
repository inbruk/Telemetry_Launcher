using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace M2BCenter.Engine
{
    class DirectoryChecker
    {
        public void CheckDirs()
        {
            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            if (Directory.Exists(md + "\\M2B") == false)
                Directory.CreateDirectory(md + "\\M2B");
            if (Directory.Exists(md + "\\M2B\\Launcher") == false)
                Directory.CreateDirectory(md + "\\M2B\\Launcher");
        }
    }
}
