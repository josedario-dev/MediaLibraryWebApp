using MediaLibrary.WebApp.Core;
using MediaLibrary.WebApp.Core.Entities;
using MediaLibrary.WebApp.Shared.DTOs;
using Microsoft.AspNetCore.Identity;

namespace MediaLibrary.WebApp.Server.Helpers.Contracts
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        /// <summary>
        /// Return a boolean if the User belongs to the role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<bool> IsUserInRoleAsync(User user, string roleName);
        Task<SignInResult> LoginAsync(LoginDto model);

        Task LogoutAsync();

        Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<User> GetUserAsync(Guid userId);
        Task<string> GenerateEmailConfirmationTokenAsync(User user);
        Task<IdentityResult> ConfirmEmailAsync(User user, string token);
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);
        Task<string> GeneratePasswordResetTokenAsync(User user);
    }
}
