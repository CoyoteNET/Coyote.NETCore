using CoyoteNET.Application.Threads.Commands;
using CoyoteNET.Shared.RequestInput;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoyoteNET.Controllers
{
    [Route("api/v1/[controller]")]
    public class ThreadController : DefaultController
    {
        public ThreadController(IMediator m) : base(m)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateThread([FromBody] CreateThreadInput data)
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

            var command = new CreateThreadCommand(data.Body, data.Title, data.ThreadCategoryId.Value, data.Tags, id);

            var result = await Mediator.Send(command);

            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetThread([FromRoute] int? id)
        {
            if (id == null)
                return BadRequest("Incorrect Id.");

            var query = new GetThreadQuery(id);

            var result = await Mediator.Send(query);

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetThreads()
        {
            var query = new GetThreadsQuery();

            var result = await Mediator.Send(query);

            return Json(result);
        }
    }
}