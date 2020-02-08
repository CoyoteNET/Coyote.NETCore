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
    public class EditPostCommand : IRequest<Result<int>>
    {
        public EditPostCommand(int postId, string content, int editorId)
        {
            PostId = postId;
            Content = content;
            EditorId = editorId;
        }

        public int PostId { get; }

        public string Content { get; }

        public User Editor { get; set; }

        public int EditorId { get; set; }

        public class Handler :
            IRequestHandler<EditPostCommand, Result<int>>,
            IBusinessLogicValidation<EditPostCommand>
        {
            private readonly Context _context;

            public Handler(Context context)
            {
                _context = context;
            }

            public async Task<Result<int>> Handle(EditPostCommand command, CancellationToken cancellationToken)
            {
                command.Editor = await _context.Users.FirstOrDefaultAsync(x => x.Id == command.EditorId);

                var verifyResult = await Verify(command);

                if (!verifyResult.Success)
                    return new Result<int>(ErrorType.BadRequest, string.Join(Environment.NewLine, verifyResult.Result));

                return await EditPost(command);
            }

            private async Task<Result<int>> EditPost(EditPostCommand request)
            {
                var post = await _context
                                 .Posts
                                 .FirstOrDefaultAsync(x => x.Id == request.PostId);

                //post.Editions.Add(new PostEdit(post, request.Editor)); // reference loop?????????????

                post.Content = request.Content;

                await _context.SaveChangesAsync();

                return new Result<int>(post.Id);
            }

            public async Task<(bool Success, IEnumerable<string> Result)> Verify(EditPostCommand ValidationObject)
            {
                var errors = new List<string>();

                if (ValidationObject == null)
                {
                    errors.Add("Data to create new thread was not received. Something went wrong.");
                    return (false, errors);
                }

                if (ValidationObject.Editor == null)
                {
                    errors.Add("Unable to determine User's profile");
                }

                var post = await _context
                                 .Posts
                                 .Include(x => x.Author)
                                 .FirstOrDefaultAsync(c => c.Id == ValidationObject.PostId);

                if (post == null)
                {
                    errors.Add($"Post with an Id: '{ValidationObject.PostId}' does not exist.");
                }

                if (post.Author.Id != ValidationObject.EditorId)
                {
                    errors.Add($"Only post's author can edit its content.");
                }

                if (string.IsNullOrWhiteSpace(ValidationObject.Content))
                {
                    errors.Add("Post body cannot be empty.");
                }

                return (errors.Count == 0, errors);
            }
        }
    }
}
