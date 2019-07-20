using System;
using System.Threading;
using System.Threading.Tasks;
using CoyoteNETCore.DAL;
using CoyoteNETCore.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoyoteNETCore.Application.Account.Commands
{
    public class RegisterUserCommand : IRequest<Result<string>>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        private class Handler : IRequestHandler<RegisterUserCommand, Result<string>>
        {
            private readonly Context _db;
            private readonly IMediator _mediator;
            private readonly IPasswordHasher<User> _passwordHasher;

            public Handler(Context db, IMediator mediator, IPasswordHasher<User> passwordHasher)
            {
                _db = db;
                _mediator = mediator;
                _passwordHasher = passwordHasher;
            }

            public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                var verifyResult = await Verify<string>(request);
                if (verifyResult != null) //TODO: ugly
                {
                    return verifyResult;
                }

                var user = new User(request.Name, request.Email);
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

                _db.Users.Add(user);
                await _db.SaveChangesAsync(cancellationToken);

                await _mediator.Publish(new UserRegistered { Email = request.Email }, cancellationToken);

                return new Result<string>("User has been registered");
            }

            private async Task<Result<T>> Verify<T>(RegisterUserCommand request)
            {
                if (await _db.Users.AnyAsync(x => string.Equals(x.Name, request.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    return new Result<T>(ErrorType.AlreadyExists, "An account with the given username already exists.");
                }

                if (await _db.Users.AnyAsync(x => string.Equals(x.Email, request.Email, StringComparison.OrdinalIgnoreCase)))
                {
                    return new Result<T>(ErrorType.AlreadyExists, "The e-mail address provided is already used.");
                }

                return null; //TODO: ugly
            }
        }
    }
}