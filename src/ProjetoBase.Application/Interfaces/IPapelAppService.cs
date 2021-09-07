using ProjetoBase.Application.ViewModels.Direitos;
using ProjetoBase.Application.ViewModels.Papeis;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;
using System.Collections.Generic;

namespace ProjetoBase.Application.Interfaces
{
    public interface IPapelAppService : IAppService<RoleViewModel>, ITabelaAppService<RoleTableViewModel>, ISelecaoAppService<Guid, string>
    {
        NotificationResult AdicionarAutorizacoes(RoleClaimViewModel viewModel);
        RoleClaimViewModel ObterAutorizacoes(Guid id);
        IEnumerable<MenuClaimViewModel> ObterMenusAutorizados(Guid id);
    }
}
