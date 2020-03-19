using System.Threading.Tasks;

namespace CoyoteNET.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync();
    }
}