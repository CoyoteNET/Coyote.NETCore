using CoyoteNETCore.Application.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoyoteNETCore.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class RegisterController : DefaultController
    {
        public RegisterController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public IActionResult Register([FromBody]RegisterUserCommand command)
        {
            Mediator.Send(command);

            return NoContent();
        }

        
    }
}