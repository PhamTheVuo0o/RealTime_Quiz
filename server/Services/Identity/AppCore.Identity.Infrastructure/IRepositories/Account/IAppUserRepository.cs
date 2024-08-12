using Microsoft.AspNetCore.Identity;
using AppCore.Identity.Domain.Entities;
using AppCore.Infrastructure.Persistence.Repositories;

namespace AppCore.Identity.Infrastructure.IRepositories.Account
{
    public interface IAppUserRepository : IBaseRepository<AppUser>
    {
        UserManager<AppUser> GetUserManager();
        Task<bool> IsEmailExists(string email);
        Task<AppUser> GetUserByEmail(string email);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<bool> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword);
        Task<string> GeneratePasswordResetTokenAsync(AppUser user);
        Task<bool> ResetPasswordAsync(AppUser user, string token, string newPassword);
        Task AddOrUpdateUserAsync(AppUser request);
    }
}
