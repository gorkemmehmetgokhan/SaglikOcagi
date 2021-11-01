using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaglikOcagi.Entity;


namespace SaglikOcagi.Common
{
    public class Tools
    {
        public static DB_SaglikMerkeziEntities db = null;

        public static DB_SaglikMerkeziEntities GetConnection()
        {
            if (db == null)
            {
                db = new DB_SaglikMerkeziEntities();
            }

            return db;
        }
    }
}
