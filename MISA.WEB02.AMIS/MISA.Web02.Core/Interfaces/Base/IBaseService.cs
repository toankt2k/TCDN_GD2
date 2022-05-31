using MISA.Web02.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Interfaces.Base
{
    /// <summary>
    /// cũng cấp các interface xử lý các nghiệp vụ cho các dihcj vụ
    /// </summary>
    public interface IBaseService<T>
    {
        /// <summary>
        /// lấy tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetService();
        /// <summary>
        /// lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetByIdService(Guid id);
        /// <summary>
        /// lất mã code mới nhất
        /// </summary>
        /// <returns></returns>
        public string GetNewCodeService();
        /// <summary>
        /// thêm mới bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public int InsertService(T entity);
        /// <summary>
        /// sửa bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public int UpdateService(T entity, Guid id);
        /// <summary>
        /// xóa bản ghi
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public int DeleteService(Guid entityId);
        /// <summary>
        /// xoas nhiều bản ghi trong bảng
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        public int MultiDelete(List<Guid> listId);

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
        public string FilterService(int currentPage, int pageSize, string? filterText);

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
        public byte[] ExportService(int currentPage, int pageSize, string? filterText, List<TableInfo> columns);


    }
}
