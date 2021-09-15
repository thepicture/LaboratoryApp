using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryApp.Models
{
    public class AppContext
    {
        private static LaboratoryBaseEntities _instance;
        /// <summary>
        /// DbFactory for database fetching.
        /// </summary>
        /// <returns></returns>
        public static LaboratoryBaseEntities GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LaboratoryBaseEntities();
            }
            return _instance;
        }
    }
}
