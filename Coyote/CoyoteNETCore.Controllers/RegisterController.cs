using System.Threading.Tasks;
using CoyoteNETCore.Application.Account.Commands;
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
        public async Task<IActionResult> Register([FromBody]RegisterUserCommand command)
        {
            var (success, result) = await Mediator.Send(command);

            return StatusCode(200, new {success, result});
        }       
    }
}