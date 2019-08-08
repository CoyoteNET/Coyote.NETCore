using CoyoteNETCore.Application.Threads.Commands;
using CoyoteNETCore.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoyoteNETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : DefaultController
    {
        public HomeController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("SampleEndpoint")]
        public async Task<IActionResult> SampleEndpoint()
        {
            // tests
            var command = new CreateThreadCommand("Test", "test", 0, 0);

            var output = await Mediator.Send(command);
            return StatusCode(200, new { output.IsSucceeded, output.Error });
        }
    }
}