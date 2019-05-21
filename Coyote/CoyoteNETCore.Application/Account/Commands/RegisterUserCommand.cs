using System.Threading;
using System.Threading.Tasks;
using CoyoteNETCore.DAL;
using CoyoteNETCore.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CoyoteNETCore.Application.Account.Commands
{
    public class RegisterUserCommand : IRequest<(bool Success, string Result)>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        private class Handler : IRequestHandler<RegisterUserCommand, (bool Success, string Result)>
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

            public async Task<(bool Success, string Result)> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                var user = new User(request.Name, request.Email);
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

                _db.Users.Add(user);
                await _db.SaveChangesAsync(cancellationToken);

                await _mediator.Publish(new UserRegistered { Email = request.Email }, cancellationToken);

                return (true, "User has been registered");
            }
        }
    }
}