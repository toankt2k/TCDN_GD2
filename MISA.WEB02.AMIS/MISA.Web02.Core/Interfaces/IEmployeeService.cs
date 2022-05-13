using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Interfaces
{
    /// <summary>
    /// cũng cấp các interface xử lý các nghiệp vụ cho các dihcj vụ
    /// </summary>
    public interface IEmployeeService:IBaseService<Employee>
    {
        /// <summary>
        /// phân trang, tìm kiếm
        /// </summary>
        /// <param name="currentPage">page hiện tại</param>
        /// <param name="pageSize">số bản ghi/1page</param>
        /// <param name="filterText">tên/mã nhân viên cần tìm kiếm</param>
        /// <returns>
        /// count: tổng số bản ghi phù hợp với kq tìm kiếm
        /// list danh sách nhân viên đã được filter
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095(15/04/2022)
        public object FilterService(int currentPage, int pageSize, string? filterText);
        /// <summary>
        /// xuất danh sách nhân viên sang file excel
        /// </summary>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095(21/04/2022)
        public byte[] ExportEmployee();
    }
}
