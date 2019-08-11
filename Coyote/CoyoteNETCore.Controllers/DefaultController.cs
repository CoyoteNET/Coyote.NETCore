using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using CoyoteNETCore.Application;

namespace CoyoteNETCore.Controllers
{
    public class DefaultController : Controller
    {
        protected readonly IMediator Mediator;

        public DefaultController(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected IActionResult CreateResponse<T>(Result<T> result, Func<IActionResult> actionSucceeded)
        {
            return !result.IsSucceeded ? HandleError(result.Error) : actionSucceeded();
        }

        private IActionResult HandleError(Error error)
        {
            switch (error.ErrorType)
            {
                case ErrorType.NotFound:
                    return NotFound(error.Description);
                default:
                    return BadRequest(error.Description);
            }
        }
    }
}
    
