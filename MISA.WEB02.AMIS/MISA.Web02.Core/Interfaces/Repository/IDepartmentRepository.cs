using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Interfaces.Repository
{
    public interface IDepartmentRepository:IBaseRepository<Department>
    {
        #region cũ k dùng
        ///// <summary>
        ///// Lấy tất cả phòng ban
        ///// </summary>
        ///// <returns>
        ///// danh sách phòng ban
        ///// </returns>
        ///// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        //public IEnumerable<Department> Get();
        ///// <summary>
        ///// Lấy phòng ban theo id
        ///// </summary>
        ///// <param name="id">id phòng ban</param>
        ///// <returns>
        ///// phòng ban lấy được
        ///// </returns>
        ///// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        //public Department GetById(Guid id);
        ///// <summary>
        ///// thêm mới phòng ban
        ///// </summary>
        ///// <param name="department"></param>
        ///// <returns>
        ///// số hàng ảnh hưởng
        ///// </returns>
        ///// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        //public int Insert(Department department);
        ///// <summary>
        ///// thay đổi thông tin phòng ban
        ///// </summary>
        ///// <param name="department"></param>
        ///// <param name="departmentId"></param>
        ///// <returns>
        ///// số hàng bị ảnh hưởng
        ///// </returns>
        ///// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        //public int Update(Department department);
        ///// <summary>
        ///// xóa 1 phòng ban trong database
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        ///// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        //public int Delete(Guid id);
        ///// <summary>
        ///// lấy mã phòng ban mới nhất 
        ///// </summary>
        ///// <returns></returns>
        //public string GetNewCode();
        ///// <summary>
        ///// Tìm phòng ban có mã phòng ban trùng với mã được truyền vào
        ///// </summary>
        ///// <param name="departmentCode"></param>
        ///// <returns></returns>
        ///// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        //public Department FindByCode(string departmentCode);
        #endregion
    }
}
