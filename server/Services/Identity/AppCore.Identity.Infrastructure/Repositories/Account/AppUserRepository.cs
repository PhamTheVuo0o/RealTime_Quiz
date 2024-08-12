using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using AppCore.Identity.Domain.Entities;
using AppCore.Identity.Infrastructure.IRepositories.Account;
using AppCore.Infrastructure.Common;
using AppCore.Infrastructure.Persistence.Repositories;
using System.Data;
using AppCore.Infrastructure.Common.Constants;

namespace AppCore.Identity.Infrastructure.Repositories.Account
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<AppSetting> _config;
        public AppUserRepository(DataContext context,
            IHttpContextAccessor httpContextAccessor,
            UserManager<AppUser> userManager,
            IOptions<AppSetting> config) : base(context, httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _config = config;
        }

        public UserManager<AppUser> GetUserManager()
        {
            return _userManager;
        }

        public async Task<bool> IsEmailExists(string email)
        {
            var user = await GetUserByEmail(email);
            return user != null;
        }


        public async Task<AppUser> GetUserByEmail(string email)
        {
            var normalizedEmail = email.ToUpper();
            var user = await GetAsync(x => x.NormalizedEmail == normalizedEmail);
            return user;
        }

        public Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            return _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<bool> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword)
        {
            var rlt = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (rlt != null && rlt.Succeeded) 
            {
                user.Author = user.Id.ToString();
                await UpdateAsync(user);
                return true;
            }
            return false;
        }

        public Task<string> GeneratePasswordResetTokenAsync(AppUser user)
        {
            return _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<bool> ResetPasswordAsync(AppUser user, string token, string newPassword)
        {
            var rlt = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (rlt != null && rlt.Succeeded)
            {
                user.Author = user.Id.ToString();
                await UpdateAsync(user);
                return true;
            }
            return false;
        }

        public async Task AddOrUpdateUserAsync(AppUser request)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                await _userManager.CreateAsync(request, CoreConstant.PASSWORD_DEFAULT);
            }
            else
            {
                var properties = _context.Model.FindEntityType(typeof(AppUser))?.GetProperties();
                if (properties != null)
                {
                    foreach (var item in properties)
                    {
                        var valueFromNewEntity = request.GetType().GetProperty(item.Name)?.GetValue(request);
                        if (valueFromNewEntity != null)
                        {
                            user.GetType().GetProperty(item.Name)?.SetValue(user, valueFromNewEntity);
                        }
                    }
                    await UpdateAsync(user);
                }
            }
        }
    }
}
