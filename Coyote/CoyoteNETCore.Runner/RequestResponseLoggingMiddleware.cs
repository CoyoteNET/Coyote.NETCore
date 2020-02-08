using Microsoft.AspNetCore.Http;
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
            Console.WriteLine(request);
            Console.WriteLine();

            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                await _next(context);
                var response = await FormatResponse(context.Response);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();

            // In HTTP2 ContentLength is optional.
            // https://svn.tools.ietf.org/svn/wg/httpbis/specs/rfc7230.html#header.content-length
            // https://svn.tools.ietf.org/svn/wg/httpbis/specs/rfc7230.html#message.body.length

            var buffer = new byte[Convert.ToInt32(request.ContentLength ?? (request.Body?.Length ?? 0))];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Position = 0; // allows framework to process this body once again

            bodyAsText = PreventPasswordsFromBeingLogged(request.Path, bodyAsText);

            return $"{request.Scheme.ToUpper()}/{request.Method} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        public static string PreventPasswordsFromBeingLogged(PathString requestPath, string bodyAsText)
        {
            // this method will not work if there's more than 1 'Password' being sent.

            if (bodyAsText is null)
                return "";

            var protected_endspoints = new[] { "/Account/Login", "/Account/Register" };

            var removePassword = protected_endspoints.Any(x => requestPath.Value.StartsWith(x, StringComparison.OrdinalIgnoreCase));

            if (removePassword)
            {
                const string json_property_name = "Password\":\"";
                var indexOfPassword = bodyAsText.IndexOf(json_property_name);

                if (indexOfPassword != -1)
                {
                    indexOfPassword += json_property_name.Length;
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