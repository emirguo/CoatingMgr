using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoatingMgr
{
    public class Common
    {
        private static string _dbPath;
        public static string DBPath
        {
            get { return _dbPath; }
            set{ _dbPath = value; }
        }

        private static readonly string _accountTableName = "account";
        public static string ACCOUNTTABLENAME
        {
            get { return _accountTableName; }
        }

        private static readonly string _stockManagerTableName = "stockmanager";
        public static string STOCKMANAGERTABLENAME
        {
            get { return _stockManagerTableName; }
        }

        private static readonly string _inStockTableName = "instock";
        public static string INSTOCKTABLENAME
        {
            get { return _inStockTableName; }
        }

        private static readonly string _outStockTableName = "outstock";
        public static string OUTSTOCKTABLENAME
        {
            get { return _outStockTableName; }
        }

        private static readonly string _warnManagerTableName = "warnmanager";
        public static string WARNMANAGERTABLENAME
        {
            get { return _warnManagerTableName; }
        }

        private static readonly string _logTableName = "log";
        public static string LOGTABLENAME
        {
            get { return _logTableName; }
        }

        private static readonly string _masterTableName = "master";
        public static string MASTERTABLENAME
        {
            get { return _masterTableName; }
        }

        public static string FilterNum(string s)
        {
            string strRemoved = Regex.Replace(s, "[a-z]", "", RegexOptions.IgnoreCase);
            return strRemoved;
        }
    }
}
