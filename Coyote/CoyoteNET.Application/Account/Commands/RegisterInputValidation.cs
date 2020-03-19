using CoyoteNET.Shared.RequestInput;
using FluentValidation;

namespace CoyoteNET.Application.Account.Commands
{
    public class RegisterInputValidation : AbstractValidator<RegisterInput>
    {
        public RegisterInputValidation()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                                 .EmailAddress().WithMessage("A valid email is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
                                    .MinimumLength(10).WithMessage("The password should be at least 10 characters long.");
        }
    }
}
