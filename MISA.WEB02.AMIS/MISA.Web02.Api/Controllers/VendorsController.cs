using Microsoft.AspNetCore.Mvc;
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
    public class VendorsController : MisaBaseController<Vendor>
    {

        #region Fields
        private IVendorService _vendorService;

        #endregion
        #region ApiController
        public VendorsController(IVendorService vendorService) : base(vendorService)
        {
            this._vendorService = vendorService;
        }
        
        [HttpGet("NewVendorCode")]
        public IActionResult GetNewCode()
        {
            try
            {
                //trả về danh sách đã được filter và tổng số bản ghi
                var result = _vendorService.GetNewCodeService();
                return Ok(result);
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
