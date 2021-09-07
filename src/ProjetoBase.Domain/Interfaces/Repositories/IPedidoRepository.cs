using ProjetoBase.Domain.Entities;
using System;

namespace ProjetoBase.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        bool ExistemPedidosAssociadoCliente(Guid clienteId);
        bool ExistemItensPedidoAssociadoProduto(Guid produtoId);
    }
}
