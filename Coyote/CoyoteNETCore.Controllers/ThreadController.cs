using CoyoteNETCore.Application.Threads.Commands;
using CoyoteNETCore.Shared.RequestInput;
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

        [HttpPost]
        public async Task<IActionResult> CreateThread([FromBody] CreateThread data)
        {
            if (data == null)
            {
                return BadRequest("Received data is broken.");
            }

            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id))
            {
                return BadRequest("Unable to determine User's profile");
            }

            var command = new CreateThreadCommand(data.Body, data.Title, data.ThreadCategoryId, id);

            var result = await Mediator.Send(command);

            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetThread([FromRoute] int? id)
        {
            var query = new GetThreadQuery(id);

            var result = await Mediator.Send(query);

            return Json(result);
        }
    }
}