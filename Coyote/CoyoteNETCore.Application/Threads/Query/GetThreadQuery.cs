using CoyoteNETCore.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoyoteNETCore.Application.Threads.Commands
{
    public class GetThreadQuery : IRequest<Result<Shared.Thread>>
    {
        public GetThreadQuery(int? id)
        {
            Id = id;
        }

        public int? Id { get; }

        public class Handler :
            IRequestHandler<GetThreadQuery, Result<Shared.Thread>>,
            IBusinessLogicValidation<GetThreadQuery>
        {
            private readonly Context _context;

            public Handler(Context context)
            {
                _context = context;
            }

            public async Task<Result<Shared.Thread>> Handle(GetThreadQuery request, CancellationToken cancellationToken)
            {
                var verify = await Verify(request);

                if (!verify.Success)
                    return new Result<Shared.Thread>(ErrorType.BadRequest, verify.Result);

                var thread = await _context
                                   .Threads
                                   .Include(x => x.Author)
                                   .Include(x => x.Posts)
                                   .Include(x => x.Category)
                                   .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (thread == null)
                    return new Result<Shared.Thread>(ErrorType.NotFound, $"Thread with an id {request.Id} does not exist");

                return new Result<Shared.Thread>(thread);
            }

            public async Task<(bool Success, IEnumerable<string> Result)> Verify(GetThreadQuery ValidationObject)
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
