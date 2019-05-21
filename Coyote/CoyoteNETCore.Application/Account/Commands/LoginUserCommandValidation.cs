using System.Linq;
using CoyoteNETCore.DAL;
using FluentValidation;

namespace CoyoteNETCore.Application.Account.Commands
{
    public class LoginUserCommandValidation : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidation(Context db)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");

            RuleFor(x => x.Name).Custom((name, context) =>
            {
                var user = db.Users.Single(x => x.Name == name);
                if (user == null)
                {
                    context.AddFailure("User not found.");
                }
            });
        }
    }
}
