using Dapper;
using MISA.Infrastructure.Helpers;
using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Interfaces.Base;
using MySqlConnector;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Respository
{
    /// <summary>
    /// kết nối database và thực thi các câu lệnh sql dùng chung cho các thực thể
    /// </summary>
    /// <typeparam name="T"> class truyền vào</typeparam>
    public class BaseRespository<T> : IBaseRepository<T>
    {
        #region Fields
        public string _sqlPostgreslqString;

        #endregion
        #region Constructor
        public BaseRespository()
        {
            //_sqlConnectionString = "Host=3.0.89.182;Port=3306;Database=MISA.WEB02.NDTOAN;User Id=dev;Password=12345678";
            _sqlPostgreslqString = "Host=140.238.38.197;Port=5432;Database=MISA_WEB02_AMIS;User Id=admin;Password=Anhtoankt12@";
            //_sqlConnectionString = "Host=localhost;Port=3306;Database=MISA.WEB02.NDTOAN;User Id=root;Password=''";
        }
        #endregion
        #region Methods
        /// <summary>
        /// Lấy toàn bộ dữ liệu trong bảng
        /// </summary>
        /// <returns>
        /// Danh sách dữ liệu
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public virtual IEnumerable<T> Get()
        {
            var entityName = typeof(T).Name;
            string commandText = $"SELECT * FROM {BindingEntity.ToSnakeCase(entityName)}";
            //dữ liệu trả về gồm các propperty của T
            var data = BindingEntity.Query<T>(commandText, _sqlPostgreslqString, CommandType.Text);
            //trả về kết quả
            return data.Result;
        }

        /// <summary>
        /// Lấy mã đối tượng mới nhất trong hệ thống
        /// </summary>
        /// <returns>Mã đối tượng mới nhất trong hệ thống</returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public virtual string GetNewCode()
        {
            //table name snake
            var tableName = BindingEntity.ToSnakeCase(typeof(T).Name);
            //sql query
            var commandText = $"SELECT e.{tableName}_code FROM {tableName} as e";
            //trả về danh sách các code với các key và value
            var data = BindingEntity.Query<string>(commandText, _sqlPostgreslqString, CommandType.Text);
            var listEntityCode = data.Result;
            //danh sách code khi tách 2 chữ đầu
            var listCode = new List<int>();
            //xóa hết các kí tự là chữ trong code
            string pattern = @"\D";
            for (int i = 0; i < listEntityCode.Count(); i++)
            {
                var code = listEntityCode.ElementAt(i);
                listCode.Add(int.Parse(Regex.Replace(code.ToString(), pattern, "")));
            }
            var newCode = $"1";
            if (listCode.Count > 0)
            {
                newCode = $"{listCode.Max() + 1}";
            }
            return newCode;
        }

        /// <summary>
        /// Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// <returns>
        /// trả về số hàng bị ảnh hưởng
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public virtual int Insert(T entity)
        {
            //lấy tên của entity
            var entityName = typeof(T).Name;
            //lấy id mới nhất
            //lấy property Id của entity
            var propId = typeof(T).GetProperty($"{typeof(T).Name}Id");
            //set giá trị cho prop id
            propId.SetValue(entity, Guid.NewGuid());
            //set commandText gọi function
            string commandText = $"func_insert_{BindingEntity.ToSnakeCase(entityName)}";
            //tạo tham số
            var param = new List<NpgsqlParameter>(); 
            //lấy danh sách property của entity
            var listProperties = typeof(T).GetProperties();
            foreach (var prop in listProperties)
            {
                //chuyển qua snake case
                var tableColunm = BindingEntity.ToSnakeCase(prop.Name);
                //xem prop có cho phép map vào database không
                var isNotMap = prop.IsDefined(typeof(NotMap), false);
                var value = entity.GetType().GetProperty(prop.Name).GetValue(entity);
                if (!isNotMap)
                {
                    param.Add(new NpgsqlParameter($"@m_{tableColunm}", value == null ? DBNull.Value : value.ToString()));
                }
            }
            var res = BindingEntity.Execute(commandText,_sqlPostgreslqString,CommandType.StoredProcedure,param);
            //trả về kết quả
            return res.Result;
        }

        /// <summary>
        /// cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="employeeId"></param>
        /// <returns>số hàng bị ảnh hưởng</returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public virtual int Update(T entity)
        {
            var entityName = typeof(T).Name;
            //sử dụng func update
            string commandText = $"func_update_{BindingEntity.ToSnakeCase(entityName)}";
            //tạo tham số
            var param = new List<NpgsqlParameter>();
            //lấy danh sách property của entity
            var listProperties = typeof(T).GetProperties();
            foreach (var prop in listProperties)
            {
                //chuyển qua snake case
                var tableColunm = BindingEntity.ToSnakeCase(prop.Name);
                //xem prop có cho phép map vào database không
                var isNotMap = prop.IsDefined(typeof(NotMap), false);
                if (!isNotMap)
                {
                    //giá trị mới được truyền vào
                    var value = entity.GetType().GetProperty(prop.Name).GetValue(entity);
                    param.Add(new NpgsqlParameter($"@m_{tableColunm}", value == null ? DBNull.Value : value.ToString()));
                }
            }

            //update
            var res = BindingEntity.Execute(commandText,_sqlPostgreslqString,CommandType.StoredProcedure,param);
            //trả về kết quả
            return res.Result;
        }

        /// <summary>
        /// Lấy nhân viên theo mã id
        /// </summary>
        /// <param name="id">mã id nhân viên</param>
        /// <returns>
        /// nhân viên tìm được/null
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public virtual T GetById(Guid id)
        {
            var tableName = BindingEntity.ToSnakeCase(typeof(T).Name);
            //lấy dữ liệu
            string commandText = $"SELECT * FROM {tableName} as e WHERE {tableName}_id = @m_{tableName}_id";
            //khởi tạo tham số
            var param = new List<NpgsqlParameter>();
            param.Add(new NpgsqlParameter($"@m_{tableName}_id", id.ToString()));
            //dữ liệu trả về gồm các propperty của T
            var data = BindingEntity.Query<T>(commandText, _sqlPostgreslqString, CommandType.Text, param);
            var result = data.Result.FirstOrDefault();
            //trả về kết quả
            return result;
        }

        /// <summary>
        /// Xóa 1 bản ghi với id truyền vào
        /// </summary>
        /// <param name="id">mã id nhân viên</param>
        /// <returns>
        /// số hàng bị ảnh hưởng
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public virtual int Delete(Guid id)
        {
            //table name snake
            var tableName = BindingEntity.ToSnakeCase(typeof(T).Name);
            //lấy dữ liệu
            string commandText = $"WITH deleted AS (DELETE FROM {tableName} WHERE {tableName}_id = @m_{tableName}_id IS TRUE RETURNING *) SELECT count(*) FROM deleted;";
            //khởi tạo tham số
            var param = new List<NpgsqlParameter>();
            param.Add(new NpgsqlParameter($"@m_{tableName}_id", id.ToString()));
            //dữ liệu trả về số hàng được xóa
            var data = BindingEntity.Execute(commandText, _sqlPostgreslqString,CommandType.Text,param);
            var result = data.Result;
            //trả về kết quả
            return result;
        }

        /// <summary>
        /// xóa nhiều bản ghi trong database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public virtual int MultiDelete(List<Guid> listId)
        {
            //table name dang snake
            var tableName = BindingEntity.ToSnakeCase(typeof(T).Name);
            //khởi tạo kết nối
            var sqlConnection = new NpgsqlConnection(_sqlPostgreslqString);
            //comamnd text delete nhiều
            string commandText = "";
            //ds nhân viên cần xóa
            var listParam = "";
            //khởi tạo tham số, sinh chuỗi tham số
            var param = new List<NpgsqlParameter>();
            //thêm param và tạo tham số commandtext
            for (int i = 0; i < listId.Count; i++)
            {
                param.Add(new NpgsqlParameter($"@id{i}", listId.ElementAt(i).ToString()));
                listParam += $"@id{i}" + ",";
            }
            listParam = listParam.Substring(0, listParam.Length - 1).Trim();
            commandText = $"WITH deleted AS (DELETE FROM {tableName} WHERE {tableName}_id IN ({listParam}) IS TRUE RETURNING *) SELECT count(*) FROM deleted;";
            //dữ liệu trả về gồm các propperty của Employee
            var data = BindingEntity.Execute(commandText, _sqlPostgreslqString, CommandType.Text, param);
            var res = data.Result;
            //trả về kết quả
            return res;
        }

        /// <summary>
        /// tìm bản ghi có code trùng vói code truyền vào
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual T FindByCode(string code)
        {
            //tableNAme dạng snake case
            var tableName = typeof(T).Name;
            //lấy dữ liệu
            string commandText = $"SELECT * FROM {tableName} WHERE {tableName}_code = @m_{tableName}_code";
            //khởi tạo tham số
            var param = new List<NpgsqlParameter>();
            param.Add(new NpgsqlParameter($"@m_{tableName}_code", code.ToString()));
            //dữ liệu trả về thông tin của đối tượng
            var data = BindingEntity.Query<T>(commandText, _sqlPostgreslqString, CommandType.Text, param);
            var res = data.Result.FirstOrDefault();
            //trả về kết quả
            return res;
        }

        /// <summary>
        /// trả về chuỗi json gồm số bản ghi và danh sách kết quả
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="filterText"></param>
        /// <returns></returns>
        public string Filter(int currentPage, int pageSize, string? filterText)
        {
            var tableName = BindingEntity.ToSnakeCase(typeof(T).Name);
            //khởi tạo kết nối
            var sqlConnection = new NpgsqlConnection(_sqlPostgreslqString);
            //set option cho filter
            //vị trí bắt đầu lấy
            var offset = (currentPage - 1) * pageSize;
            //số bản ghi lấy
            var limit = pageSize;
            //lấy dữ liệu
            string commandText = $"func_filter_{tableName}";
            //truyền param
            var param = new List<NpgsqlParameter>();
            param.Add(new NpgsqlParameter("@m_filter_text", filterText == null ? "" : filterText));
            param.Add(new NpgsqlParameter("@m_limit", limit.ToString()));
            param.Add(new NpgsqlParameter("@m_offset", offset.ToString()));
            sqlConnection.Open();
            //dữ liệu trả về gồm các propperty của Employee
            //truy vấn trả về kq cho 2 câu lệnh sql
            var data = BindingEntity.Query<string>(commandText, _sqlPostgreslqString, CommandType.StoredProcedure,param);

            //trả về kết quả
            return data.Result.FirstOrDefault();
        }
        #endregion
    }
}
