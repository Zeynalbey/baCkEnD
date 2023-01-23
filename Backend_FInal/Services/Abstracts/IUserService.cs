using Backend_Final.Areas.Client.ViewModels.Authentication;
using Backend_Final.Database.Models;

namespace Backend_Final.Services.Abstracts
{
    public interface IUserService
    {
        public bool IsAuthenticated { get; }
        public User CurrentUser { get; }

        Task<bool> CheckPasswordAsync(string? email, string? password);
        string GetCurrentUserFullName();
        Task SignInAsync(Guid id, string? role = null);
        Task SignInAsync(string? email, string? password, string? role = null);
        Task CreateAsync(RegisterViewModel model);
        Task SignOutAsync();
        Task<bool> CheckEmailConfirmedAsync(string? email);

    }
}
