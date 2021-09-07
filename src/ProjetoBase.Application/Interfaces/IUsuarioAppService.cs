using ProjetoBase.Application.ViewModels.Direitos;
using ProjetoBase.Application.ViewModels.Usuarios;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;
using System.Collections.Generic;

namespace ProjetoBase.Application.Interfaces
{
    public interface IUsuarioAppService : IAppService<UserViewModel>, ITabelaAppService<UserTableViewModel>, ISelecaoAppService<Guid, string>
    {
        NotificationResult RedefinirSenha(ResetPasswordViewModel viewModel);
        NotificationResult EsqueceuSenha(ForgotPasswordViewModel viewModel);
        NotificationResult ConfirmarEmail(ConfirmEmailViewModel viewModel);
        NotificationResult EnviarTokenConfirmacaoPorEmail(Guid id);
        NotificationResult AdicionarAutorizacoes(UserClaimViewModel viewModel);
        UserClaimViewModel ObterAutorizacoes(Guid id);
        IEnumerable<MenuClaimViewModel> ObterMenusAutorizados(Guid id);
        NotificationResult AdicionarPapeis(UserRoleViewModel viewModel);
        UserRoleViewModel ObterPapeis(Guid id);
        IEnumerable<string> ObterNomesPapeis(Guid id);
    }
}
