using ProjetoBase.Application.Interfaces;
using ProjetoBase.Application.ViewModels.Pedidos;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;

namespace ProjetoBase.Application.Services
{
    public class PedidoAppService : IPedidoAppService
    {
        public NotificationResult Adicionar(PedidoViewModel viewModel) => throw new NotImplementedException();

        public NotificationResult Atualizar(PedidoViewModel viewModel) => throw new NotImplementedException();

        public NotificationResult Remover(Guid id) => throw new NotImplementedException();

        public NotificationResult Ativar(Guid id) => throw new NotImplementedException();

        public NotificationResult Desativar(Guid id) => throw new NotImplementedException();

        public PedidoViewModel ObterPorId(Guid id) => throw new NotImplementedException();

        public void Dispose() => throw new NotImplementedException();
    }
}
