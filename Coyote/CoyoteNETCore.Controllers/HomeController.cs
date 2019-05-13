using CoyoteNETCore.Application.Thread.Command;
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
        public HomeController(IMediator m) : base(m)
        {
        }

        [HttpGet("SampleEndpoint")]
        public async Task<IActionResult> SampleEndpoint()
        {
            // tests
            var command = new CreateThreadCommand("Test", "test", new User("_", "_", "_", "_"));

            var output = await _med.Send(command);
            return StatusCode(200, new { output.Success, output.Result });
        }
    }
}