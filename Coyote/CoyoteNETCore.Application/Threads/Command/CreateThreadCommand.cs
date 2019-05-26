using CoyoteNETCore.DAL;
using CoyoteNETCore.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoyoteNETCore.Application.Threads.Commands
{
    public class CreateThreadCommand : IRequest<(bool Success, IEnumerable<string> Result, int? Id)>
    {
        public CreateThreadCommand(string body, string title, int categoryId, int authorId)
        {
            Body = body;
            Title = title;
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
            IRequestHandler<CreateThreadCommand, (bool Success, IEnumerable<string> Result, int? Id)>, 
            IBusinessLogicValidation<CreateThreadCommand>
        {
            private readonly Context _context;

            public Handler(Context context)
            {
                _context = context;
            }

            public async Task<(bool Success, IEnumerable<string> Result, int? Id)> Handle(CreateThreadCommand request, CancellationToken cancellationToken)
            {
                request.Author = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.AuthorId);

                var verifyResult = await Verify(request);

                if (!verifyResult.Success)
                {
                    return (false, verifyResult.Result, null);
                }

                return await CreateThread(request);
            }

            private async Task<(bool Success, IEnumerable<string> Result, int? Id)> CreateThread(CreateThreadCommand request)
            {
                var category = await _context.ThreadCategories.FindAsync(request.ThreadCategoryId);

                var thread = new Shared.Thread(category, request.Tags, request.Title, request.Author);

                var first_post = new Post(request.Body, thread, request.Author);

                thread.Posts.Add(first_post);

                await _context.Threads.AddAsync(thread);

                await _context.SaveChangesAsync();

                return (true, Enumerable.Empty<string>(), thread.Id);
            }

            public async Task<(bool Success, IEnumerable<string> Result)> Verify(CreateThreadCommand ValidationObject)
            {
                var problems = new List<string>();

                if (ValidationObject == null)
                {
                    problems.Add("Data to create new thread was not received. Something went wrong.");
                    return (true, problems);
                }

                if (ValidationObject.Author == null)
                {
                    problems.Add("Unable to determine User's profile");
                }

                if (ValidationObject.Author?.IsUserBanned ?? true)
                {
                    problems.Add("Banned users are unable to create threads.");
                }

                if (!await _context.ThreadCategories.AnyAsync(c => c.Id == ValidationObject.ThreadCategoryId))
                {
                    problems.Add($"Thread category with an Id: '{ValidationObject.ThreadCategoryId}' does not exist.");
                }

                if (string.IsNullOrWhiteSpace(ValidationObject.Body))
                {
                    problems.Add("Thread cannot be empty.");
                }

                return (!problems.Any(), problems);
            }

        }
    }
}
