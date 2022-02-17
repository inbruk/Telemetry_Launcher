using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopEngine
{
    public class Service
    {
        DBAccess.DB_Telemetry_ShopAppsEntities db = new DBAccess.DB_Telemetry_ShopAppsEntities();

        public Service()
        {

        }

        public void CheckDB()
        {
            //if (db.DatabaseExists() == false)
            //    db.CreateDatabase();
        }
    }
}
