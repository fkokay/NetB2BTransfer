using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetB2BTransfer.Logo.Library.Class
{
    public class LogoUtils
    {
        public static string TableNameWithFirm(string dbname, string firmnr, string table)
        {
            var dbName = "[" + dbname + "].[dbo].";
            return dbName + ($"LG_{FirmNoStr(firmnr)}_{table}");
            //return ($"LG_{FirmNoStr(firmnr)}_{table}");
        }
        public static string TableNameWithFirmPlusPeriod(string dbname, string firmnr, string periodnr, string table)
        {
            var dbName = "[" + dbname + "].[dbo].";
            return dbName + ($"LG_{FirmNoStr(firmnr)}_{PeriodStr(periodnr)}_{table}");
        }
        public static string TableViewNameWithFirmPlusPeriod(string dbname, string firmnr, string periodnr, string table)
        {
            var dbName = "[" + dbname + "].[dbo].";
            return dbName + ($"LV_{FirmNoStr(firmnr)}_{PeriodStr(periodnr)}_{table}");
        }

        public static string FirmNoStr(string firmnr)
        {
            var fN = firmnr.ToString();
            switch (fN.Length)
            {
                case 1:
                    return ("00" + fN);

                case 2:
                    return ("0" + fN);

                case 3:
                    return fN;
            }
            return "";
        }

        public static string PeriodStr(string periodnr)
        {
            var p = periodnr.ToString();
            if (p.Length < 2)
            {
                p = "0" + p;
            }
            return p;
        }
    }
}
