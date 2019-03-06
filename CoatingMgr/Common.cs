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
        private static readonly string[] _accountTableColumns = { "id", "账号", "密码", "权限" };
        public static string[] ACCOUNTTABLECOLUMNS
        {
            get { return _accountTableColumns; }
        }
        private static readonly string[] _accountTableColumnsType = { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT" };
        public static string[] ACCOUNTTABLECOLUMNSTYPE
        {
            get { return _accountTableColumnsType; }
        }

        private static readonly string _stockCountTableName = "stockCount";
        public static string STOCKCOUNTTABLENAME
        {
            get { return _stockCountTableName; }
        }
        private static readonly string[] _stockCountTableColumns = { "id", "类型", "名称", "颜色", "适用机型", "仓库", "重量", "告警类型", "备注" };
        public static string[] STOCKCOUNTTABLECOLUMNS
        {
            get { return _stockCountTableColumns; }
        }
        private static readonly string[] _stockCountTableColumnsType = { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" };
        public static string[] STOCKCOUNTTABLECOLUMNSTYPE
        {
            get { return _stockCountTableColumnsType; }
        }

        private static readonly string _inStockTableName = "instock";
        public static string INSTOCKTABLENAME
        {
            get { return _inStockTableName; }
        }
        private static readonly string[] _inStockTableColumns = { "id", "条形码", "名称", "颜色", "类型", "重量", "适用机型", "生产日期", "有效期", "仓库", "操作员", "操作日期", "操作时间", "操作类型", "告警类型", "备注" };
        public static string[] INSTOCKTABLECOLUMNS
        {
            get { return _inStockTableColumns; }
        }
        private static readonly string[] _inStockTableColumnsType = { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" };
        public static string[] INSTOCKTABLECOLUMNSTYPE
        {
            get { return _inStockTableColumnsType; }
        }

        private static readonly string _outStockTableName = "outstock";
        public static string OUTSTOCKTABLENAME
        {
            get { return _outStockTableName; }
        }
        private static readonly string[] _outStockTableColumns = { "id", "条形码", "名称", "颜色", "类型", "重量", "适用机型", "生产日期", "有效期", "仓库", "操作员", "操作日期", "操作时间", "操作类型", "告警类型", "备注" };
        public static string[] OUTSTOCKTABLECOLUMNS
        {
            get { return _outStockTableColumns; }
        }
        private static readonly string[] _outStockTableColumnsType = { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" };
        public static string[] OUTSTOCKTABLECOLUMNSTYPE
        {
            get { return _outStockTableColumnsType; }
        }

        private static readonly string _warnManagerTableName = "warnmanager";
        public static string WARNMANAGERTABLENAME
        {
            get { return _warnManagerTableName; }
        }
        private static readonly string[] _warnManagerTableColumns = { "id", "仓库", "产品", "颜色", "类型", "库存上限", "库存下限", "告警时间", "告警类型", "规则创建人", "规则创建时间" };
        public static string[] WARNMANAGERTABLECOLUMNS
        {
            get { return _warnManagerTableColumns; }
        }
        private static readonly string[] _warnManagerTableColumnsType = { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" };
        public static string[] WARNMANAGERTABLECOLUMNSTYPE
        {
            get { return _warnManagerTableColumnsType; }
        }

        private static readonly string _stockLogTableName = "stocklog";
        public static string STOCKLOGTABLENAME
        {
            get { return _stockLogTableName; }
        }
        private static readonly string[] _stocklogTableColumns = { "id", "条形码", "名称", "颜色", "类型", "重量", "适用机型", "生产日期", "有效期", "仓库", "操作员", "操作日期", "操作时间", "操作类型", "告警类型", "备注" };
        public static string[] STOCKLOGTABLECOLUMNS
        {
            get { return _stocklogTableColumns; }
        }
        private static readonly string[] _stockLogTableColumnsType = { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" };
        public static string[] STOCKLOGTABLECOLUMNSTYPE
        {
            get { return _stockLogTableColumnsType; }
        }

        private static readonly string _stirLogTableName = "stirlog";
        public static string STIRLOGTABLENAME
        {
            get { return _stirLogTableName; }
        }
        private static readonly string[] _stirlogTableColumns = { "id", "机型 ", "部件", "颜色", "温度", "湿度", "比例", "类型", "名称", "条形码", "设定重量", "倒入重量", "计量时间", "操作员", "操作日期", "操作时间", "确认主管", "备注" };
        public static string[] STIRLOGTABLECOLUMNS
        {
            get { return _stirlogTableColumns; }
        }
        private static readonly string[] _stirLogTableColumnsType = { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT ", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" };
        public static string[] STIRLOGTABLECOLUMNSTYPE
        {
            get { return _stirLogTableColumnsType; }
        }

        private static readonly string _masterTableName = "master";
        public static string MASTERTABLENAME
        {
            get { return _masterTableName; }
        }

        public static string FilterChar(string s)
        {
            string strRemoved = Regex.Replace(s, "[a-z]", "", RegexOptions.IgnoreCase);
            return strRemoved;
        }

    }
}
