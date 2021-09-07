using ProjetoBase.Application.ViewModels.Contas;
using ProjetoBase.Infrastructure.CrossCutting.Common.Login;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;

namespace ProjetoBase.Application.Interfaces
{
    public interface IContaAppService : IDisposable
    {
        LoginResult Login(SignInViewModel viewModel);
        AccountViewModel ObterContaUsuarioAutenticado();
        NotificationResult AtualizarContaUsuarioAutenticado(AccountViewModel viewModel);
        NotificationResult AtualizarSenhaUsuarioAutenticado(UpdatePasswordViewModel viewModel);
    }
}
