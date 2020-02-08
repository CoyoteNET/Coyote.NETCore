using CoyoteNET.Shared.RequestInput;
using FluentValidation;

namespace CoyoteNET.Application.Threads.Commands
{
    public class CreateThreadCommandValidation : AbstractValidator<CreateThreadInput>
    {
        public CreateThreadCommandValidation()
        {

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Thread is required to have a non-empty title");

            RuleFor(x => x.ThreadCategoryId)
                .NotNull()
                .WithMessage("Thread needs an Id of category to be associated with.");

            RuleFor(x => x.Body)
                .NotEmpty()
                .WithMessage("Thread is required to have a non-empty body");

            RuleFor(x => x.Tags)
                .NotEmpty()
                .WithMessage("Thread needs to have specified some tags.");
        }
    }
}
