using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Net.Mail;
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
        public static string USER_MANAGER = "管理员";
        public static string USER_WORKER = "操作员";

        private static readonly string _stockCountTableName = "stockCount";
        public static string STOCKCOUNTTABLENAME
        {
            get { return _stockCountTableName; }
        }
        private static readonly string[] _stockCountTableColumns = { "id", "类型", "名称", "颜色", "适用机型", "仓库", "重量", "库存上限", "库存下限", "告警时间", "备注" };
        public static string[] STOCKCOUNTTABLECOLUMNS
        {
            get { return _stockCountTableColumns; }
        }
        private static readonly string[] _stockCountTableColumnsType = { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" };
        public static string[] STOCKCOUNTTABLECOLUMNSTYPE
        {
            get { return _stockCountTableColumnsType; }
        }

        private static readonly string _inStockTableName = "instock";
        public static string INSTOCKTABLENAME
        {
            get { return _inStockTableName; }
        }
        private static readonly string[] _inStockTableColumns = { "id", "条形码", "名称", "颜色", "类型", "重量", "适用机型", "仓库", "生产日期", "有效期", "操作员", "操作日期", "操作时间", "操作类型", "告警时间", "备注" };
        public static string[] INSTOCKTABLECOLUMNS
        {
            get { return _inStockTableColumns; }
        }
        private static readonly string[] _inStockTableColumnsType = { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" };
        public static string[] INSTOCKTABLECOLUMNSTYPE
        {
            get { return _inStockTableColumnsType; }
        }
        /*
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
        */
        private static readonly string _warnManagerTableName = "warnManager";
        public static string WARNMANAGERTABLENAME
        {
            get { return _warnManagerTableName; }
        }
        private static readonly string[] _warnManagerTableColumns = { "id", "仓库", "名称", "颜色", "类型", "库存上限", "库存下限", "告警时间", "规则创建人", "规则创建时间" };
        public static string[] WARNMANAGERTABLECOLUMNS
        {
            get { return _warnManagerTableColumns; }
        }
        private static readonly string[] _warnManagerTableColumnsType = { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" };
        public static string[] WARNMANAGERTABLECOLUMNSTYPE
        {
            get { return _warnManagerTableColumnsType; }
        }

        private static readonly string _stockLogTableName = "stockLog";
        public static string STOCKLOGTABLENAME
        {
            get { return _stockLogTableName; }
        }
        private static readonly string[] _stocklogTableColumns = { "id", "条形码", "名称", "颜色", "类型", "重量", "适用机型", "仓库", "生产日期", "有效期", "操作员", "操作日期", "操作时间", "操作类型", "告警时间", "备注" };
        public static string[] STOCKLOGTABLECOLUMNS
        {
            get { return _stocklogTableColumns; }
        }
        private static readonly string[] _stockLogTableColumnsType = { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" };
        public static string[] STOCKLOGTABLECOLUMNSTYPE
        {
            get { return _stockLogTableColumnsType; }
        }

        private static readonly string _stirLogTableName = "stirLog";
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

        public static readonly Dictionary<string, string> COATINGTYPE = new Dictionary<string, string> { { "A", "涂料" }, { "B", "固化剂" }, { "C", "C稀释剂" }, { "D", "清洗剂" } };

        public static readonly string[] WARNDATES = { "有效期前1天", "有效期前1周", "有效期前15天", "有效期前1月", "有效期前3月" };

        public static readonly string[] STOCKSNAME = { "1号仓库", "2号仓库", "3号仓库", "4号仓库" };

        public static string FilterChar(string s)
        {
            string strRemoved = Regex.Replace(s, "[a-z]", "", RegexOptions.IgnoreCase);
            return strRemoved;
        }

        //根据告警规则更新库存上、下限告警数据
        public static void UpdateStockCountWarn(string name, string stock, string color, string type, string maximum, string minimum, string expirydate)
        {
            SQLiteDataReader dataReader = SqlLiteHelper.GetInstance().ReadTable(Common.STOCKCOUNTTABLENAME, new string[] { "名称", "颜色", "仓库", "类型" }, new string[] { "=", "=", "=", "=" }, new string[] { name, color, stock, type });
            if (dataReader.HasRows && dataReader.Read())//色剂已经存在
            {
                SqlLiteHelper.GetInstance().UpdateValues(Common.STOCKCOUNTTABLENAME, new string[] { "库存上限", "库存下限", "告警时间" }, new string[] { maximum, minimum, expirydate }, "id", dataReader["id"].ToString());
            }
        }

        //根据告警规则更新在库有效期告警
        public static void UpdateExpiryDateWarn()
        {
            SQLiteDataReader warnDR = SqlLiteHelper.GetInstance().ReadFullTable(WARNMANAGERTABLENAME);
            if (warnDR.HasRows && warnDR.Read())
            {
                string expiryDate = warnDR["告警时间"].ToString();
                if (!expiryDate.Equals(""))
                {
                    string name = warnDR["名称"].ToString();
                    string stock = warnDR["仓库"].ToString();
                    string color = warnDR["颜色"].ToString();
                    string type = warnDR["类型"].ToString();
                }
            }
        }

        public static void AnalysisAllStockWarn()
        {
        }

        public static void AnalysisAllExpiryDateWarn()
        {

        }

        //发送邮件
        public static void SendMailLocalhost()
        {
            if (DateTime.Now.ToString("yyyy-MM-dd").Equals(Properties.Settings.Default.MailDate))
            {
                return;
            }
            string mailCount = Properties.Settings.Default.MailCount;
            string mailpassword = Properties.Settings.Default.MailPassword;
            string mailSMTP = Properties.Settings.Default.MailSMTP;
            string mailPort = Properties.Settings.Default.MailPort;
            string mailTo = Properties.Settings.Default.MailTo;
            string mailCC = Properties.Settings.Default.MailCC;

            if (mailCount == null || mailCount.Equals("") 
                || mailpassword == null || mailpassword.Equals("") 
                || mailSMTP == null || mailSMTP.Equals("") 
                || mailPort == null || mailPort.Equals("") 
                || mailTo == null || mailTo.Equals(""))
            {
                return;
            }

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            string[] mailToArray = mailTo.Split(new char[2] { ';', '；' });
            for (int i = 0; i < mailToArray.Length ; i++)
            {
                msg.To.Add(mailToArray[i]);
            }
            if (mailCC != null && !mailCC.Equals(""))
            {
                string[] mailCCArray = mailCC.Split(new char[2] { ';', '；' });
                for (int i = 0; i < mailCCArray.Length; i++)
                {
                    msg.CC.Add(mailCCArray[i]);
                }
            }

            msg.From = new MailAddress(mailCount, mailCount, System.Text.Encoding.UTF8);//发件人地址（可以随便写），发件人姓名，编码

            msg.Subject = "涂料告警信息";//邮件标题  
            msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码  
            msg.Body = "邮件内容";//邮件内容  
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码  
            msg.IsBodyHtml = false;//是否是HTML邮件  
            msg.Priority = MailPriority.High;//邮件优先级 

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Host = mailSMTP;
            client.Port = Convert.ToInt32(mailPort);
            client.Credentials = new System.Net.NetworkCredential(mailCount, mailpassword);//邮箱账号、密码

            object userState = msg;
            try
            {
                client.SendAsync(msg, userState);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Properties.Settings.Default.MailDate = DateTime.Now.ToString("yyyy-MM-dd");
            Properties.Settings.Default.Save();
        }
    }
}
