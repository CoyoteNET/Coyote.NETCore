using CoyoteNETCore.Application.Threads.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoyoteNETCore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HomeController : DefaultController
    {
        public HomeController(IMediator m) : base(m)
        {

        }

        [HttpGet("Thread")]
        public async Task<IActionResult> GetThread()
        {
            return Json(new { });
        }

        [HttpPost("Thread")]
        public async Task<IActionResult> CreateThread([FromBody] CreateThreadCommand data)
        {       
            if (int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out var id))
            {
                data.AuthorId = id;
            }
            else
            {
                return BadRequest("Unable to determine User's profile");
            }
            
            var result = await _mediator.Send(data);

            return Json(new { });
        }
    }
}