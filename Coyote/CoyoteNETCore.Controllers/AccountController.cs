using System.Threading.Tasks;
using CoyoteNETCore.Application.Account.Commands;
using CoyoteNETCore.Shared.RequestInput;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CoyoteNETCore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : DefaultController
    {
        public AccountController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterInput input)
        {
            if (input == null)
                return BadRequest("The received data is broken");

            var command = new RegisterUserCommand(input?.Username, input?.Email, input?.Password);

            var result = await Mediator.Send(command);

            return CreateResponse(result, Ok(result.Value));
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> Login([FromBody]LoginInput input)
        {
            if (input == null)
                return BadRequest("The received data is broken");

            var command = new LoginUserCommand(input?.Name, input?.Password);

            var result = await Mediator.Send(command);

            return CreateResponse(result, Json(result.Value));
        }

        [HttpGet("GitHub/LogIn")]
        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, "GitHub");
        }

        [HttpGet("LogOff")]
        public IActionResult LogOff()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" });
        }
    }
}