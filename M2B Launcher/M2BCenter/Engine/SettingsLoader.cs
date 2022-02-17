using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using M2BCenter.Model;

namespace M2BCenter.Engine
{
    class SettingsLoader
    {
        private static UserSettingsModel _sm = new UserSettingsModel();

        public static UserSettingsModel SM
        {
            get { return _sm; }
            set { _sm = value; }
        }

        public static void Load()
        {
            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            XmlSerializer s = new XmlSerializer(typeof(UserSettingsModel));
            if (File.Exists(md + "\\M2B\\Launcher\\Settings.m2bs") == false)
                Save();
            FileStream fs = new FileStream(md + "\\M2B\\Launcher\\Settings.m2bs", FileMode.Open);
            _sm = (UserSettingsModel)s.Deserialize(fs);
            fs.Close();
        }

        public static void Save()
        {
            string md = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            if (File.Exists(md + "\\M2B\\Launcher\\Settings.m2bs") == true)
                File.Delete(md + "\\M2B\\Launcher\\Settings.m2bs");
            XmlSerializer xml = new XmlSerializer(typeof(UserSettingsModel));
            TextWriter tw = new StreamWriter(md + "\\M2B\\Launcher\\Settings.m2bs");
            xml.Serialize(tw, _sm);
            tw.Close();
        }
    }
}
