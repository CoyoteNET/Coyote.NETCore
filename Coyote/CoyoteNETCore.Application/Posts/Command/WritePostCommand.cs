using CoyoteNETCore.Application.Interfaces;
using CoyoteNETCore.DAL;
using CoyoteNETCore.Shared;
using CoyoteNETCore.Shared.Entities;
using CoyoteNETCore.Shared.ResultHandling;
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
    public class WritePostCommand : IRequest<Result<int>>
    {
        public WritePostCommand(int threadId, string body, int authorId)
        {
            ThreadId = threadId;
            Body = body;
            AuthorId = authorId;
            AuthorId = authorId;
        }

        public int ThreadId { get; }

        public string Body { get; }

        public User Author { get; set; }

        public int AuthorId { get; set; }

        public class Handler :
            IRequestHandler<WritePostCommand, Result<int>>,
            IBusinessLogicValidation<WritePostCommand>
        {
            private readonly Context _context;

            public Handler(Context context)
            {
                _context = context;
            }

            public async Task<Result<int>> Handle(WritePostCommand command, CancellationToken cancellationToken)
            {
                command.Author = await _context.Users.FirstOrDefaultAsync(x => x.Id == command.AuthorId);

                var verifyResult = await Verify(command);

                if (!verifyResult.Success)
                    return new Result<int>(ErrorType.BadRequest, string.Join(Environment.NewLine, verifyResult.Result));

                return await WritePost(command);
            }

            private async Task<Result<int>> WritePost(WritePostCommand request)
            {
                var thread = await _context
                                    .Threads
                                    .Include(x => x.Posts)
                                    .FirstOrDefaultAsync(x => x.Id == request.ThreadId);

                var post = new Post(request.Body, thread, request.Author);
                thread.Posts.Add(post);
                await _context.SaveChangesAsync();

                return new Result<int>(thread.Id);
            }

            public async Task<(bool Success, IEnumerable<string> Result)> Verify(WritePostCommand ValidationObject)
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

                if (!await _context.Threads.AnyAsync(c => c.Id == ValidationObject.ThreadId))
                {
                    errors.Add($"Thread with an Id: '{ValidationObject.ThreadId}' does not exist.");
                }

                if (string.IsNullOrWhiteSpace(ValidationObject.Body))
                {
                    errors.Add("Post body cannot be empty.");
                }

                return (errors.Count == 0, errors);
            }
        }
    }
}
