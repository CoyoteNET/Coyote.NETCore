using MediatR;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using CoyoteNETCore.DAL;
using CoyoteNETCore.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoyoteNETCore.Application.Auth.Commands
{
    public class LoginUserCommand : IRequest<(bool Success, string Result)>
    {
        public string Name { get; set; }

        public string Password { get; set; }

        private class Handler : IRequestHandler<LoginUserCommand, (bool Success, string Result)>
        {
            private readonly Context _db;
            private readonly IPasswordHasher<User> _passwordHasher;
            private readonly IHttpContextAccessor _httpAccessor;

            public Handler(Context db, IPasswordHasher<User> passwordHasher, IHttpContextAccessor httpAccessor)
            {
                _db = db;
                _passwordHasher = passwordHasher;
                _httpAccessor = httpAccessor;
            }

            public async Task<(bool Success, string Result)> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _db.Users.SingleAsync(x => x.Name == request.Name, cancellationToken);
                if (user == null)
                {
                    return (false, "User Not Found");
                }

                var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
                if (verificationResult == PasswordVerificationResult.Success)
                {
                    await SingIn(request);
                    return (true, "Logged");
                }

                return (false, "Wrong Password");
            }

            private async Task SingIn(LoginUserCommand request)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, request.Name),
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await _httpAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
            }
        }
    }
}