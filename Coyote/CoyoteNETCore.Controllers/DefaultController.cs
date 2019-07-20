﻿using CoyoteNETCore.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
﻿using System;
using CoyoteNETCore.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
                case ErrorType.BadRequest:
                case ErrorType.AlreadyExists:
                    return BadRequest(error.Description);
                default:
                    return BadRequest(error.Description);
            }
        }
    }
}
    
