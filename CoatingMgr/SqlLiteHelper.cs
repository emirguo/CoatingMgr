using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace CoatingMgr
{
    class SqlLiteHelper
    {
        public static SqlLiteHelper mInstance = null;

        /// <summary>
        /// 数据库连接定义
        /// </summary>
        public SQLiteConnection dbConnection;

        /// <summary>
        /// SQL命令定义
        /// </summary>
        public SQLiteCommand dbCommand;

        /// <summary>
        /// 数据读取定义
        /// </summary>
        public SQLiteDataReader dataReader;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlLiteHelper()
        {
            if (dbConnection == null)           
            {
                try
                {
                    dbConnection = new SQLiteConnection("data source=" + Common.DBPath);
                    dbConnection.Open();
                }
                catch (Exception e)
                {
                    Log(e.ToString());
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接SQLite库字符串</param>
        public SqlLiteHelper(string connectionString)
        {
            if (dbConnection == null)
            {
                try
                {
                    dbConnection = new SQLiteConnection(connectionString);
                    dbConnection.Open();
                }
                catch (Exception e)
                {
                    Log(e.ToString());
                }
            }
        }

        public static SqlLiteHelper GetInstance()
        {
            if (mInstance == null)
            {

                mInstance = new SqlLiteHelper();
            }

            return mInstance;
        }

        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <returns>The query.</returns>
        /// <param name="queryString">SQL命令字符串</param>
        public SQLiteDataReader ExecuteQuery(string queryString)
        {
            try
            {
                dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = queryString;
                dataReader = dbCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                Log(e.Message);
            }

            return dataReader;
            
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseConnection()
        {
            //销毁Commend
            if (dbCommand != null)
            {
                dbCommand.Cancel();
            }
            dbCommand = null;
            //销毁Reader
            if (dataReader != null)
            {
                dataReader.Close();
            }
            dataReader = null;
            //销毁Connection
            if (dbConnection != null)
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
            dbConnection = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

        }

        /// <summary>
        /// 读取整张数据表
        /// </summary>
        /// <returns>The full table.</returns>
        /// <param name="tableName">数据表名称</param>
        public SQLiteDataReader ReadFullTable(string tableName)
        {
            string queryString = "SELECT * FROM " + tableName;
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 向指定数据表中插入数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="values">插入的数值</param>
        public SQLiteDataReader InsertValues(string tableName, string[] values)
        {
            string queryString = "INSERT INTO " + tableName + " VALUES ( null, '" + values[0] + "'";
            for (int i = 1; i < values.Length; i++)
            {
                queryString += ", " + "'" + values[i] + "'";
            }
            queryString += " )";
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 向指定数据表中插入数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="values">插入的数值</param>
        public SQLiteDataReader InsertValues(string tableName, List<string> values)
        {
            string queryString = "INSERT INTO " + tableName + " VALUES ('" + values[0] + "'";
            for (int i = 1; i < values.Count; i++)
            {
                queryString += ", " + "'" + values[i] + "'";
            }
            queryString += " )";
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 更新指定数据表内的数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        /// <param name="key">关键字</param>
        /// <param name="value">关键字对应的值</param>
        /// <param name="operation">运算符：=,<,>,...，默认“=”</param>
        public SQLiteDataReader UpdateValues(string tableName, string[] colNames, string[] colValues, string key, string value, string operation = "=")
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length");
            }

            string queryString = "UPDATE " + tableName + " SET " + colNames[0] + "=" + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += ", " + colNames[i] + "=" + "'" + colValues[i] + "'";
            }
            queryString += " WHERE " + key + operation + "'" + value + "'";
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 删除指定数据表内的数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        public SQLiteDataReader DeleteValuesOR(string tableName, string[] colNames, string[] colValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
            }

            string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += "OR " + colNames[i] + operations[0] + "'" + colValues[i] + "'";
            }
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 删除指定数据表内的数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        public SQLiteDataReader DeleteValuesAND(string tableName, string[] colNames, string[] colValues, string[] operations)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
            {
                throw new SQLiteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
            }

            string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + "'" + colValues[0] + "'";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += " AND " + colNames[i] + operations[i] + "'" + colValues[i] + "'";
            }
            return ExecuteQuery(queryString);
        }


        /// <summary>
        /// 创建数据表
        /// </summary> +
        /// <returns>The table.</returns>
        /// <param name="tableName">数据表名</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colTypes">字段名类型</param>   
        public SQLiteDataReader CreateTable(string tableName, string[] colNames, string[] colTypes)
        {
            string queryString = "CREATE TABLE IF NOT EXISTS " + tableName + "( " + colNames[0] + " " + colTypes[0];
            for (int i = 1; i < colNames.Length; i++)
            {
                queryString += ", " + colNames[i] + " " + colTypes[i];
            }
            queryString += "  ) ";
            return ExecuteQuery(queryString);
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
        public SQLiteDataReader ReadTable(string tableName, string[] items, string[] colNames, string[] operations, string[] colValues)
        {
            string queryString = "SELECT " + items[0];
            for (int i = 1; i < items.Length; i++)
            {
                queryString += ", " + items[i];
            }
            queryString += " FROM " + tableName + " WHERE " + colNames[0] + " " + operations[0] + " '" + colValues[0]+ "'";
            for (int i = 1; i < colNames.Length; i++)
            {
                queryString += " AND " + colNames[i] + " " + operations[i] + " '" + colValues[0] + "' ";
            }
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 向指定数据表中插入另一张表的数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tagTable">目标数据表</param>
        /// <param name="sourceTable">源数据表</param>
        ///insert into log select * from sourcestock;
        public SQLiteDataReader InsertDataFromOtherTable(string tagTable, string sourceTable)
        {
            string queryString = "INSERT INTO " + tagTable + " SELECT * FROM " + sourceTable;
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 向指定数据表中插入另一张表的数据，并重新生成ID
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tagTable">目标数据表</param>
        /// <param name="sourceTable">源数据表</param>
        ///insert into stockmanager select null,条形码,名称,颜色,类型,标准重量,适用机型,生产日期,有效期,仓库名称,操作员,操作时间,操作类型,告警类型,备注 from instock
        public SQLiteDataReader InsertDataWithoutIdFromOtherTable(string tagTable, string sourceTable)
        {
            string queryString = "INSERT INTO " + tagTable + " SELECT NULL, 条形码, 名称, 颜色, 类型, 标准重量, 适用机型, 生产日期, 有效期, 仓库名称, 操作员, 操作时间, 操作类型, 告警类型, 备注 FROM " + sourceTable;
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 清除表内所有数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="TableName">数据表</param>
        ///DELETE FROM TableName;
        public SQLiteDataReader ClearTable(string tableName)
        {
            string queryString = "DELETE FROM " + tableName;
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 请表ID归0
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="TableName">数据表</param>
        ///DELETE FROM sqlite_sequence WHERE name = ‘TableName’;
        public SQLiteDataReader ResetTableId(string tableName)
        {
            string queryString = "DELETE FROM sqlite_sequence WHERE name = '" + tableName + "'";
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 获取某个字段所有不同值的数据
        /// </summary>
        /// <returns>list<string>.</returns>
        /// <param name="TableName">数据表</param>
        /// <param name="column">字段</param>
        ///SELECT column FROM TableName;
        public List<string> GetValueTypeByColumnFromTable(string tableName, string column)
        {
            List<string> list = new List<string>();
            string queryString = "SELECT " + column + " FROM " + tableName;
            SQLiteDataReader dataReader = ExecuteQuery(queryString);
            while (dataReader.Read())
            {
                string value = dataReader.GetValue(0).ToString();
                if (!list.Contains(value))
                {
                    list.Add(value);
                }
            }
            return list;
        }

        /// <summary>
        /// 保存DataTable中的数据到数据库
        /// </summary>
        /// <returns>Boolean</returns>
        /// <param name="tableName">数据表</param>
        /// <param name="dt">数据</param>
        ///SELECT column FROM TableName;
        public void SaveDataTableToDB(DataTable dataTable, string tableName)
        {
            List<string> columnsName = new List<string>();
            List<string> columnsType = new List<string>();
            foreach (DataColumn dc in dataTable.Columns)
            {
                columnsName.Add(dc.ColumnName);
                columnsType.Add("TEXT");
            }

            CreateTable(tableName, columnsName.ToArray(), columnsType.ToArray());
            foreach (DataRow dataRow in dataTable.Rows)
            {
                List<string> values = new List<string>();
                for (int i = 0; i < columnsName.Count; i++)
                {
                    string value = dataRow[columnsName[i]].ToString();
                    values.Add(value);
                }
                InsertValues(tableName, values);
            }
        }


        /// <summary>
        /// 本类log
        /// </summary>
        /// <param name="s"></param>
        static void Log(string s)
        {
            Console.WriteLine("class SqLiteHelper:::" + s);
        }
        
    }
}
