using CoyoteNET.Shared.RequestInput;
using FluentValidation;

namespace CoyoteNET.Application.Account.Commands
{
    public class LoginInputValidation : AbstractValidator<LoginInput>
    {
        public LoginInputValidation()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
