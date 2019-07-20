using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CoyoteNETCore.Application.Interfaces;

namespace CoyoteNETCore.Application.Account.Commands
{
    public class UserRegistered : INotification
    {
        public string Email { get; set; }

        private class UserRegisteredHandler : INotificationHandler<UserRegistered>
        {
            private readonly INotificationService _notification;

            public UserRegisteredHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(UserRegistered notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync();
            }
        }
    }
}