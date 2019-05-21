using System.Threading.Tasks;

namespace CoyoteNETCore.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync();
    }
}