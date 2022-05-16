using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using Npgsql;
using System.Text.RegularExpressions;
using MISA.Infrastructure.Helpers;
using System.Data;

namespace MISA.Infrastructure.Respository
{
    public class EmployeeRepository : BaseRespository<Employee>, IEmployeeRepository
    {
        
        #region Methods
        /// <summary>
        /// thực hiện lấy dữ liệu phân trang cho employee
        /// </summary>
        /// <param name="currentPage">trang hiện tại</param>
        /// <param name="pageSize">số bản ghi/trang</param>
        /// <param name="filterText"></param>
        /// <returns>
        /// count: tổng số bản ghi phù hợp với kq tìm kiếm;
        /// list: danh sách nhân viên đã được filter
        /// </returns>
        public object Filters(int currentPage, int pageSize, string? filterText)
        {
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlPostgreslqString);
            //set option cho filter
            //vị trí bắt đầu lấy
            var offset = (currentPage - 1) * pageSize;
            //số bản ghi lấy
            var limit = pageSize;
            //lấy dữ liệu
            string sqlCommand = "SELECT * FROM Employee AS e"
                + " INNER JOIN Department d ON e.DepartmentId = d.DepartmentId"
                + " WHERE e.EmployeeName LIKE @m_FilterText OR"
                + " e.EmployeeCode LIKE @m_FilterText OR e.PhoneNumber LIKE @m_FilterText"
                + " ORDER BY e.ModifyDate DESC LIMIT @m_offset,@m_limit;"
                + " Select Count(1) FROM Employee AS e"
                + " WHERE e.EmployeeName LIKE @m_FilterText OR e.EmployeeCode LIKE @m_FilterText OR"
                + " e.EmployeeCode LIKE @m_FilterText OR e.PhoneNumber LIKE @m_FilterText";
            //truyền param
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add("@m_FilterText", filterText == null ? "%%" : $"%{filterText}%");
            dynamicParam.Add("@m_offset", offset);
            dynamicParam.Add("@m_limit", limit);
            //dữ liệu trả về gồm các propperty của Employee
            //truy vấn trả về kq cho 2 câu lệnh sql
            var data = sqlConnection.QueryMultiple(sql: sqlCommand, param: dynamicParam);
            //lấy kết qua r lần lượt theo truy vấn
            var list = data.Read<Employee>();
            var totalRecord = data.Read<int>().Single();
            var result = new
            {
                //danh sách nhân viên trả về
                list = list,
                //tổng số bản ghi phù hợp
                totalRecord = totalRecord,
                //số trang 
                totalPage = (totalRecord % limit == 0 ? totalRecord / limit : totalRecord / limit + 1),
            };
            //trả về kết quả
            return result;
        }
        /// <summary>
        /// Lấy toàn bộ nhân viên
        /// </summary>
        /// <returns>
        /// Danh sách nhân viên 
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public IEnumerable<Employee> Gets()
        {
            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlPostgreslqString);
            //lấy dữ liệu
            string sqlCommand = "SELECT *"
            + " FROM Employee as e"
            + " INNER JOIN Department ON e.DepartmentId = Department.DepartmentId";
            //dữ liệu trả về gồm các propperty của Employee
            //và thêm các property: DeparmentId,DeparmentCode,PositionCode,PositionId
            var data = sqlConnection.Query<Employee>(sql: sqlCommand);
            //trả về kết quả
            return data;
        }
        /// <summary>
        /// Lấy nhân viên theo mã id
        /// </summary>
        /// <param name="id">mã id nhân viên</param>
        /// <returns>
        /// nhân viên tìm được/null
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public Employee GetByIds(Guid id)
        {


            //khởi tạo kết nối
            var sqlConnection = new MySqlConnection(_sqlPostgreslqString);
            //lấy dữ liệu
            string sqlCommand = "SELECT *"
            + " FROM Employee as e"
            + " INNER JOIN Department ON e.DepartmentId = Department.DepartmentId"
            + " WHERE EmployeeId = @EmployeeId";
            //khởi tạo tham số
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add("@EmployeeId", id);
            //dữ liệu trả về gồm các propperty của Employee
            //và thêm các property: DeparmentId,DeparmentCode,PositionCode,PositionId
            var data = sqlConnection.Query<Employee>(sql: sqlCommand, param: dynamicParam).FirstOrDefault();
            //trả về kết quả
            return data;
        }


        public object testPostgre()
        {
            
            return null;
        }
        #endregion
    }
}
