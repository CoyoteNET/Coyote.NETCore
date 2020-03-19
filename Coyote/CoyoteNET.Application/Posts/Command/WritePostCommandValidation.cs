using CoyoteNET.Shared.RequestInput;
using FluentValidation;

namespace CoyoteNET.Application.Threads.Commands
{
    public class WritePostCommandValidation : AbstractValidator<WritePostInput>
    {
        public WritePostCommandValidation()
        {
            RuleFor(x => x.ThreadId)
                    .NotNull()
                    .WithMessage("Thread Id is incorrect.");

            RuleFor(x => x.Body)
                    .NotEmpty()
                    .WithMessage("Thread body cannot be empty.");
        }
    }
}
