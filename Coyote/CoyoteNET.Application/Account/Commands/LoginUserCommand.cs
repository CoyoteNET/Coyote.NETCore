using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CoyoteNET.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CoyoteNET.Shared.ResultHandling;
using CoyoteNET.Shared.Entities;
using CoyoteNET.Application.Services;
using CoyoteNET.Shared.Auth;

namespace CoyoteNET.Application.Account.Commands
{
    public class LoginUserCommand : IRequest<Result<JsonWebToken>>
    {
        public LoginUserCommand(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public string Name { get; set; }

        public string Password { get; set; }

        private class Handler : IRequestHandler<LoginUserCommand, Result<JsonWebToken>>
        {
            private readonly Context _db;
            private readonly IPasswordHasher<User> _passwordHasher;
            private readonly JwtService _jwt;

            public Handler(Context db, IPasswordHasher<User> passwordHasher, JwtService jwt)
            {
                _db = db;
                _passwordHasher = passwordHasher;
                _jwt = jwt;
            }

            public async Task<Result<JsonWebToken>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _db.Users.SingleOrDefaultAsync(x => x.Username == request.Name, cancellationToken);

                if (user == null)
                {
                    return new Result<JsonWebToken>(ErrorType.NotFound, "User Not Found");
                }

                var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
                if (verificationResult == PasswordVerificationResult.Success)
                {
                    var token = SignInJWT(user);
                    return new Result<JsonWebToken>(token);
                }

                return new Result<JsonWebToken>(ErrorType.BadRequest, "Wrong Password");
            }

            private JsonWebToken SignInJWT(User user)
            {
                return _jwt.BuildToken(user);
            }
        }
    }
}