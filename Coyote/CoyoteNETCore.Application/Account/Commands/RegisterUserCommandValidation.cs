using System.Linq;
using CoyoteNETCore.DAL;
using FluentValidation;

namespace CoyoteNETCore.Application.Account.Commands
{
    public class RegisterUserCommandValidation : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidation(Context db)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                                 .EmailAddress().WithMessage("A valid email is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
                                    .MinimumLength(10).WithMessage("The password should be at least 10 characters long.");

            RuleFor(x => x.Name).Custom((name, context) =>
            {
                if (db.Users.Any(x => x.Name == name))
                {
                    context.AddFailure("An account with the given username already exists.");
                }
            });

            RuleFor(x => x.Name).Custom((email, context) =>
            {
                if (db.Users.Any(x => x.Email == email))
                {
                    context.AddFailure("The e-mail address provided is already used.");
                }
            });


        }
    }
}
