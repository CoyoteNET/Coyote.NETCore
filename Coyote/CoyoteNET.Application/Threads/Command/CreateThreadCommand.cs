using CoyoteNET.Application.Interfaces;
using CoyoteNET.DAL;
using CoyoteNET.Shared;
using CoyoteNET.Shared.Entities;
using CoyoteNET.Shared.ResultHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoyoteNET.Application.Threads.Commands
{
    public class CreateThreadCommand : IRequest<Result<int>>
    {
        public CreateThreadCommand(string body, string title, int categoryId, string tags, int authorId)
        {
            Body = body;
            Title = title;
            Tags = tags;
            ThreadCategoryId = categoryId;
            AuthorId = authorId;
        }

        public string Body { get; }

        public string Title { get; }

        public string Tags { get; }

        public User Author { get; set; }

        public int AuthorId { get; set; }

        public int ThreadCategoryId { get; }

        public class Handler :
            IRequestHandler<CreateThreadCommand, Result<int>>,
            IBusinessLogicValidation<CreateThreadCommand>
        {
            private readonly Context _context;

            public Handler(Context context)
            {
                _context = context;
            }

            public async Task<Result<int>> Handle(CreateThreadCommand command, CancellationToken cancellationToken)
            {
                command.Author = await _context.Users.FirstOrDefaultAsync(x => x.Id == command.AuthorId);

                var verifyResult = await Verify(command);

                if (!verifyResult.Success)
                    return new Result<int>(ErrorType.BadRequest, string.Join(Environment.NewLine, verifyResult.Result));

                return await CreateThread(command);
            }

            private async Task<Result<int>> CreateThread(CreateThreadCommand request)
            {
                var category = await _context.ThreadCategories.FindAsync(request.ThreadCategoryId);

                var thread = new Shared.Entities.Thread(category, request.Tags, request.Title, request.Author);

                var first_post = new Post(request.Body, thread, request.Author);

                thread.Posts.Add(first_post);

                await _context.Threads.AddAsync(thread);

                await _context.SaveChangesAsync();

                return new Result<int>(thread.Id);
            }

            public async Task<(bool Success, IEnumerable<string> Result)> Verify(CreateThreadCommand ValidationObject)
            {
                var errors = new List<string>();

                if (ValidationObject == null)
                {
                    errors.Add("Data to create new thread was not received. Something went wrong.");
                    return (false, errors);
                }

                if (ValidationObject.Author == null)
                {
                    errors.Add("Unable to determine User's profile");
                }

                //if (ValidationObject.Author?.IsUserBanned ?? true)
                //{
                //    problems.Add("Banned users are unable to create threads.");
                //}

                if (!await _context.ThreadCategories.AnyAsync(c => c.Id == ValidationObject.ThreadCategoryId))
                {
                    errors.Add($"Thread category with an Id: '{ValidationObject.ThreadCategoryId}' does not exist.");
                }

                if (string.IsNullOrWhiteSpace(ValidationObject.Body))
                {
                    errors.Add("Thread cannot be empty.");
                }

                return (errors.Count == 0, errors);
            }
        }
    }
}
