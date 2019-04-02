using CoyoteNETCore.Application.Thread.Command;
using CoyoteNETCore.DAL;
using CoyoteNETCore.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
            var command = new CreateThreadCommand("Test", "test", new User());

            var output = await _med.Send(command);
            return StatusCode(200, new { output.Success, output.Result });
        }
    }
}