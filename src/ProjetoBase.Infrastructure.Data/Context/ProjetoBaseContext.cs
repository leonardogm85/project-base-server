using Microsoft.EntityFrameworkCore;
using ProjetoBase.Domain.Entities;
using ProjetoBase.Infrastructure.Data.Mappings;

namespace ProjetoBase.Infrastructure.Data.Context
{
    public class ProjetoBaseContext : DbContext
    {
        public ProjetoBaseContext(DbContextOptions<ProjetoBaseContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<UnidadeMedida> UnidadesMedida { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new FornecedorMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new UnidadeMedidaMap());
            modelBuilder.ApplyConfiguration(new PedidoMap());
            modelBuilder.ApplyConfiguration(new ItemPedidoMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
