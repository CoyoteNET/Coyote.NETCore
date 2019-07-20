using System.Threading.Tasks;
using CoyoteNETCore.Application.Interfaces;

namespace CoyoteNETCore.Application.Services
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync()
        {
            return Task.CompletedTask;
        }
    }
}