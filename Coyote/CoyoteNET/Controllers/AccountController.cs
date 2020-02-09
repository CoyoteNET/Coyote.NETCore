using System.Threading.Tasks;
using CoyoteNET.Application.Account.Commands;
using CoyoteNET.Shared.RequestInput;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CoyoteNET.Controllers
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
            var result = await Mediator.Send(new RegisterUserCommand(input.Username, input.Email, input.Password));

            return CreateResponse(result, Ok(result.Value));
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> Login([FromBody]LoginInput input)
        {
            var result = await Mediator.Send(new LoginUserCommand(input.Username, input.Password));

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