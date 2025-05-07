using Ecom.API.Errors;
using Ecom.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public BugsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("not-found")]
        public IActionResult GetNotFound()
        {
            var product = _dataContext.Products.Find(Guid.Empty);
            if (product == null)
            {
                return  NotFound(new BaseCommonResponse(404));
            }
            return Ok(product);
        }


        [HttpGet("server-error")]
        public IActionResult GetServerError()
        {
            var product = _dataContext.Products.Find(Guid.Empty);
            product.Name = "Server Error";
            return Ok();
        }     
        
        [HttpGet("bad-request/{id}")]
        public ActionResult<string> GetNotFoundRequest(int id)
        {
            return Ok("Okay done successfully");
        }

        [HttpGet("bad-request")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new BaseCommonResponse(400));
        }
    }
}
