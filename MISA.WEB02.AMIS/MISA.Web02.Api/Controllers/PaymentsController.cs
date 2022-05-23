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
    }
}
