using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Infrastructure.Respository;
using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Exceptions;
using MISA.Web02.Core.Interfaces;
using MISA.Web02.Core.Interfaces.Repository;
using MISA.Web02.Core.Interfaces.Service;
using MISA.Web02.Core.Resourses;

namespace MISA.Web02.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : MisaBaseController<Employee>
    {
        #region Fields
        private IEmployeeService _employeeService;

        #endregion
        #region ApiController
        public EmployeesController(IEmployeeService employeeService, IEmployeeRepository employeeRepository) :base(employeeService)
        {
            this._employeeService = employeeService;
        }

        /// <summary>
        /// Lấy mã nhân viên mơi nhất
        /// </summary>
        /// <returns>
        /// Trả về mã nhân viên mới nhất
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                //lấy hậu tố lớn nhất trong bảng employee
                var data = _employeeService.GetNewCodeService();
                return Ok(data);
            }
            catch (MISAExceptions ex)
            {
                var result = new MISAServiceResult()
                {
                    devMsg = ex.Message,
                    userMsg = MISAMessageResource.VN_MisaExceptionMsg,
                    data = ex.Data,
                };
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = MISAMessageResource.VN_MisaExceptionMsg,
                    data = ex.Data,
                };
                return StatusCode(500, result);
            }
        }

        

        /// <summary>
        /// xuất danh sách tất cả nhân viên ra file excel
        /// </summary>
        /// <returns></returns>
        /// Author: Nguyễn Đức Toán-MF1095(24/04/2022)
        [HttpGet("export")]
        public IActionResult Export()
        {
            try
            {
                //trả về danh sách đã được filter và tổng số bản ghi
                var file = _employeeService.ExportEmployee();
                return File(file, "xlsx/xls", "Danh_sach_nhan_vien.xlsx");
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = MISAMessageResource.VN_MisaExceptionMsg,
                    data = ex.Data,
                };
                return StatusCode(500, result);
            }
        }
        #endregion

    }
}
