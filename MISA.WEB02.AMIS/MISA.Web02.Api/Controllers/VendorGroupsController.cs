using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web02.Core.Entities;
using MISA.Web02.Core.Interfaces.Base;
using MISA.Web02.Core.Interfaces.Service;

namespace MISA.Web02.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VendorGroupsController : MisaBaseController<VendorGroup>
    {
        public VendorGroupsController(IVendorGroupService vendorGroupService) : base(vendorGroupService)
        {
        }

        public override IActionResult MultiDelete([FromBody] List<Guid> listId)
        {
            return NotFound();
        }

        public override IActionResult Post([FromBody] VendorGroup entity)
        {
            return NotFound();
        }

        public override IActionResult Put([FromRoute] Guid id, [FromBody] VendorGroup entity)
        {
            return NotFound();
        }
    }
}
