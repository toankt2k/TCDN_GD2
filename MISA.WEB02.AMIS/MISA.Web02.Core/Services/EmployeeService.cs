using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Exceptions;
using MISA.Web02.Core.Helpers.ExportExcel;
using MISA.Web02.Core.Interfaces.Repository;
using MISA.Web02.Core.Interfaces.Service;
using MISA.Web02.Core.Resourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Web02.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Fields
        IEmployeeRepository _employeeRepository;
        #endregion
        #region Contructor
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        public override int InsertService(Employee employee)
        {
            //validate dữ liệu
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //validate dữ liệu trống
            var validateEmptyResult = ValidateEmpty(employee);
            if (validateEmptyResult.Count() > 0)
            {
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, validateEmptyResult);
            }
            //lấy code của entity truyền vào
            var code = typeof(Employee).GetProperty($"EmployeeCode").GetValue(employee).ToString();
            //code đúng format
            var rx = new Regex(@"^[a-zA-Z]{2}-[0-9]{5}$");
            if (!rx.IsMatch(code.ToUpper()))
            {
                errorMsg.Add($"EmployeeCode", MISAMessageResource.VN_CodeInvalid);
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }
            //kiểm tra mã code trùng
            var data = _employeeRepository.FindByCode(code);
            //nếu có bản ghi trùng với mã hiện tại
            if (data != null)
            {
                //lấy thuộc tính hiển thị của employeeCode
                var employeeCodeDisplay = (MISADisplay)Attribute.GetCustomAttribute(typeof(Employee).GetProperty("EmployeeCode"), typeof(MISADisplay));
                errorMsg.Add($"EmployeeCode", $"{employeeCodeDisplay.Name} <{code}> {MISAMessageResource.VN_DataDuplicate}");

            }
            if (errorMsg.Count() > 0)
            {
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }
            //format lại dữ liệu cho employee
            employee.EmployeeName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(employee.EmployeeName);//viết hoa mỗi chữ đầu trong tên
            employee.EmployeeCode = employee.EmployeeCode.ToUpper();//viết hoa tất cả chữ cái trong EmployeeCode
            //thêm mới database
            return _employeeRepository.Insert(employee);
        }

        /// <summary>
        /// sửa 1 bản ghi
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán - MF1095 (14/04/2022)
        public int UpdateServicea(Employee employee, Guid id)
        {
            Dictionary<string, string> errorMsg = new Dictionary<string, string>();
            //validate dữ liệu trống
            var validateEmptyResult = ValidateEmpty(employee);
            foreach (var item in validateEmptyResult)
            {
                errorMsg.Add(item.Key, item.Value);
            }
            //lấy code của entity truyền vào
            var code = typeof(Employee).GetProperty($"EmployeeCode").GetValue(employee).ToString();
            //code đúng format
            var rx = new Regex(@"^[a-zA-Z]{2}-[0-9]{5}$");
            if (!rx.IsMatch(code.ToUpper()))
            {
                errorMsg.Add($"EmployeeCode", MISAMessageResource.VN_CodeInvalid);
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }
            //lấy Employee cũ trong data base
            var data = _employeeRepository.GetById(id);
            //nếu không tìm thấy bản ghi / bản ghi đã bị xóa trước khi sửa 
            if (data == null)
            {
                errorMsg.Add($"EmployeeNotFound", MISAMessageResource.VN_DataNotFound);
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }
            //kiểm tra mã bị trùng nếu người dùng sửa mã
            if (data.EmployeeCode != employee.EmployeeCode)
            {
                //kiểm tra mã code trùng
                var employeeByCode = _employeeRepository.FindByCode(code);
                //nếu có bản ghi trùng với mã hiện tại
                if (employeeByCode != null)
                {
                    //lấy thuộc tính hiển thị của employeeCode
                    var employeeCodeDisplay = (MISADisplay)Attribute.GetCustomAttribute(typeof(Employee).GetProperty("EmployeeCode"), typeof(MISADisplay));
                    errorMsg.Add($"EmployeeCode", $"{employeeCodeDisplay.Name} <{code}> {MISAMessageResource.VN_DataDuplicate}");
                }
            }
            if (errorMsg.Count() > 0)
            {
                throw new MISAExceptions(MISAMessageResource.VN_DataInvalid, errorMsg);
            }
            //set lại data
            //lấy các prop trong entity
            var props = typeof(Employee).GetProperties();
            foreach (var prop in props)
            {
                //lấy giá trị của entity đó
                var entityValue = prop.GetValue(employee);
                //lấy giá trị của data
                var dataValue = prop.GetValue(employee);
                if (entityValue == null)
                {
                    prop.SetValue(employee, dataValue);
                }
            }
            //format lại dữ liệu cho employee
            employee.EmployeeName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(employee.EmployeeName);//viết hoa mỗi chữ đầu trong tên
            employee.EmployeeCode = employee.EmployeeCode.ToUpper();//viết hoa tất cả chữ cái trong EmployeeCode
            return _employeeRepository.Update(employee);
        }

        /// <summary>
        /// phân trang, tìm kiếm
        /// </summary>
        /// <param name="currentPage">page hiện tại</param>
        /// <param name="pageSize">số bản ghi/1page</param>
        /// <param name="filterText">tên/mã nhân viên cần tìm kiếm</param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095(19/04/2022)
        public object FilterService(int currentPage, int pageSize, string? filterText)
        {
            var result = _employeeRepository.Filter(currentPage, pageSize, filterText);
            return result;
        }

        /// <summary>
        /// lấy 1 nhân viên theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095(19/04/2022)
        public override Employee GetByIdService(Guid id)
        {
            return _employeeRepository.GetById(id);
        }
        
        /// <summary>
        /// lấy tất cả nhân viên
        /// </summary>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095(19/04/2022)
        public override IEnumerable<Employee> GetService()
        {
            var result = _employeeRepository.Get();
            return result;
        }

        /// <summary>
        /// lấy mã mới nhất của nhân viên
        /// </summary>
        /// <returns>
        /// chuỗi 5 chữ số
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095(19/04/2022)
        public override string GetNewCodeService()
        {
            var result = $"NV-{base.GetNewCodeService().PadLeft(5, '0')}";
            return result;
        }

        /// <summary>
        /// xuất danh sách tất cả nhân viên ra file excel
        /// </summary>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095(25/04/2022)
        public byte[] ExportEmployee()
        {
            var listEmployee = _employeeRepository.Get();
            var result = EmployeeListEPP.ExportEmployeeList(listEmployee);
            return result;
        }
        #endregion
    }
}
