using System.Threading.Tasks;
using CoyoteNETCore.Application.Account.Commands;
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
        public async Task<IActionResult> Register([FromBody]RegisterUserCommand command)
        {
            var (success, result) = await Mediator.Send(command);

            return StatusCode(200, new { success, result });
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> Login([FromBody]LoginUserCommand command)
        {
            var (success, result) = await Mediator.Send(command);

            return StatusCode(200, new { success, result });
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