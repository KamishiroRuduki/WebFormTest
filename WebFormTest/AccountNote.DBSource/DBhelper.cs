using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountNote.DBSource
{
   public class DBhelper
    {
        public static string GetConnectionString()
        {
            // string val = ConfigurationManager.AppSettings["ConnectionString"];
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }
    }
}
