using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Interfaces.Service
{
    /// <summary>
    /// cũng cấp các interface xử lý các nghiệp vụ cho các dihcj vụ
    /// </summary>
    public interface IEmployeeService:IBaseService<Employee>
    {
        /// <summary>
        /// xuất danh sách nhân viên sang file excel
        /// </summary>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095(21/04/2022)
        public byte[] ExportEmployee();
    }
}
