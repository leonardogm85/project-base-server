using ProjetoBase.Application.ViewModels;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;

namespace ProjetoBase.Application.Interfaces
{
    public interface IAppService<TViewModel> : IDisposable
        where TViewModel : ViewModel
    {
        NotificationResult Adicionar(TViewModel viewModel);
        NotificationResult Atualizar(TViewModel viewModel);
        NotificationResult Remover(Guid id);
        NotificationResult Ativar(Guid id);
        NotificationResult Desativar(Guid id);
        TViewModel ObterPorId(Guid id);
    }
}
