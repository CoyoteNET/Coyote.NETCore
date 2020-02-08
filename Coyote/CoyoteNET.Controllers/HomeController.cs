using CoyoteNET.Application.Threads.Commands;
using CoyoteNET.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoyoteNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : DefaultController
    {
        public HomeController(IMediator mediator) : base(mediator)
        {
        }
    }
}