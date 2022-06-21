using Microsoft.AspNetCore.Mvc;

namespace AspIdentityApp.Controllers
{
    [Route("api/authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {      
        private readonly IAuthService _authService;

        public AuthenticateController(IAuthService authService)
        {         
            _authService = authService;
        }

        /// <returns>LoginResponse object</returns>
        /// <response code="200">Returns LoginResponse object</response>
        /// <response code="400">If ModelState is invalid</response>
        /// <response code="401">If user not found or password is wrong</response>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(401, Type = typeof(string))]
        public async Task<ActionResult<LoginResponse>> Login(LoginModel model)
        {
            var loginResult = await _authService.LoginAsync(model);

            if (loginResult is null)
                return Unauthorized("User not found or password is wrong");            

            return Ok(loginResult);
        }

        /// <returns>Response object</returns>
        /// <response code="201">Returns Response object</response>
        /// <response code="400">If ModelState is invalid</response>
        /// <response code="500">If user creation failed</response>
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(500, Type = typeof(Response))]
        public async Task<ActionResult<Response>> Register(RegisterModel model)
        {
            var registerResult = await _authService.RegisterAsync(model);

            if (registerResult.Status.Equals("Error"))
                return StatusCode(StatusCodes.Status500InternalServerError, registerResult);
            
            return StatusCode(StatusCodes.Status201Created, registerResult);
        }
    }
}
