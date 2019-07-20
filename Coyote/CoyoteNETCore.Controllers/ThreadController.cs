using CoyoteNETCore.Application.Threads.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoyoteNETCore.Controllers
{
    [Route("api/v1/[controller]")]
    public class ThreadController : DefaultController
    {
        public ThreadController(IMediator m) : base(m)
        {

        }

        [HttpGet("Thread")]
        public IActionResult GetThread()
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
            
            var result = await Mediator.Send(data);

            return Json(new { });
        }
    }
}