using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Interfaces.Repository
{
    public interface IEmployeeRepository:IBaseRepository<Employee>
    {
        #region cũ, không dùng
        ///// <summary>
        ///// Lấy tất cả nhân viên
        ///// </summary>
        ///// <returns>
        ///// danh sách nhân viên
        ///// </returns>
        ///// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        //public IEnumerable<Employee> Get();
        ///// <summary>
        ///// Lấy nhân viên theo id
        ///// </summary>
        ///// <param name="id">id nhân viên</param>
        ///// <returns>
        ///// nhân viên lấy được
        ///// </returns>
        ///// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        //public Employee GetById(Guid id);
        ///// <summary>
        ///// thêm mới nhân viên
        ///// </summary>
        ///// <param name="employee"></param>
        ///// <returns>
        ///// số hàng ảnh hưởng
        ///// </returns>
        ///// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        //public int Insert(Employee employee);
        ///// <summary>
        ///// thay đổi thông tin nhân viên
        ///// </summary>
        ///// <param name="employee"></param>
        ///// <param name="employeeId"></param>
        ///// <returns>
        ///// số hàng bị ảnh hưởng
        ///// </returns>
        ///// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        //public int Update(Employee employee);
        ///// <summary>
        ///// xóa 1 nhân viên trong database
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        ///// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        //public int Delete(Guid id);
        ///// <summary>
        ///// lấy mã nhân viên mới nhất 
        ///// </summary>
        ///// <returns></returns>
        //public string GetNewCode();
        ///// <summary>
        ///// Tìm nhân viên có mã nhân viên trùng với mã được truyền vào
        ///// </summary>
        ///// <param name="employeeCode"></param>
        ///// <returns></returns>
        ///// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        //public Employee FindByCode(string employeeCode);
        #endregion

    }
}
