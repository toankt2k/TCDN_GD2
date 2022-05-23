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
    public class TableOptionsController : MisaBaseController<TableOption>
    {
        private ITableOptionService _tableOptionService;
        public TableOptionsController(ITableOptionService tableOptionService) : base(tableOptionService)
        {
            _tableOptionService = tableOptionService;
        }

        public override IActionResult Delete(Guid id)
        {
            return NotFound();
        }
        
        /// <summary>
        /// lấy dữ liệu theo mã bảng
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("ByCode")]
        public IActionResult GetByCode(string code)
        {
            try
            {
                var result = _tableOptionService.GetByCode(code);
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
        /// sửa table option bằng code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPut("ByCode")]
        public IActionResult Put(TableOption table)
        {
            try
            {
                var result = _tableOptionService.PutByCode(table);
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

        public override IActionResult Filter(int currentPage, int pageSize, string? filterText)
        {
            return NotFound(); 
        }

        public override IActionResult Get()
        {
            return NotFound();
        }

        public override IActionResult GetById(Guid id)
        {
            return base.GetById(id);
        }

        public override IActionResult MultiDelete([FromBody] List<Guid> listId)
        {
            return NotFound();
        }

        public override IActionResult Post([FromBody] TableOption entity)
        {
            return NotFound();
        }

        public override IActionResult Put([FromRoute] Guid id, [FromBody] TableOption entity)
        {
            return NotFound();
        }
    }
}
