using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Interfaces.Base;
using MISA.Web02.Core.Interfaces.Service;

namespace MISA.Web02.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountObjectsController : MisaBaseController<AccountObject>
    {
        public AccountObjectsController(IAccountObjectService accountObjectService) : base(accountObjectService)
        {
        }
    }
}
