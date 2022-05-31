using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Resourses;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Helpers.ExportExcel
{
    public class BaseExportEPP
    {
        /// <summary>
        /// xuất dữ liệu file ex cel theo tên cột và dan gsacsh dữ liệu truyền vào
        /// </summary>
        /// <typeparam name="T">class tương ứng với dữ liệu</typeparam>
        /// <param name="data">danh sách dữ liệu</param>
        /// <param name="columns">thông tin các cột cần xuất</param>
        /// <returns></returns>
        public static byte[] Export<T>(List<T> data, List<TableInfo> columns)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var excelPackage = new ExcelPackage(new FileInfo("C:\\Users\\toank\\toan2k.xlsx")))
            {
                // Tạo title cho file Excel
                excelPackage.Workbook.Properties.Title = "Danh sách";
                //thêm 1 sheet để làm việc với tệp excel
                excelPackage.Workbook.Worksheets.Add("Danh sách");
                ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets[0];
                int row = 1;
                int col = 1;
                BuildHeader(columns, workSheet, ref row, col);
                row += 1;
                BuildData<T>(columns, workSheet, data, ref row, col);
                col = columns.Count;
                //tự động dãn cột
                workSheet.Cells.AutoFitColumns();
                //return file
                var file = excelPackage.GetAsByteArray();
                //dừng ile excel
                excelPackage.Dispose();
                return file;
            }
        }
        /// <summary>
        /// build header cho worksheet
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="workSheet"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private static void BuildHeader(List<TableInfo> columns, ExcelWorksheet workSheet, ref int row, int col)
        {
            //header cho bảng
            foreach (var column in columns)
            {
                if (column.Align == "center")
                {
                    workSheet.Column(col).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//  căn giữa
                }
                if (column.Align == "right")
                {
                    workSheet.Column(col).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;//  căn phải
                }
                if (column.Align == "left")
                {
                    workSheet.Column(col).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;//  căn trái
                }
                workSheet.Cells[row, col].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[row, col].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[row, col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[row, col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[row, col].Value = column.Name;
                workSheet.Cells[row, col].Style.Font.Bold = true;//In đậm
                workSheet.Cells[row, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[row, col].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#BBB"));//background-color
                col++;
            }
            workSheet.Row(row).Height = 20;
        }
        /// <summary>
        /// build data cho worksheet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columns"></param>
        /// <param name="workSheet"></param>
        /// <param name="data"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private static void BuildData<T>(List<TableInfo> columns, ExcelWorksheet workSheet, List<T> data, ref int row, int col)
        {
            //header cho bảng
            foreach (var item in data)
            {
                foreach (var column in columns)
                {
                    workSheet.Cells[row, col].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[row, col].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[row, col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[row, col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    var prop = item.GetType().GetProperty(column.Key);
                    if (prop == null) continue;
                    var valueProp = prop.GetValue(item);
                    if (valueProp == null)
                    {
                        workSheet.Cells[row, col].Value = "";
                    }
                    else
                    {
                        if (valueProp.GetType() == typeof(DateTime))
                        {
                            workSheet.Cells[row, col].Value = ((DateTime)valueProp).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            workSheet.Cells[row, col].Value = valueProp;
                        }
                        if (valueProp.GetType() == typeof(float))
                        {
                            workSheet.Cells[row, col].Style.Numberformat.Format = "#,##0.00";
                        }
                    }
                    
                    col++;
                }
                row++;
                col = 1;
            }

        }
    }
}
