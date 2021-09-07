using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Interfaces
{
    public interface IUserService : IService<User>, ITableService<UserTable>, ISelectService<Guid, string>
    {
        Task<NotificationResult> ResetPasswordAsync(ResetPassword resetPassword);
        Task<NotificationResult> ForgotPasswordAsync(ForgotPassword forgotPassword);
        Task<NotificationResult> ConfirmEmailAsync(ConfirmEmail confirmEmail);
        Task<NotificationResult> SendEmailConfirmationTokenAsync(Guid id);
        Task<NotificationResult> SendPasswordResetTokenAsync(Guid id);
        Task<bool> ExistsAdministratorAsync();
        Task<NotificationResult> AddUserClaimsAsync(UserClaim userClaim);
        Task<UserClaim> GetUserClaimsAsync(Guid id);
        Task<IEnumerable<MenuClaim>> GetMenuClaimsAsync(Guid id);
        Task<NotificationResult> AddUserRolesAsync(UserRole userRole);
        Task<UserRole> GetUserRolesAsync(Guid id);
        Task<IEnumerable<string>> GetRoleNamesAsync(Guid id);
    }
}
