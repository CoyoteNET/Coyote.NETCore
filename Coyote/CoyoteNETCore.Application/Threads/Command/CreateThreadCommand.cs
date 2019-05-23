using CoyoteNETCore.DAL;
using CoyoteNETCore.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoyoteNETCore.Application.Threads.Command
{
    public class CreateThreadCommand : IRequest<(bool Success, IEnumerable<string> Result, int? Id)>
    {
        public CreateThreadCommand(string body, string title, int categoryId, User author)
        {
            Body = body;
            Title = title;
            ThreadCategoryId = categoryId;
            Author = author ?? throw new Exception();
        }

        public string Body { get; }

        public string Title { get; }

        public string Tags { get; }

        public User Author { get; }

        public int ThreadCategoryId { get; }

        private class Handler :
            IRequestHandler<CreateThreadCommand, (bool Success, IEnumerable<string> Result, int? Id)>, 
            BusinessLogicValidation<CreateThreadCommand>
        {
            private readonly Context _context;

            public Handler(Context context)
            {
                _context = context;
            }

            public async Task<(bool Success, IEnumerable<string> Result, int? Id)> Handle(CreateThreadCommand request, CancellationToken cancellationToken)
            {
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

                await _context.Threads.AddAsync(thread);

                await _context.SaveChangesAsync();

                return (true, Enumerable.Empty<string>(), thread.Id);
            }

            public async Task<(bool Success, IEnumerable<string> Result)> Verify(CreateThreadCommand ValidationObject)
            {
                var problems = new List<string>();

                if (ValidationObject.Author.IsUserBanned)
                {
                    problems.Add("Banned users are unable to create threads.");
                }

                if (!_context.ThreadCategories.Any(c => c.Id == ValidationObject.ThreadCategoryId))
                {
                    problems.Add($"Thread category with an Id: '{ValidationObject.ThreadCategoryId}' does not exist.");
                }

                return (!problems.Any(), problems);
            }

        }
    }
}
