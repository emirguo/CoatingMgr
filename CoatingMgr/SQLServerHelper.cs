using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CoatingMgr
{
    class SQLServerHelper
    {
        /// <summary>
        /// 创建Access数据库
        /// </summary>
        /// <returns>真为创建成功，假为创建失败或是文件已存在</returns>
        /// Data Source=.;Initial Catalog=db_bookmanage; Integrated Security=SSPI
        public static bool CreateDB()
        {
            bool result = false;
            if (Properties.Settings.Default.SQLIP.Equals(string.Empty)
                || Properties.Settings.Default.SQLPort.Equals(string.Empty)
                || Properties.Settings.Default.SQLUser.Equals(string.Empty)
                || Properties.Settings.Default.SQLPwd.Equals(string.Empty))
            {
                MessageBox.Show("请先设置数据库信息");
                return result;
            }

            try
            {
                string connStr = "Data Source = " + Properties.Settings.Default.SQLIP /* +"," + Properties.Settings.Default.SQLPort */ + ";Persist Security Info = yes; uid = " + Properties.Settings.Default.SQLUser + "; PWD = " + Properties.Settings.Default.SQLPwd;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("CREATE DATABASE " + Common.DBFileName + ";", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建数据库失败，" + ex.Message);
                Logger.Instance.WriteLog("创建数据库失败，" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 创建数据表
        /// </summary> +
        /// <returns>create result</returns>
        /// <param name="tableName">数据表名</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colTypes">字段名类型</param>  
        /// sql = "CREATE TABLE table1(ID AUTOINCREMENT,GdNum TEXT(50),Address TEXT(50),PrintResult yesno,PrintDate DateTime,CONSTRAINT table1_PK PRIMARY KEY(ID));";
        public static bool CreateTable(string tableName, string[] colNames, string[] colTypes)
        {
            bool result = false;
            if (Properties.Settings.Default.SQLIP.Equals(string.Empty)
                || Properties.Settings.Default.SQLPort.Equals(string.Empty)
                || Properties.Settings.Default.SQLUser.Equals(string.Empty)
                || Properties.Settings.Default.SQLPwd.Equals(string.Empty))
            {
                MessageBox.Show("请先设置数据库信息");
                return result;
            }

            string connStr = "Data Source = " + Properties.Settings.Default.SQLIP + "," + Properties.Settings.Default.SQLPort
                + "; uid = " + Properties.Settings.Default.SQLUser + "; pwd = " + Properties.Settings.Default.SQLPwd + ";Initial Catalog = " + Common.DBFileName;

            string sql = "CREATE TABLE " + tableName + " ([" + colNames[0] + "] " + colTypes[0];
            for (int i = 1; i < colNames.Length; i++)
            {
                sql += ", [" + colNames[i] + "] " + colTypes[i];
            }
            sql += ")";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog("创建表" + tableName + "失败," + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 更新数据表字段
        /// </summary> +
        /// <returns>update result</returns>
        /// <param name="tableName">数据表名</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colTypes">字段名类型</param>  
        /// ALTER TABLE People ADD Sex Boolean
        public static bool UpdateTable(string tableName, string[] colNames, string[] colTypes)
        {
            bool result = false;
            if (Properties.Settings.Default.SQLIP.Equals(string.Empty)
                || Properties.Settings.Default.SQLPort.Equals(string.Empty)
                || Properties.Settings.Default.SQLUser.Equals(string.Empty)
                || Properties.Settings.Default.SQLPwd.Equals(string.Empty))
            {
                MessageBox.Show("请先设置数据库信息");
                return result;
            }

            string connStr = "Data Source = " + Properties.Settings.Default.SQLIP + "," + Properties.Settings.Default.SQLPort
                + "; uid = " + Properties.Settings.Default.SQLUser + "; pwd = " + Properties.Settings.Default.SQLPwd + ";Initial Catalog = " + Common.DBFileName;

            string sql = "ALTER TABLE " + tableName + " ADD " + colNames[0] + " " + colTypes[0];
            for (int i = 1; i < colNames.Length; i++)
            {
                sql += ", " + colNames[i] + " " + colTypes[i];
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog("更新表" + tableName + "失败," + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 删除表
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="TableName">数据表</param>
        ///DROP TABLE TableName;
        public static bool DeleteTable(string tableName)
        {
            bool result = false;
            string connStr = "Data Source = " + Properties.Settings.Default.SQLIP + "," + Properties.Settings.Default.SQLPort
                + "; uid = " + Properties.Settings.Default.SQLUser + "; pwd = " + Properties.Settings.Default.SQLPwd + ";Initial Catalog = " + Common.DBFileName;

            string sql = "DROP TABLE " + tableName;
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog("删除表" + tableName + "失败," + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 清除表内所有数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="TableName">数据表</param>
        ///DELETE FROM TableName;
        public static bool ClearTable(string tableName)
        {
            bool result = false;
            string connStr = "Data Source = " + Properties.Settings.Default.SQLIP + "," + Properties.Settings.Default.SQLPort
                + "; uid = " + Properties.Settings.Default.SQLUser + "; pwd = " + Properties.Settings.Default.SQLPwd + ";Initial Catalog = " + Common.DBFileName;

            string sql = "DELETE FROM " + tableName;
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog("清空表" + tableName + "失败," + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 请表ID归0
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="TableName">数据表</param>
        ///DELETE FROM sqlite_sequence WHERE name = ‘TableName’;
        public static bool ResetTableId(string tableName)
        {
            return false;
        }

        /// <summary>
        /// 读取表中所有数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static DataTable Read(string tableName)
        {
            DataTable dt = new DataTable();
            string connStr = "Data Source = " + Properties.Settings.Default.SQLIP + "," + Properties.Settings.Default.SQLPort
                + "; uid = " + Properties.Settings.Default.SQLUser + "; pwd = " + Properties.Settings.Default.SQLPwd + ";Initial Catalog = " + Common.DBFileName;

            string sql = "SELECT * FROM " + tableName;
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        SqlDataAdapter mda = new SqlDataAdapter(cmd);
                        mda.Fill(dt);
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog("读取表" + tableName + "失败," + ex.Message);
                return null;
            }
            return dt;
        }

        /// <summary>
        /// 从数据表中读取数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colNames">列名</param>
        /// <param name="operations">运算符,大于，小于，等于</param>
        /// <param name="colValues">数值</param>
        /// <returns></returns>
        /// SELECT * FROM TABLE WHERE NAME='A' AND TYPE='T';
        public static DataTable Read(string tableName, string[] colNames, string[] operations, string[] colValues)
        {
            DataTable dt = new DataTable();
            string connStr = "Data Source = " + Properties.Settings.Default.SQLIP + "," + Properties.Settings.Default.SQLPort
                + "; uid = " + Properties.Settings.Default.SQLUser + "; pwd = " + Properties.Settings.Default.SQLPwd + ";Initial Catalog = " + Common.DBFileName;

            string sql = "SELECT * FROM " + tableName + " WHERE " + colNames[0] + " " + operations[0] + " '" + colValues[0] + "'";
            for (int i = 1; i < colNames.Length; i++)
            {
                sql += " AND " + colNames[i] + " " + operations[i] + " '" + colValues[i] + "' ";
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        SqlDataAdapter mda = new SqlDataAdapter(cmd);
                        mda.Fill(dt);
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog("读取表" + tableName + "失败," + ex.Message);
                return null;
            }
            return dt;
        }

        /// <summary>
        /// 从数据表中读取数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colNames">列名</param>
        /// <param name="operations">运算符,大于，小于，等于</param>
        /// <param name="colValues">数值</param>
        /// <param name="orAnds">判断条件间的关系，OR/AND</param>
        /// <returns></returns>
        /// SELECT * FROM TABLE WHERE NAME='A' OR TYPE='T';
        public static DataTable Read(string tableName, string[] colNames, string[] operations, string[] colValues, string[] orAnds)
        {
            DataTable dt = new DataTable();
            string connStr = "Data Source = " + Properties.Settings.Default.SQLIP + "," + Properties.Settings.Default.SQLPort
                + "; uid = " + Properties.Settings.Default.SQLUser + "; pwd = " + Properties.Settings.Default.SQLPwd + ";Initial Catalog = " + Common.DBFileName;

            string sql = "SELECT * FROM " + tableName + " WHERE " + colNames[0] + " " + operations[0] + " '" + colValues[0] + "'";
            for (int i = 1; i < colNames.Length; i++)
            {
                sql += " " + orAnds[i - 1] + " " + colNames[i] + " " + operations[i] + " '" + colValues[i] + "' ";
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        SqlDataAdapter mda = new SqlDataAdapter(cmd);
                        mda.Fill(dt);
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog("读取表" + tableName + "失败," + ex.Message);
                return null;
            }

            return dt;
        }

        /// <summary>
        /// 数据表中插入一行
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="values">数据</param>
        /// <returns>是否插入成功</returns>
        /// "INSERT INTO TABLE (账号,密码,权限) values ('a','a','管理员')";
        public static bool Insert(string tableName, string[] columns, string[] values)
        {
            bool result = false;
            string connStr = "Data Source = " + Properties.Settings.Default.SQLIP + "," + Properties.Settings.Default.SQLPort
                + "; uid = " + Properties.Settings.Default.SQLUser + "; pwd = " + Properties.Settings.Default.SQLPwd + ";Initial Catalog = " + Common.DBFileName;

            string sql = "INSERT INTO " + tableName + " ([" + columns[0];
            for (int i = 1; i < columns.Length; i++)
            {
                sql += "], [" + columns[i];
            }
            sql += "])" + " VALUES ('" + values[0] + "'";
            for (int i = 1; i < values.Length; i++)
            {
                sql += ", '" + values[i] + "'";
            }
            sql += " )";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog("插入表" + tableName + "失败," + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 数据表中删除
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colNames">列名</param>
        /// <param name="colValues">数值</param>
        /// <param name="operations">运算符,大于，小于，等于</param>
        /// <param name="relations">判断条件间的关系 AND/OR</param>
        /// <returns>是否删除成功</returns>
        /// /// DELETE FROM TABLE WHERE NAME='A' AND TYPE='T';
        public static bool Delete(string tableName, string[] colNames, string[] colValues, string[] operations, string[] relations)
        {
            bool result = false;
            string connStr = "Data Source = " + Properties.Settings.Default.SQLIP + "," + Properties.Settings.Default.SQLPort
                + "; uid = " + Properties.Settings.Default.SQLUser + "; pwd = " + Properties.Settings.Default.SQLPwd + ";Initial Catalog = " + Common.DBFileName;

            string sql = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                sql += relations[i - 1] + colNames[i] + operations[i] + "'" + colValues[i] + "'";
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog("从" + tableName + "删除失败," + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 更新表中数据
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        /// <param name="key">关键字</param>
        /// <param name="value">关键字对应的值</param>
        /// <param name="operation">运算符：=,<,>,...，默认“=”</param>
        /// <returns></returns>
        public static bool Update(string tableName, string[] colNames, string[] colValues, string key, string value, string operation = "=")
        {
            bool result = false;
            string connStr = "Data Source = " + Properties.Settings.Default.SQLIP + "," + Properties.Settings.Default.SQLPort
                + "; uid = " + Properties.Settings.Default.SQLUser + "; pwd = " + Properties.Settings.Default.SQLPwd + ";Initial Catalog = " + Common.DBFileName;

            string sql = "UPDATE " + tableName + " SET " + colNames[0] + "=" + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                sql += ", " + colNames[i] + "=" + "'" + colValues[i] + "'";
            }
            sql += " WHERE " + key + operation + value;
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog("更新表" + tableName + "失败," + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 获取某个字段所有不同值的数据
        /// </summary>
        /// <returns>list<string>.</returns>
        /// <param name="TableName">数据表</param>
        /// <param name="column">字段</param>
        ///SELECT column FROM TableName;
        ///SELECT column FROM TableName WHERE 色番='YR-614P' AND 涂层='下涂'
        public static List<string> GetTypesOfColumn(string tableName, string column, string[] colNames, string[] operations, string[] colValues)
        {
            List<string> list = new List<string>();
            DataTable dt = new DataTable();
            string connStr = "Data Source = " + Properties.Settings.Default.SQLIP + "," + Properties.Settings.Default.SQLPort
                + "; uid = " + Properties.Settings.Default.SQLUser + "; pwd = " + Properties.Settings.Default.SQLPwd + ";Initial Catalog = " + Common.DBFileName;

            string sql = "SELECT " + column + " FROM " + tableName;
            if (colNames != null && colNames.Length > 0)
            {
                sql += " WHERE " + colNames[0] + " " + operations[0] + " '" + colValues[0] + "' ";
                for (int i = 1; i < colNames.Length; i++)
                {
                    sql += " AND " + colNames[i] + " " + operations[i] + " '" + colValues[i] + "' ";
                }
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        SqlDataAdapter mda = new SqlDataAdapter(cmd);
                        mda.Fill(dt);
                        foreach (DataRow item in dt.Rows)
                        {
                            string value = item[0].ToString();
                            if (!list.Contains(value))
                            {
                                list.Add(value);
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog("读取表" + tableName + "失败," + ex.Message);
            }
            return list;
        }

        /// <summary>
        /// 保存DataTable中的数据到数据库
        /// </summary>
        /// <param name="dataTable">数据</param>
        /// <param name="tableName">数据表</param>
        public static void SaveDataTableToDB(DataTable dataTable, string tableName)
        {
            List<string> columns = new List<string>();
            List<string> types = new List<string>();

            DeleteTable(tableName);
            columns.Add("id"); 
            types.Add("INT IDENTITY PRIMARY KEY");//types.Add("AUTOINCREMENT");
            foreach (DataColumn dc in dataTable.Columns)
            {
                columns.Add(dc.ColumnName);
                types.Add("VARCHAR(128)");
            }
            CreateTable(tableName, columns.ToArray(), types.ToArray());

            columns.RemoveAt(0);//去掉ID
            int a = dataTable.Rows.Count;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                List<string> values = new List<string>();
                for (int i = 0; i < columns.Count; i++)
                {
                    string value = dataRow[columns[i]].ToString();
                    values.Add(value);
                }
                Insert(tableName, columns.ToArray(), values.ToArray());
            }
        }

        /// <summary>
        /// Reads the table.
        /// </summary>
        /// <returns>The table.</returns>
        /// <param name="tableName">Table name.</param>
        /// <param name="items">Items.</param>
        /// <param name="colNames">Col names.</param>
        /// <param name="operations">Operations.</param>
        /// <param name="colValues">Col values.</param>
        /// select*from stockcount where (重量>库存上限 AND 库存上限!='') OR (重量<库存下限 AND 库存下限!='')
        public static DataTable ReadStockWarnFromTable(string tableName, string[] colNames, string[] operations, string[] colValues, string[] orAnds)
        {
            DataTable dt = new DataTable();
            string connStr = "Data Source = " + Properties.Settings.Default.SQLIP + "," + Properties.Settings.Default.SQLPort
                + "; uid = " + Properties.Settings.Default.SQLUser + "; pwd = " + Properties.Settings.Default.SQLPwd + ";Initial Catalog = " + Common.DBFileName;

            string sql = "SELECT * FROM " + tableName + " WHERE ( " + colNames[0] + " " + operations[0] + " " + colValues[0] + " ";
            for (int i = 1; i < colNames.Length; i++)
            {
                sql += (orAnds[i - 1].Equals("OR") ? " ) " + orAnds[i - 1] + " ( " : orAnds[i - 1] + " ") + colNames[i] + " " + operations[i] + " " + colValues[i] + " ";
            }
            sql += ")";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        SqlDataAdapter mda = new SqlDataAdapter(cmd);
                        mda.Fill(dt);
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteLog("读取表" + tableName + "失败," + ex.Message);
            }

            return dt;
        }
    }
}
