using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;

namespace MarketingAutomationTool.Utilities
{
    public class ExcelUtility
    {
        public bool WriteDataTableToExcel(Guid UserId, DataTable sourceTable, string worksheetName, string saveAsLocation, string ReportType)
        {

            try
            {
                Microsoft.Office.Interop.Excel.Application excel;
                Microsoft.Office.Interop.Excel.Workbook excelworkBook;
                Microsoft.Office.Interop.Excel.Worksheet excelSheet;
                Microsoft.Office.Interop.Excel.Range excelCellrange;


                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;
                excelworkBook = excel.Workbooks.Add(Type.Missing);
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                excelSheet.Name = "Export";

                int iRowIndex = 0;
                int iColIndex = 0;

                foreach (DataRow dr in sourceTable.Rows)
                {
                    iRowIndex++;
                    foreach (DataColumn dc in sourceTable.Columns)
                    {
                        if (IncludeColumn(dc.ColumnName))
                        {
                            iColIndex++;
                            if (iRowIndex == 1)
                            {
                                excelSheet.Cells[iRowIndex, iColIndex] = dc.ColumnName;
                                excelSheet.Cells[iRowIndex + 1, iColIndex] = dr[dc.ColumnName].ToString();
                            }
                            else
                            {
                                excelSheet.Cells[iRowIndex, iColIndex] = dr[dc.ColumnName].ToString();
                            }
                        }
                        
                    }

                    iColIndex = 0;
                    if (iRowIndex == 1)
                    {
                        iRowIndex++;
                    }
                }


                string sColumnsToExclude = ConstantValues.LeadsExportColumnsToExclude;
                string[] sColumnsToExcludeList = sColumnsToExclude.Split('|');

                //Autofit contents to cells
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[sourceTable.Rows.Count+1, sourceTable.Columns.Count - sColumnsToExcludeList.Length]];
                excelCellrange.EntireColumn.AutoFit();
                Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border.Weight = 2d;

                //Format header (column names)
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[1, sourceTable.Columns.Count - sColumnsToExcludeList.Length]];
                string sExportExcelHeaderColor= ConfigurationManager.AppSettings["ExportExcelHeaderColor"].ToString().Trim();
                FormattingExcelCells(excelCellrange, sExportExcelHeaderColor, System.Drawing.Color.White, true);

                excelworkBook.SaveAs(saveAsLocation);
                excelworkBook.Close();
                excel.Quit();

                return true;
            }
            catch(Exception ex)
            {
                Logger.Logger.WriteLog(UserId.ToString(), "ExcelUtility-WriteDataTableToExcel", string.Empty, "Error", ex.Message);
                return false;
            }
        }

        private void FormattingExcelCells(Microsoft.Office.Interop.Excel.Range range, string HTMLcolorCode, System.Drawing.Color fontColor, bool IsFontbool)
        {
            range.Interior.Color = System.Drawing.ColorTranslator.FromHtml(HTMLcolorCode);
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(fontColor);
            if (IsFontbool == true)
            {
                range.Font.Bold = IsFontbool;
            }
        }

        private bool IncludeColumn(string columname)
        {
            string sColumnsToExclude = ConstantValues.LeadsExportColumnsToExclude;
            string[] sColumnsToExcludeList= sColumnsToExclude.Split('|');

            foreach(string colName in sColumnsToExcludeList)
            {
                if (columname.ToUpper().Trim() == colName.ToUpper().Trim())
                    return false;
            }

            return true;
        }
    }
}