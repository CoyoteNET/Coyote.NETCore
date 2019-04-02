using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoyoteNETCore.Controllers
{
    public class DefaultController : Controller
    {
        protected readonly IMediator _med;

        public DefaultController(IMediator m)
        {
            _med = m;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
    }
}
    
