using System.Threading.Tasks;
using CoyoteNET.Application.Interfaces;

namespace CoyoteNET.Application.Services
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync()
        {
            return Task.CompletedTask;
        }
    }
}