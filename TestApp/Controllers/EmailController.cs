using Microsoft.AspNetCore.Mvc;
using TestApp.Core;

namespace TestApp.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSenderService _emailSenderService;

        public EmailController(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        /// <returns>Send emailMessage to recipient</returns>
        /// <response code="200">Returns the succeed message</response>
        /// <response code="400">If ModelState is invalid or some troubles</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        public async Task<ActionResult<string>> Post(EmailMessage message)
        {
            var result = await _emailSenderService.SendEmailAsync(message);

            if (result)
            {
                Log.Information($"Message send to: {message.Recipient} action was succeeded");
                return Ok($"Message send to: { message.Recipient}");
            }

            Log.Warning($"Message send to: {message.Recipient} action was failed");
            return BadRequest($"Message send to: {message.Recipient} was failed");
        }
    }
}
