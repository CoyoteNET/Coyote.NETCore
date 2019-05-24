using CoyoteNETCore.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CoyoteNETCore.Controllers
{
    public class DefaultController : Controller
    {
        protected readonly IMediator _med;

        public DefaultController(IMediator mediator)
        {
            _med = mediator;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
    }
}
    
