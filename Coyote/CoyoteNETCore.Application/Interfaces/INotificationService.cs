using System.Threading.Tasks;

namespace CoyoteNETCore.Application.Interfaces
{
    internal interface INotificationService
    {
        Task SendAsync();
    }
}