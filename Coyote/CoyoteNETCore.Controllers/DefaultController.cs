using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using CoyoteNETCore.Application;
using CoyoteNETCore.Shared.ResultHandling;

namespace CoyoteNETCore.Controllers
{
    public class DefaultController : Controller
    {
        protected readonly IMediator Mediator;

        public DefaultController(IMediator mediator)
        {
            Mediator = mediator;
        }
            
        protected IActionResult CreateResponse<T>(Result<T> result, IActionResult actionSucceeded)
        {
            return !result.IsSucceeded ? HandleError(result.Error) : actionSucceeded;
        }

        private IActionResult HandleError(Error error)
        {
            switch (error.ErrorType)
            {
                case ErrorType.NotFound:
                    return NotFound(error.ErrorMessage);
                default:
                    return BadRequest(error.ErrorMessage);
            }
        }
    }
}