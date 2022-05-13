using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Exceptions;
using MISA.Web02.Core.Interfaces;
using MISA.Web02.Core.Resourses;

namespace MISA.Web02.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : MisaBaseController<Department>
    {
        #region Fields
        private IDepartmentService _departmentService;
        //private IEmployeeService _employeeService;

        #endregion
        #region ApiController
        public DepartmentsController(IDepartmentService departmentService) :base(departmentService)
        {
            this._departmentService = departmentService;
        }
        /// <summary>
        /// Lấy mã nhân viên mơi nhất
        /// </summary>
        /// <returns>
        /// Trả về mã nhân viên mới nhất
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        [HttpGet("NewDepartmentCode")]
        public IActionResult GetNewDepartmentCode()
        {
            try
            {
                //lấy hậu tố lớn nhất trong bảng employee
                var data = _departmentService.GetNewCodeService();
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
        #endregion

    }
}
