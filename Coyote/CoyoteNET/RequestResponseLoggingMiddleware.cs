using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoyoteNET.Shared.Extensions;

namespace CoyoteNET
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
            Console.WriteLine(await FormatRequest(context.Request));
            var originalBodyStream = context.Response.Body;
            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;
            await _next(context);
            Console.WriteLine(await FormatResponse(context.Response));
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();

            using var data = new StreamReader(request.Body, Encoding.UTF8, false, 1024, true);
            var body = await data.ReadToEndAsync();
            request.Body.Seek(0, SeekOrigin.Begin);
            var bodyAsText = PreventPasswordsFromBeingLogged(request.Path, body);

            return $"{request.Scheme.ToUpper()}/{request.Method} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        public static string PreventPasswordsFromBeingLogged(PathString requestPath, string bodyAsText)
        {
            // this method will not work if there's more than 1 'Password' being sent.
            if (bodyAsText is null)
                return "";

            var protectedEndpoints = new[] { "/Account/Login", "/Account/Register" };

            var removePassword = protectedEndpoints.Any(x => requestPath.Value.StartsWith(x, StringComparison.OrdinalIgnoreCase));

            if (removePassword)
            {
                const string jsonPropertyName = "Password\":\"";
                var indexOfPassword = bodyAsText.IndexOf(jsonPropertyName, StringComparison.Ordinal);

                if (indexOfPassword != -1)
                {
                    indexOfPassword += jsonPropertyName.Length;
                    var nextIndex = bodyAsText.IndexOf('"', indexOfPassword);
                    
                    if (nextIndex != -1)
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