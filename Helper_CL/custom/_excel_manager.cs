using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ClosedXML.Excel;

namespace Helper_CL.custom
{
    public static class _excel_manager
    {
        public static byte[] excel_file(DataTable dt,List<string> hide_column = null)
        {
            using (var workbook = new XLWorkbook())
            {
                // Add a worksheet to the workbook
                var worksheet = workbook.Worksheets.Add("Sheet1");

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cell(1,i+1).Value = dt.Columns[i].ColumnName;
                    worksheet.Column(i+1).Width = 20;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        worksheet.Cell(i + 2,j+ 1).Value = dt.Rows[i][j].ToString();
                    }
                }
                if (hide_column != null && hide_column.Count > 0)
                {
                    foreach (var columnName in hide_column)
                    {
                        var column = dt.Columns[columnName];
                        if (column != null)
                        {
                            worksheet.Column(column.Ordinal + 1).Hide();
                        }
                    }
                }

               

                var rangeWithData = worksheet.RangeUsed();

                // Create an Excel table (ListObject) from the range
                var excelTable = rangeWithData.CreateTable();

                // Set the table name (optional)
                excelTable.Name = "ExcelTable";
                //excelTable.Style.TableStyle = TableTheme.TableStyleLight9;

                // Save the workbook to a file
                //string filePath = "example.xlsx";
                //workbook.SaveAs(filePath);

                // Convert workbook to byte array
                using (var ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    return ms.ToArray();
                }
            }
        }

        public static byte[] export_excel_file(DataTable dt, List<string> hide_column = null)
        {
            using (var workbook = new XLWorkbook())
            {
                // Add a worksheet to the workbook
                var worksheet = workbook.Worksheets.Add("Sheet1");
                worksheet.Cell(1, 1).InsertTable(dt);

                if (hide_column != null && hide_column.Count > 0)
                {
                    foreach (var columnName in hide_column)
                    {
                        var column = worksheet.Columns(columnName);
                        if (column != null)
                        {
                            column.Hide();
                        }
                    }
                }

                var table = worksheet.Tables.FirstOrDefault();
                if (table != null)
                {
                    table.ShowAutoFilter = true;
                    table.Theme = XLTableTheme.TableStyleLight16;
                }

                // Save the workbook to a file
                //string filePath = "example.xlsx";
                //workbook.SaveAs(filePath);

                // Convert workbook to byte array
                using (var ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    return ms.ToArray();
                }
            }
        }

        public static DataTable read_excel_file(Stream stream)
        {
            DataTable dataTable = new DataTable();
            using (var workbook = new XLWorkbook(stream))
            {
                // Assuming the first worksheet in the Excel file
                var worksheet = workbook.Worksheet(1);

                // Headers
                var firstRow = worksheet.FirstRowUsed();
                foreach (var cell in firstRow.Cells())
                {
                    dataTable.Columns.Add(cell.Value.ToString());
                }

                // Data
                var rows = worksheet.RowsUsed().Skip(1); // Skip headers
                foreach (var row in rows)
                {
                    var dataRow = dataTable.NewRow();
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        dataRow[i] = row.Cell(i + 1).Value.ToString();
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }
            return dataTable;
        }
    }
}
