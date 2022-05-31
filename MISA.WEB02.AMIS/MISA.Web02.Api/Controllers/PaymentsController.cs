using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Exceptions;
using MISA.Web02.Core.Interfaces.Base;
using MISA.Web02.Core.Interfaces.Service;
using MISA.Web02.Core.Resourses;

namespace MISA.Web02.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentsController : MisaBaseController<Payment>
    {
        private IPaymentService _paymentService;
        public PaymentsController(IPaymentService paymentService) : base(paymentService)
        {
            _paymentService = paymentService;
        }
        
        public override IActionResult Post(Payment payment)
        {
            try
            {
                var result = _paymentService.AddPayment(payment);
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
        /// <summary>
        /// Lấy số phiếu chi mới nhất
        /// </summary>
        /// <returns>
        /// Trả về số mới nhất
        /// </returns>
        /// Author: Nguyễn Đức Toán-MF1095 (13/04/2022)
        [HttpGet("NewPaymentCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                dynamic s = new
                {
                    res = 11,
                    res1 = 12
                };
                var k = s.res;
                //lấy hậu tố lớn nhất trong bảng employee
                var data = _paymentService.GetNewCodeService();
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
    }
}
