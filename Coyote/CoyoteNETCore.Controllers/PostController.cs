using CoyoteNETCore.Application.Threads.Commands;
using CoyoteNETCore.Shared.RequestInput;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoyoteNETCore.Controllers
{
    [Route("api/v1/[controller]")]
    public class PostController : DefaultController
    {
        public PostController(IMediator m) : base(m)
        {
        }

        [HttpPost]
        public async Task<IActionResult> WritePost([FromBody] WritePostInput data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data == null)
            {
                return BadRequest("Received data is broken.");
            }

            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id))
            {
                return BadRequest("Unable to determine User's profile");
            }

            var command = new WritePostCommand(data.ThreadId.Value, data.Body, id);

            var result = await Mediator.Send(command);

            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> EditPost([FromRoute] EditPostInput data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (data == null)
            {
                return BadRequest("Received data is broken.");
            }

            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id))
            {
                return BadRequest("Unable to determine User's profile");
            }

            var command = new EditPostCommand(data.PostId.Value, data.Body, id);

            var result = await Mediator.Send(command);

            return Json(result);
        }
    }
}