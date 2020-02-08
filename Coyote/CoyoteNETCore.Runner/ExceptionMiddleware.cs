using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Coyote.NETCore
{
    public class ExceptionMiddleware : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;

            // temporary logs :>
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);

            context.Result = new BadRequestObjectResult("Something went terribly wrong");
        }
    }
}
