using CoyoteNET.Shared.RequestInput;
using FluentValidation;

namespace CoyoteNET.Application.Threads.Commands
{
    public class EditPostCommandValidation : AbstractValidator<EditPostInput>
    {
        public EditPostCommandValidation()
        {
            RuleFor(x => x.PostId)
                    .NotNull()
                    .WithMessage("Thread Id is incorrect.");

            RuleFor(x => x.Body)
                    .NotEmpty()
                    .WithMessage("Thread body cannot be empty.");
        }
    }
}
