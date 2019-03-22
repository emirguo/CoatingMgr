using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace CoatingMgr
{
    class ExcelHelper
    {
        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="table">要导入的数据</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public static MemoryStream RenderDataTableToExcel(string fileName, DataTable table)
        {
            MemoryStream ms = new MemoryStream();
            using (table)
            {
                IWorkbook workbook = new HSSFWorkbook();
                ISheet sheet = workbook.CreateSheet();
                {
                    IRow headerRow = sheet.CreateRow(0);

                    foreach (DataColumn column in table.Columns)
                    {
                        headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);
                    }

                    int rowIndex = 1;

                    foreach (DataRow row in table.Rows)
                    {
                        IRow dataRow = sheet.CreateRow(rowIndex);

                        foreach (DataColumn column in table.Columns)
                        {
                            dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                        }

                        rowIndex++;
                    }

                    workbook.Write(ms);
                    ms.Flush();
                    ms.Position = 0;
                }
                return ms;
            }
        }

        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public static DataTable RenderDataTableFromExcel(string fileName, int SheetIndex, bool isFirstRowColumn)
        {
            if (fileName == null || fileName.Equals(""))
            {
                return null;
            }

            IWorkbook workbook = null;
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.EndsWith(".xlsx"))
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (fileName.EndsWith(".xls"))
                {
                    workbook = new HSSFWorkbook(fs);
                }

                sheet = workbook.GetSheetAt(SheetIndex);
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                            {
                                dataRow[j] = row.GetCell(j).ToString();
                            }
                        }
                        data.Rows.Add(dataRow);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            return data;
        }

        public static void ExportExcel(DataTable dt)
        {
            bool fileSaved = false;
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                DefaultExt = "xls",
                Filter = "Excel文件|*.xls",
                FileName = ""
            };
            saveDialog.ShowDialog();
            string fileName = saveDialog.FileName;
            if (fileName.IndexOf(":") < 0) return; //被点了取消
            if (fileName != null && !fileName.Equals(""))
            {
                try
                {
                    MemoryStream ms = RenderDataTableToExcel(fileName, dt);
                    using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    {
                        byte[] data = ms.ToArray();
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                        data = null;
                    }
                    fileSaved = true;
                }
                catch (Exception e)
                {
                    fileSaved = false;
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                fileSaved = false;
            }
            if (fileSaved && File.Exists(fileName))
            {
                MessageBox.Show("导出成功！", "通知");
            }
            else
            {
                MessageBox.Show("导出失败！", "通知");
            }
        }

        public static void ImportExcel(String fileName)
        {
            DataTable dt = RenderDataTableFromExcel(fileName, 0, true);
            if (dt != null)
            {
                SqlLiteHelper.GetInstance().SaveDataTableToDB(dt, Common.MASTERTABLENAME);
            }
        }

        /// <summary>
        /// 获取当前单元格所在的合并单元格的位置
        /// </summary>
        /// <param name="sheet">sheet表单</param>
        /// <param name="rowIndex">行索引 0开始</param>
        /// <param name="colIndex">列索引 0开始</param>
        /// <param name="start">合并单元格左上角坐标</param>
        /// <param name="end">合并单元格右下角坐标</param>
        /// <returns>返回false表示非合并单元格</returns>
        private static bool IsMergeCell(ISheet sheet, int rowIndex, int colIndex, out Point start, out Point end)
        {
            bool result = false;
            start = new Point(0, 0);
            end = new Point(0, 0);
            if ((rowIndex < 0) || (colIndex < 0)) return result;
            int regionsCount = sheet.NumMergedRegions;
            for (int i = 0; i < regionsCount; i++)
            {
                CellRangeAddress range = sheet.GetMergedRegion(i);
                //sheet.IsMergedRegion(range); 
                if (rowIndex >= range.FirstRow && rowIndex <= range.LastRow && colIndex >= range.FirstColumn && colIndex <= range.LastColumn)
                {
                    start = new Point(range.FirstRow, range.FirstColumn);
                    end = new Point(range.LastRow, range.LastColumn);
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
