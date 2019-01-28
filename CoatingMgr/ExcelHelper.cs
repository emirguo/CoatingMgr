using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoatingMgr
{
    class ExcelHelper
    {
        public static Stream RenderDataTableToExcel(DataTable SourceTable)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();
            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);

            // handling header. 
            foreach (DataColumn column in SourceTable.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            // handling value. 
            int rowIndex = 1;

            foreach (DataRow row in SourceTable.Rows)
            {
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);

                foreach (DataColumn column in SourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }

                rowIndex++;
            }

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            sheet = null;
            headerRow = null;
            workbook = null;

            return ms;
        }
        public static DataTable RenderDataTableFromExcel(Stream ExcelFileStream, int SheetIndex, int HeaderRowIndex)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
            HSSFSheet sheet = (HSSFSheet)workbook.GetSheetAt(SheetIndex);

            DataTable table = new DataTable();

            HSSFRow headerRow = (HSSFRow)sheet.GetRow(HeaderRowIndex);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            int rowCount = sheet.LastRowNum;

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)

                        dataRow[j] = row.GetCell(j).ToString();
                }

                table.Rows.Add(dataRow);
            }

            ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        public static void ExportExcel(DataTable dt)
        {
            MemoryStream ms = ExcelHelper.RenderDataTableToExcel(dt) as MemoryStream;

            string saveFileName = "";
            bool fileSaved = false;
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                DefaultExt = "xls",
                Filter = "Excel文件|*.xls",
                FileName = ""
            };
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消
            if (saveFileName != "")
            {
                try
                {
                    FileStream fs = new FileStream(saveDialog.FileName, FileMode.Create);
                    fs.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                    ms.Close();
                    ms.Dispose();
                    fs.Close();
                    fileSaved = true;
                }
                catch (Exception ex)
                {
                    fileSaved = false;
                    MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                }
            }
            else
            {
                fileSaved = false;
            }
            GC.Collect();//强行销毁
            if (fileSaved && File.Exists(saveFileName))
            {
                MessageBox.Show("导出成功！", "通知");
            }
            else
            {
                MessageBox.Show("导出失败！", "通知");
            }
        }

        public static DataTable ImportExcel()
        {
            DataTable dt = null;
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Excel文件|*.xls"
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = fileDialog.FileName;//得到文件所在位置。
                dt = RenderDataTableFromExcel(File.OpenRead(fileName), 0, 0);
            }
            return dt; 
        }
    }
}
