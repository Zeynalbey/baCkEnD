using Backend_Final.Areas.Client.ViewModels.Authentication;
using Backend_Final.Database.Models;

namespace Backend_Final.Services.Abstracts
{
    public interface IUserActivationService
    {
        Task SendActivationUrlAsync(User user); 

    }
}
