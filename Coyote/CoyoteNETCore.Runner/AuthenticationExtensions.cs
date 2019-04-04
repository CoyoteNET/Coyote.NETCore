using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace Coyote.NETCore
{
    public static class AuthenticationExtensions
    {
        public static AuthenticationBuilder AddGitHub(this AuthenticationBuilder builder, IConfiguration configuration)
        {
            builder.AddOAuth("GitHub", o =>
            {
                o.ClientId = configuration["GitHub:ClientId"];
                o.ClientSecret = configuration["GitHub:ClientSecret"];
                o.CallbackPath = new PathString("/signin-github");
                o.AuthorizationEndpoint = "http://github.com/login/oauth/authorize";
                o.TokenEndpoint = "https://github.com/login/oauth/access_token";
                o.UserInformationEndpoint = "https://api.github.com/user";

                o.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                o.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                o.ClaimActions.MapJsonKey("urn:github:login", "login");
                o.ClaimActions.MapJsonKey("urn:github:url", "html_url");
                o.ClaimActions.MapJsonKey("urn:github:avatar", "avatar_url");

                o.Events = new OAuthEvents
                {
                    OnCreatingTicket = async context =>
                    {
                        var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                        var response = await context.Backchannel.SendAsync(request,
                            HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                        response.EnsureSuccessStatusCode();

                        var user = JObject.Parse(await response.Content.ReadAsStringAsync());

                        context.RunClaimActions(user);
                    }
                };
            });

            return builder;
        }
    }
}