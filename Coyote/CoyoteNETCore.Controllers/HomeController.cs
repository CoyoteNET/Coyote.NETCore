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
    }
}