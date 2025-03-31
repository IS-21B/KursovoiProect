using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursovoiProect
{
    class Date
    {
        static public string host = Properties.Settings.Default.host;
        static public string user = Properties.Settings.Default.uid;
        static public string db = Properties.Settings.Default.database;
        static public string pwd = Properties.Settings.Default.pwd;
        public static int role = 0;
        static public string conStr = $@"host={host};uid={user};pwd={pwd};database={db}";
        static public string conStr2 = $@"host={host};uid={user};pwd={pwd}";
    }
}
