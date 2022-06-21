using Microsoft.AspNetCore.Mvc;

namespace AspIdentityApp.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        /// <returns>Welcome home</returns>
        /// <response code="200">Returns welcome string</response>
        [HttpGet]       
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            return Ok("Welcome home");
        }
    }
}
