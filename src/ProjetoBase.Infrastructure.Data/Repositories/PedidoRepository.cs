using Microsoft.EntityFrameworkCore;
using ProjetoBase.Domain.Entities;
using ProjetoBase.Domain.Interfaces.Repositories;
using ProjetoBase.Infrastructure.Data.Context;
using System;
using System.Linq;

namespace ProjetoBase.Infrastructure.Data.Repositories
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        protected readonly DbSet<ItemPedido> DbItemPedido;

        public PedidoRepository(ProjetoBaseContext context) : base(context) => DbItemPedido = DbContext.Set<ItemPedido>();

        public bool ExistemPedidosAssociadoCliente(Guid clienteId) => DbEntity.AsNoTracking().Any(p => p.ClienteId == clienteId);

        public bool ExistemItensPedidoAssociadoProduto(Guid produtoId) => DbItemPedido.AsNoTracking().Any(i => i.ProdutoId == produtoId);
    }
}
