using System;
using System.Threading;
using System.Threading.Tasks;
using CoyoteNETCore.Application.Services;
using CoyoteNETCore.DAL;
using CoyoteNETCore.Shared;
using CoyoteNETCore.Shared.Auth;
using CoyoteNETCore.Shared.Entities;
using CoyoteNETCore.Shared.ResultHandling;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoyoteNETCore.Application.Account.Commands
{
    public class RegisterUserCommand : IRequest<Result<JsonWebToken>>
    {
        public RegisterUserCommand(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        private class Handler : IRequestHandler<RegisterUserCommand, Result<JsonWebToken>>
        {
            private readonly Context _db;
            private readonly IMediator _mediator;
            private readonly IPasswordHasher<User> _passwordHasher;
            private readonly JwtService _jwt;

            public Handler(Context db, IMediator mediator, IPasswordHasher<User> passwordHasher, JwtService jwt)
            {
                _db = db;
                _mediator = mediator;
                _passwordHasher = passwordHasher;
                _jwt = jwt; 
            }

            public async Task<Result<JsonWebToken>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                var verifyResult = await Verify<string>(request);

                if (verifyResult != null) //TODO: ugly
                {
                    return new Result<JsonWebToken>(ErrorType.Unauthorized, "Unauthorized");
                }

                var user = new User(request.Name, request.Email);
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

                _db.Users.Add(user);
                await _db.SaveChangesAsync(cancellationToken);

                await _mediator.Publish(new UserRegistered { Email = request.Email }, cancellationToken);

                var token = _jwt.BuildToken(user);

                return new Result<JsonWebToken>(token);
            }

            private async Task<Result<T>> Verify<T>(RegisterUserCommand request)
            {
                if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Email) ||string.IsNullOrWhiteSpace(request.Password))
                {
                    return new Result<T>(ErrorType.BadRequest, "Every field - name, email and password has to have a value.");
                }

                if (await _db.Users.AnyAsync(x => x.Username.ToLower() == request.Name.ToLower()))
                {
                    return new Result<T>(ErrorType.AlreadyExists, "An account with the given username already exists.");
                }

                if (await _db.Users.AnyAsync(x => x.Email.ToLower() == request.Email.ToLower()))
                {
                    return new Result<T>(ErrorType.AlreadyExists, "The e-mail address provided is already used.");
                }

                return null; //TODO: ugly
            }
        }
    }
}