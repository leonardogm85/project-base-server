using ProjetoBase.Domain.Entities;
using ProjetoBase.Domain.Interfaces.Services;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;

namespace ProjetoBase.Domain.Services
{
    public class PedidoService : IPedidoService
    {
        public NotificationResult Adicionar(Pedido pedido) => throw new NotImplementedException();

        public NotificationResult Atualizar(Pedido pedido) => throw new NotImplementedException();

        public NotificationResult Remover(Guid id) => throw new NotImplementedException();

        public NotificationResult Ativar(Guid id) => throw new NotImplementedException();

        public NotificationResult Desativar(Guid id) => throw new NotImplementedException();

        public Pedido ObterPorId(Guid id) => throw new NotImplementedException();

        public void Dispose() => throw new NotImplementedException();
    }
}
