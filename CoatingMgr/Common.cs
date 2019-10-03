using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Net.Mail;
using System.Text.RegularExpressions;

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

        public static readonly string DBFileName = "Coat";

        public static bool DebugLog = false;

        public static string USER_MANAGER = "管理员";
        public static string USER_WORKER = "操作员";

        public static readonly string ACCOUNTTABLENAME = "account";
        public static readonly string[] ACCOUNTTABLECOLUMNS = { "id", "账号", "密码", "权限" };
        public static readonly string[] ACCOUNTTABLECOLUMNSTYPE = { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT" };
        public static readonly string[] ACCOUNTTABLECOLUMNSTYPEFORSQLSERVER = { "INT IDENTITY PRIMARY KEY", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(10)" };

        public static readonly string STORETABLENAME = "store";
        public static readonly string[] STORETABLECOLUMNS = { "id", "名称" };
        public static readonly string[] STORETABLECOLUMNSTYPE = { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT" };
        public static readonly string[] STORETABLECOLUMNSTYPEFORSQLSERVER = { "INT IDENTITY PRIMARY KEY", "VARCHAR(20)" };

        public static readonly string STOCKCOUNTTABLENAME = "stockCount";
        public static readonly string[] STOCKCOUNTTABLECOLUMNS = { "id", "类型", "名称", "颜色", "适用机型", "重量", "库存上限", "库存下限", "告警时间", "备注" };
        public static readonly string[] STOCKCOUNTTABLECOLUMNSTYPE = { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" };
        public static readonly string[] STOCKCOUNTTABLECOLUMNSTYPEFORSQLSERVER = { "INT IDENTITY PRIMARY KEY", "VARCHAR(40)", "VARCHAR(40)", "VARCHAR(40)", "VARCHAR(40)", "VARCHAR(10)", "VARCHAR(10)", "VARCHAR(10)", "VARCHAR(20)", "VARCHAR(60)" };

        public static readonly string INSTOCKTABLENAME = "instock";
        public static readonly string[] INSTOCKTABLECOLUMNS = { "id", "条形码", "名称", "颜色", "类型", "重量", "适用机型", "仓库", "生产日期", "有效期", "操作员", "入库日期", "入库时间", "告警时间", "备注" };
        public static readonly string[] INSTOCKTABLECOLUMNSTYPE = { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" };
        public static readonly string[] INSTOCKTABLECOLUMNSTYPEFORSQLSERVER = { "INT IDENTITY PRIMARY KEY", "VARCHAR(128)", "VARCHAR(40)", "VARCHAR(40)", "VARCHAR(40)", "VARCHAR(10)", "VARCHAR(40)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(60)" };

        public static readonly string WARNMANAGERTABLENAME = "warnManager";
        public static readonly string[] WARNMANAGERTABLECOLUMNS = { "id", "名称", "颜色", "类型", "库存上限", "库存下限", "告警时间", "规则创建人", "规则创建时间" };
        public static readonly string[] WARNMANAGERTABLECOLUMNSTYPE = { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" };
        public static readonly string[] WARNMANAGERTABLECOLUMNSTYPEFORSQLSERVER = { "INT IDENTITY PRIMARY KEY", "VARCHAR(40)", "VARCHAR(40)", "VARCHAR(40)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(20)" };

        public static readonly string STOCKLOGTABLENAME = "stockLog";
        public static readonly string[] STOCKLOGTABLECOLUMNS = { "id", "条形码", "名称", "颜色", "类型", "重量", "适用机型", "仓库", "生产日期", "有效期", "操作员", "操作日期", "操作时间", "操作类型", "告警时间", "备注" };
        public static readonly string[] STOCKLOGTABLECOLUMNSTYPE = { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" };
        public static readonly string[] STOCKLOGTABLECOLUMNSTYPEFORSQLSERVER = { "INT IDENTITY PRIMARY KEY", "VARCHAR(128)", "VARCHAR(40)", "VARCHAR(40)", "VARCHAR(40)", "VARCHAR(10)", "VARCHAR(40)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(60)" };

        public static readonly string STIRLOGTABLENAME = "stirLog";
        public static readonly string[] STIRLOGTABLECOLUMNS = { "id", "机种", "製品", "颜色", "涂层", "温度", "湿度", "调和比例", "类型", "名称", "条形码", "设定重量", "倒入重量", "计量时间", "操作员", "操作日期", "操作时间", "确认主管", "备注" };
        public static readonly string[] STIRLOGTABLECOLUMNSTYPE = { "INTEGER PRIMARY KEY AUTOINCREMENT", "TEXT ", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT", "TEXT" };
        public static readonly string[] STIRLOGTABLECOLUMNSTYPEFORSQLSERVER = { "INT IDENTITY PRIMARY KEY", "VARCHAR(40) ", "VARCHAR(40)", "VARCHAR(40)", "VARCHAR(10)", "VARCHAR(10)", "VARCHAR(10)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(40)", "VARCHAR(128)", "VARCHAR(10)", "VARCHAR(10)", "VARCHAR(10)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(20)", "VARCHAR(60)" };

        public static readonly string MASTERTABLENAME = "master";

        public static readonly Dictionary<string, string> COATINGTYPE = new Dictionary<string, string> { { "A", "色漆/清漆" }, { "B", "固化剂" }, { "C", "稀释剂" }, { "D", "清洗剂" }, { "E", "清漆" }, { "F", "前处理液" } };
        public static readonly Dictionary<string, string> FACTORY = new Dictionary<string, string> { { "G1012", "恩碧" }, { "G1013", "关西" }, { "G1014", "佑天" }, { "G1015", "凡净" }, { "G1018", "沌创" } };

        public static readonly string[] WARNDATETYPE = { "有效期前1天", "有效期前1周", "有效期前15天", "有效期前30天", "有效期前100天" };
        public static readonly Dictionary<string, string> WARNDATE = new Dictionary<string, string> { { "有效期前1天", "-1" }, { "有效期前1周", "-7" }, { "有效期前15天", "-15" }, { "有效期前30天", "-30" }, { "有效期前100天", "-100" } };

        public static string FilterChar(string s)
        {
            return Regex.Replace(s, "[a-z]", "", RegexOptions.IgnoreCase);
        }

        //根据告警规则更新库存上、下限告警数据
        public static void UpdateStockCountWarn(string name, string color, string type, string maximum, string minimum, string expirydate)
        {
            DataTable dt = SQLServerHelper.Read(Common.STOCKCOUNTTABLENAME, new string[] { "名称", "颜色", "类型" }, new string[] { "=", "=", "=" }, new string[] { name, color, type });
            if (dt != null && dt.Rows.Count > 0)//色剂已经存在
            {
                SQLServerHelper.Update(Common.STOCKCOUNTTABLENAME, new string[] { "库存上限", "库存下限", "告警时间" }, new string[] { maximum, minimum, expirydate }, "id", dt.Rows[0]["id"].ToString());
            }
        }

        //根据告警规则更新在库有效期告警
        private static void UpdateExpiryDateWarn()
        {
            DataTable warnDT = SQLServerHelper.Read(WARNMANAGERTABLENAME);
            if (warnDT != null && warnDT.Rows.Count > 0)
            {
                foreach (DataRow dr in warnDT.Rows)
                {
                    string warnDate = dr["告警时间"].ToString();
                    if (!warnDate.Equals(string.Empty))
                    {
                        string name = dr["名称"].ToString();
                        string color = dr["颜色"].ToString();
                        string type = dr["类型"].ToString();
                        DataTable dt = SQLServerHelper.Read(Common.INSTOCKTABLENAME, new string[] { "名称", "颜色", "类型" }, new string[] { "=", "=", "=" }, new string[] { name, color, type });
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            DateTime expiryDate = DateTime.ParseExact(dataRow["有效期"].ToString(), "yyyyMMdd", null);
                            DateTime date = expiryDate.AddDays(Convert.ToInt32(WARNDATE[warnDate]));
                            if (!date.ToString("yyyyMMdd").Equals(dataRow["告警时间"].ToString()))
                            {
                                SQLServerHelper.Update(Common.INSTOCKTABLENAME, new string[] { "告警时间" }, new string[] { date.ToString("yyyyMMdd") }, "id", dataRow["id"].ToString());
                            }
                        }
                    }
                }
            }
        }

        //分析告警数据并发送邮件，每天第一次进入MainForm时执行一次
        public static void AnalysisWarn()
        {
            if (!DateTime.Now.ToString("yyyyMMdd").Equals(Properties.Settings.Default.MailDate))
            {
                UpdateExpiryDateWarn();
                if (SendWarnMail())
                {
                    Properties.Settings.Default.MailDate = DateTime.Now.ToString("yyyyMMdd");
                    Properties.Settings.Default.Save();
                }
            }
        }

        //发送邮件
        private static Boolean SendWarnMail()
        {
            bool result = false;
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
                return false;
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
            msg.Body = SetMailBody();//邮件内容  
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码  
            msg.IsBodyHtml = true;//是否是HTML邮件  
            msg.Priority = MailPriority.High;//邮件优先级 

            SmtpClient client = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Host = mailSMTP,
                Port = Convert.ToInt32(mailPort),
                Credentials = new System.Net.NetworkCredential(mailCount, mailpassword)//邮箱账号、密码
            };

            object userState = msg;
            try
            {
                client.SendAsync(msg, userState);
                result = true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                Logger.Instance.WriteLog(ex.Message);
                result = false;
            }
            return result;
        }

        //以HTML表格形式填充邮件内容
        private static string SetMailBody()
        {
            string MailBody = "<p style=\"font-size: 10pt\">以下内容为系统自动发送，请勿直接回复，谢谢。</p>";

            //库存上、下限告警数据
            DataTable stockWarnData = SQLServerHelper.ReadStockWarnFromTable(STOCKCOUNTTABLENAME, new string[] { "重量", "库存上限", "重量", "库存下限" }, new string[] { ">", "!=", "<", "!=" }, new string[] { "库存上限", "''", "库存下限", "''" }, new string[] { "AND", "OR", "AND" });
            if (stockWarnData != null && stockWarnData.Rows.Count > 0)
            {
                MailBody += "<p style=\"font-size: 10pt\">库存告警：</p>";
                MailBody += "<table cellspacing=\"1\" cellpadding=\"3\" border=\"0\" bgcolor=\"000000\" style=\"font-size: 10pt;line-height: 15px;\">";
                MailBody += "<div align=\"center\">";
                MailBody += "<tr>";
                for (int hcol = 0; hcol < stockWarnData.Columns.Count; hcol++)
                {
                    MailBody += "<td bgcolor=\"999999\">&nbsp;&nbsp;&nbsp;";
                    MailBody += stockWarnData.Columns[hcol].ColumnName;
                    MailBody += "&nbsp;&nbsp;&nbsp;</td>";
                }
                MailBody += "</tr>";

                for (int row = 0; row < stockWarnData.Rows.Count; row++)
                {
                    MailBody += "<tr>";
                    for (int col = 0; col < stockWarnData.Columns.Count; col++)
                    {
                        MailBody += "<td bgcolor=\"dddddd\">&nbsp;&nbsp;&nbsp;";
                        MailBody += stockWarnData.Rows[row][col].ToString();
                        MailBody += "&nbsp;&nbsp;&nbsp;</td>";
                    }
                    MailBody += "</tr>";
                }
                MailBody += "</table>";
                MailBody += "</div>";
            }

            //有效期告警数据
            DataTable expiryWarnData = SQLServerHelper.Read(INSTOCKTABLENAME, new string[] { "告警时间", "告警时间" }, new string[] { ">", "<=" }, new string[] { "0", DateTime.Now.ToString("yyyyMMdd") });
            if (expiryWarnData != null && expiryWarnData.Rows.Count > 0)
            {
                MailBody += "<p style=\"font-size: 10pt\">有效期告警：</p>";
                MailBody += "<table cellspacing=\"1\" cellpadding=\"3\" border=\"0\" bgcolor=\"000000\" style=\"font-size: 10pt;line-height: 15px;\">";
                MailBody += "<div align=\"center\">";
                MailBody += "<tr>";
                for (int hcol = 0; hcol < expiryWarnData.Columns.Count; hcol++)
                {
                    MailBody += "<td bgcolor=\"999999\">&nbsp;&nbsp;&nbsp;";
                    MailBody += expiryWarnData.Columns[hcol].ColumnName;
                    MailBody += "&nbsp;&nbsp;&nbsp;</td>";
                }
                MailBody += "</tr>";

                for (int row = 0; row < expiryWarnData.Rows.Count; row++)
                {
                    MailBody += "<tr>";
                    for (int col = 0; col < expiryWarnData.Columns.Count; col++)
                    {
                        MailBody += "<td bgcolor=\"dddddd\">&nbsp;&nbsp;&nbsp;";
                        MailBody += expiryWarnData.Rows[row][col].ToString();
                        MailBody += "&nbsp;&nbsp;&nbsp;</td>";
                    }
                    MailBody += "</tr>";
                }
                MailBody += "</table>";
                MailBody += "</div>";
            }
            return MailBody;
        }

        /// <summary>
        ///等待提示界面
        /// </summary>
        private static FormProgress formProgress = null;
        public static void ShowProgress()
        {
            if (formProgress == null)
            {
                formProgress = new FormProgress();
            }
            formProgress.Show();
        }

        public static void CloseProgress()
        {
            formProgress.Close();
            formProgress = null;
        }
    }
}
