using FluentValidation;

namespace CoyoteNETCore.Application.Threads.Commands
{
    public class CreateThreadCommandValidation : AbstractValidator<CreateThreadCommand>
    {
        public CreateThreadCommandValidation()
        {
            RuleFor(x => x.Author)
                .NotNull()
                .WithMessage("Thread author does not exist.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Thread is required to have a non-empty title");

            RuleFor(x => x.Body)
                .NotEmpty()
                .WithMessage("Thread is required to have a non-empty body");
        }
    }
}
