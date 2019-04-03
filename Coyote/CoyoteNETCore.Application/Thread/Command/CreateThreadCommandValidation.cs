using CoyoteNETCore.DAL;
using CoyoteNETCore.Shared;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoyoteNETCore.Application.Thread.Command
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
        }
    }
}
