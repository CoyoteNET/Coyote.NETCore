﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoyoteNETCore.Shared.Extensions;

namespace Coyote.NETCore
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // https://exceptionnotfound.net/using-middleware-to-log-requests-and-responses-in-asp-net-core/
            var request = await FormatRequest(context.Request);
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                await _next(context);
                var response = await FormatResponse(context.Response);
                await responseBody.CopyToAsync(originalBodyStream);
                Console.WriteLine(response);
            }
            Console.WriteLine(request);
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableRewind();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Position = 0; // allows framework to process this body once again

            bodyAsText = PreventPasswordsFromBeingLogged(request.Path, bodyAsText);

            return $"{request.Scheme.ToUpper()}/{request.Method} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private static string PreventPasswordsFromBeingLogged(PathString requestPath, string bodyAsText)
        {
            var protected_endspoints = new[] { "/Account/Login", "/Account/Register" };

            var removePassword = protected_endspoints.Any(x => requestPath.Value.StartsWith(x, StringComparison.OrdinalIgnoreCase));

            if (removePassword)
            {
                const string json_property_name = "Password\":\"";
                var indexOfPassword = bodyAsText.IndexOf(json_property_name) + json_property_name.Length;
                var nextIndex = bodyAsText.IndexOf('"', indexOfPassword);

                if (indexOfPassword != -1 && nextIndex != -1)
                {
                    bodyAsText = bodyAsText.ReplaceAt(indexOfPassword, nextIndex-indexOfPassword, "redacted");
                }
            }

            return bodyAsText;
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return $"{response.StatusCode}: {text}";
        }
    }
}