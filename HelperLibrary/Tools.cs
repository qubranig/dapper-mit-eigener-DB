using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary
{
    public static class Tools
    {
        //holt er sich den datenbankstring aus dem sql server database project?
        //https://www.youtube.com/watch?v=eKkh5Xm0OlU gefragt...
        public static string GetConnectionString(string name = "DB_test_Charp" /*optionaler Parameter da bereits zugewiesen */)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
