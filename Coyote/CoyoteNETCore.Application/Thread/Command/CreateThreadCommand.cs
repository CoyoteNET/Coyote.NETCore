using CoyoteNETCore.DAL;
using CoyoteNETCore.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoyoteNETCore.Application.Thread.Command
{
    public class CreateThreadCommand : IRequest<(bool Success, string Result)>
    {
        public CreateThreadCommand(string body, string title, User author)
        {
            Body = body;
            Title = title;
            Author = author ?? throw new Exception();
        }

        public string Body { get; set; }

        public string Title { get; set; }

        public User Author { get; set; }

        private class Handler : IRequestHandler<CreateThreadCommand, (bool Success, string Result)>
        {
            private readonly Context _db;
            public Handler(Context db)
            {
                _db = db;
            }

            public Task<(bool Success, string Result)> Handle(CreateThreadCommand request, CancellationToken cancellationToken)
            {
                return Task.Delay(500).ContinueWith(t => (true, Guid.NewGuid().ToString()));
            }
        }
    }
}
