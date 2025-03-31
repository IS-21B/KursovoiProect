using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursovoiProect
{
    public class Connection
    {
        public static string host = Properties.Settings.Default.host;
        public static string database = Properties.Settings.Default.database;
        public static string uid = Properties.Settings.Default.uid;
        public static string pwd = Properties.Settings.Default.pwd;

        public static string myConnection = $@"host={host}; uid={uid}; pwd={pwd}; database={database}";
    }
}
