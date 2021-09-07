using ProjetoBase.Infrastructure.CrossCutting.Common.Login;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects;
using System;
using System.Threading.Tasks;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Interfaces
{
    public interface IAccountService : IDisposable
    {
        Task<LoginResult> SignInAsync(SignIn signIn);
        Task<Account> GetAuthenticatedUserAccountAsync();
        Task<NotificationResult> UpdateAuthenticatedUserAccountAsync(Account account);
        Task<NotificationResult> UpdateAuthenticatedUserPasswordAsync(UpdatePassword updatePassword);
    }
}
