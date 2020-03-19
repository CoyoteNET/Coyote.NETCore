using CoyoteNET.DAL;
using CoyoteNET.Shared.ResultHandling;
using CoyoteNET.Shared.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoyoteNET.Application.Interfaces;
using CoyoteNET.Shared.DTOs;

namespace CoyoteNET.Application.Threads.Commands
{
    public class GetThreadsQuery : IRequest<Result<IEnumerable<ThreadDTO>>>
    {
        public GetThreadsQuery()
        {
        }

        public class Handler :
            IRequestHandler<GetThreadsQuery, Result<IEnumerable<ThreadDTO>>>,
            IBusinessLogicValidation<GetThreadsQuery>
        {
            private readonly Context _context;

            public Handler(Context context)
            {
                _context = context;
            }

            public async Task<Result<IEnumerable<ThreadDTO>>> Handle(GetThreadsQuery request, CancellationToken cancellationToken)
            {
                var verify = await Verify(request);

                if (!verify.Success)
                    return new Result<IEnumerable<ThreadDTO>>(ErrorType.BadRequest, verify.Result);

                var threads = await _context
                                    .Threads
                                    .Include(x => x.Author)
                                    .ThenInclude(x => x.Avatar)
                                    .Include(x => x.Posts)
                                    .ThenInclude(x => x.Author)
                                    .ThenInclude(x => x.Avatar)
                                    .Include(x => x.Category)
                                    .ThenInclude(x => x.Section)
                                    .Select(x => new ThreadDTO
                                    {
                                        Id = x.Id,
                                        Title = x.Title,
                                        Tags = x.Tags,
                                        CreationDate = x.CreationDate,
                                        Author = new UserDTO
                                        {
                                            Username = x.Author.Username,
                                            Id = x.Author.Id,
                                            Avatar = x.Author.Avatar == null ? null : new FileDTO
                                            {
                                                Id = x.Author.Avatar.Id,
                                                UserFileName = x.Author.Avatar.UserFileName
                                            }
                                        },
                                        Posts = x.Posts.Select(p => new PostDTO
                                        {
                                            Id = p.Id,
                                            Content = p.Content,
                                            ThreadId = p.ThreadId,
                                            Author = new UserDTO
                                            {
                                                Username = p.Author.Username,
                                                Id = p.Author.Id,
                                                Avatar = x.Author.Avatar == null ? null : new FileDTO
                                                {
                                                    Id = p.Author.Avatar.Id,
                                                    UserFileName = p.Author.Avatar.UserFileName
                                                }
                                            }
                                        }).ToList(),
                                        Category = new ThreadCategoryDTO
                                        {
                                            Id = x.Category.Id,
                                            Name = x.Category.Name,
                                            Section = new ForumSectionDTO
                                            {
                                                Id = x.Category.Section.Id,
                                                Name = x.Category.Section.Name,
                                            }
                                        }
                                    })
                                    .ToListAsync();

                return new Result<IEnumerable<ThreadDTO>>(threads);
            }

            public async Task<(bool Success, IEnumerable<string> Result)> Verify(GetThreadsQuery ValidationObject)
            {
                var errors = new List<string>();

                if (ValidationObject == null)
                {
                    errors.Add("Data to obtain thread was not received. Something went wrong.");
                    return (false, errors);
                }

                return (errors.Count == 0, errors);
            }
        }
    }
}
