using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Exceptions;
using MISA.Web02.Core.Helpers;
using MISA.Web02.Core.Helpers.ExportExcel;
using MISA.Web02.Core.Interfaces.Base;
using MISA.Web02.Core.Resourses;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Services
{
    public class BaseService<T> : IBaseService<T>
    {
        #region Fields
        private IBaseRepository<T> _baseRepository;
        #endregion
        #region Constructor
        public BaseService(IBaseRepository<T> tiem)
        {
            this._baseRepository = tiem;
        }
        #endregion
        /// <summary>
        /// xóa 1 bản ghi
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public virtual int DeleteService(Guid entityId)
        {
            var entityName = typeof(T).Name;
            //validate dữ liệu
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //kiểm tra bản ghi đã tồn tại
            var entityInDatabase = _baseRepository.GetById(entityId);
            if (entityInDatabase == null)//nếu bản ghi không tồn tại tức là bản ghi đã bị xóa trước đó
            {
                //thêm lỗi vào errorMsg
                errorMsg.Add($"{entityName}Empty", MISAMessageResource.VN_DataNotFound);
            }
            if (errorMsg.Count() > 0)
            {
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }
            var result = _baseRepository.Delete(entityId);
            return result;

        }
        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public virtual int InsertService(T entity)
        {
            var entityName = typeof(T).Name;
            //validate dữ liệu
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //validate dữ liệu trống
            var validateEmptyResult = ValidateEmpty(entity);
            if (validateEmptyResult.Count() > 0)
            {
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }
            //thêm mới database
            var result = _baseRepository.Insert(entity);
            return result;
        }
        /// <summary>
        /// sửa 1 bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán - MF1095 (14/04/2022)
        public virtual int UpdateService(T entity, Guid id)
        {
            
            var entityName = typeof(T).Name;
            //gán lại id cho entity mới
            typeof(T).GetProperty($"{entityName}Id").SetValue(entity,id);
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //validate dữ liệu trống
            var validateEmptyResult = ValidateEmpty(entity);
            if (validateEmptyResult.Count() > 0)//nếu dữ liệu bị trống
            {
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }
            //lấy code của entity truyền vào
            var newCode = typeof(T).GetProperty($"{entityName}Code").GetValue(entity).ToString();
            ////code đúng format
            //var rx = new Regex(@"[a-zA-Z]{2}-[0-9]+$");
            //if (!rx.IsMatch(newCode.ToUpper()))
            //{
            //    errorMsg.Add($"{entityName}Code", MISAMessageResource.VN_CodeInvalid);
            //}
            //lấy giá trị cũ trong data base
            var oldEntity = _baseRepository.GetById(id);
            //nếu không tìm thấy bản ghi / bản ghi đã bị xóa trước khi sửa 
            if (oldEntity == null)
            {
                errorMsg.Add($"{entityName}NotFound", MISAMessageResource.VN_DataNotFound);
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }
            else
            {
                //lấy mã code của đối tượng đã có trong database
                var oldCode = typeof(T).GetProperty($"{entityName}Code").GetValue(oldEntity).ToString();
                if (oldCode != newCode)//nếu bằng mã mới có nghĩa khô sửa code
                {
                    var existCode = _baseRepository.FindByCode(newCode);
                    if(existCode != null)//nếu khác null có nghĩa mã code đã tồn tại
                    {
                        //thêm 1 lỗi vào danh sách lỗi
                        errorMsg.Add($"{entityName}Code", MISAMessageResource.VN_CodeDuplicate);
                        //throw exception
                        throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
                    }
                    
                }
            }
            if (errorMsg.Count() > 0)
            {
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }
            //set lại data
            //lấy các prop trong entity
            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                //lấy giá trị của entity đó
                var entityValue = prop.GetValue(entity);
                //lấy giá trị của data
                var dataValue = prop.GetValue(oldEntity);
                //nếu các trường không được nhập vào thì người dùng không muốn thay đổi
                //nên giữ nguyên giá trị cũ trong database
                if (entityValue == null)
                {
                    prop.SetValue(entity, dataValue);
                }
            }
            // gọi update đến repository
            var result = _baseRepository.Update(entity);
            return result;
        }

        /// <summary>
        /// xóa nhiều bản ghi trong bảng
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095(15/04/2022)
        public virtual int MultiDelete(List<Guid> listId)
        {
            var result = _baseRepository.MultiDelete(listId);
            return result;
        }

        /// <summary>
        /// kiểm tra các trường required bị trống
        /// </summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095(15/04/2022)
        protected Dictionary<string, string> ValidateEmpty(T entity)
        {
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //kiểm tra các trường required có bị null không
            //lấy các prop trong entity
            var props = typeof(T).GetProperties();
            foreach (var prop in props)
            {
                //kiểm tra xem có được định nghĩa là required không
                var isRequired = prop.IsDefined(typeof(MISARequired), false);
                if (isRequired)
                {
                    //lấy giá trị của prop đó
                    var propValue = prop.GetValue(entity);
                    //kiểm tra propValue null
                    if (propValue == null)
                    {
                        //lấy các thuộc tính được hiển thị hiển thị của prop
                        var displayAttribute = (MISADisplay)Attribute.GetCustomAttribute(prop, typeof(MISADisplay));
                        errorMsg.Add($"{prop.Name}", $"{displayAttribute.Name} {MISAMessageResource.VN_NotEmptyMsg}");

                    }
                    //kiểm tra là chuỗi rỗng
                    else if (string.IsNullOrEmpty(propValue.ToString()))
                    {
                        //lấy các thuộc tính được hiển thị hiển thị của prop
                        var displayName = (MISADisplay)Attribute.GetCustomAttribute(prop, typeof(MISADisplay));
                        errorMsg.Add($"{prop.Name}", $"{displayName.Name} {MISAMessageResource.VN_NotEmptyMsg}");

                    }
                }
            }
            return errorMsg;
        }

        public virtual IEnumerable<T> GetService()
        {
            var result = _baseRepository.Get();
            return result;
        }

        public virtual T GetByIdService(Guid id)
        {
            var result = _baseRepository.GetById(id);
            return result;
        }
        public virtual string GetNewCodeService()
        {
            var result = _baseRepository.GetNewCode();
            return result;
        }

        public string FilterService(int currentPage, int pageSize, string? filterText)
        {
            var result = _baseRepository.Filter(currentPage, pageSize,filterText);
            return result;
        }

        public byte[] ExportService(int currentPage, int pageSize, string? filterText, List<TableInfo> columns)
        {
            var res = _baseRepository.Filter(currentPage, pageSize, filterText);
            //đổi về dang json
            var jObj = JObject.Parse(res);
            //đổi về dạng pascalcase
            jObj.Capitalize();
            //Convert từ json về object
            var data = JsonSerializer.Deserialize<FilterResult<T>>(jObj.ToString());
            //gọi hàm xuất dữ liệu
            var result = BaseExportEPP.Export<T>(data.List, columns);
            return result;
        }
    }
}
