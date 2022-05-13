using MISA.Web02.Core.Entities;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Helpers.ExportExcel
{
    /// <summary>
    /// xuất dữ liêu jra excel sử dụng EPPlus
    /// </summary>
    public class EmployeeListEPP
    {
        /// <summary>
        /// xuất dữ liệu bảng employee
        /// </summary>
        /// <param name="employeeList"></param>
        /// <returns></returns>
        public static byte[] ExportEmployeeList(IEnumerable<Employee> employeeList)
        {
            using (var excelPackage = new ExcelPackage(new FileInfo("C:\\Users\\toank\\toan2k.xlsx")))
            {
                // Tạo title cho file Excel
                excelPackage.Workbook.Properties.Title = "Danh sách nhân viên";
                //thêm 1 sheet để làm việc với tệp excel
                excelPackage.Workbook.Worksheets.Add("Danh sách nhân viên");
                ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets[0];
                //TIÊU đề của sheet
                workSheet.Cells["A1:K1"].Merge = true;//nối cottj
                workSheet.Cells["A1:K1"].Value = "DANH SÁCH NHÂN VIÊN";//title của sheet
                workSheet.Cells["A1:K1"].Style.Font.Size = 18;//font size
                workSheet.Cells["A2:K2"].Merge = true;//nối cột
                //header cho bảng
                workSheet.Cells["A3"].Value = "STT";
                workSheet.Cells["B3"].Value = "Mã nhân viên";
                workSheet.Cells["C3"].Value = "Tên nhân viên";
                workSheet.Cells["D3"].Value = "Giới tính";
                workSheet.Cells["E3"].Value = "Ngày sinh";
                workSheet.Cells["F3"].Value = "ĐT di động";
                workSheet.Cells["G3"].Value = "Chức danh";
                workSheet.Cells["H3"].Value = "Tên đơn vị";
                workSheet.Cells["I3"].Value = "Số tài khoản";
                workSheet.Cells["J3"].Value = "Tên ngân hàng";
                workSheet.Cells["K3"].Value = "Chi nhánh";
                

                int count = 0;//stt
                int index = 3;//vị trí bắt đầu của dữ liệu
                //thêm các sheet chi tiết
                foreach (var item in employeeList)
                {
                    count++;
                    index++;
                    //stt
                    workSheet.Cells[index, 1].Value = count;
                    //mã Nhân viên
                    workSheet.Cells[index, 2].Value = item.EmployeeCode==null?"":item.EmployeeCode.Trim();
                    //tên nhân viên
                    workSheet.Cells[index, 3].Value = item.EmployeeName==null?"":item.EmployeeName.Trim();
                    //giới tính
                    workSheet.Cells[index, 4].Value = item.GenderName==null?"":item.GenderName.Trim();
                    //ngày sinh
                    workSheet.Cells[index, 5].Value = item.DateOfBirth == null ? "" : ((DateTime)item.DateOfBirth).ToString("dd/MM/yyyy");
                    //đt di động
                    workSheet.Cells[index, 6].Value = item.PhoneNumber==null?"":item.PhoneNumber.Trim();
                    //chức danh
                    workSheet.Cells[index, 7].Value = item.PositionName==null?"":item.PositionName.Trim();
                    //tên đơn vị
                    workSheet.Cells[index, 8].Value = item.DepartmentName==null?"":item.DepartmentName.Trim();
                    //số tài khoản
                    workSheet.Cells[index, 9].Value = item.BankAccount==null?"":item.BankAccount.Trim();
                    //tên ngân hàng
                    workSheet.Cells[index, 10].Value = item.BankName==null?"":item.BankName.Trim();
                    //chi nhánh
                    workSheet.Cells[index, 11].Value = item.BankBranch==null?"":item.BankBranch.Trim();

                }
                //format file
                workSheet.Cells[$"A1:K{index}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[$"A1:K{index}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[$"A1:K{index}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[$"A1:K{index}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                //fomat dữ liệu
                workSheet.Cells[$"A1:K{index}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;//căn trái
                workSheet.Column(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//  căn giữa
                //set căn giữa hàng cho các cột
                workSheet.Cells[$"A1:K{index}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;//căn giữa dòng
                //thêm dữ liệu các cột
                workSheet.Cells["A3:K3"].Style.Font.Bold = true;//In đậm
                workSheet.Cells["A3:K3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["A3:K3"].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#BBB"));//background-color
                workSheet.Row(3).Height = 20;//độ rộng của cột header
                workSheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;//căn giữa chiều dọc tiêu dề
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//căn giữa chiều ngang tiêu đề
                //tự động dãn cột
                workSheet.Cells.AutoFitColumns();

                //return file
                var file = excelPackage.GetAsByteArray();
                //dừng ile excel
                excelPackage.Dispose();
                return file;
            }
        }
    }
}
