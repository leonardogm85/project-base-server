using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoBase.Domain.Entities;

namespace ProjetoBase.Infrastructure.Data.Mappings
{
    public class ItemPedidoMap : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Quantidade)
                .HasColumnType("decimal(11,2)")
                .IsRequired();

            builder.Property(i => i.Valor)
                .HasColumnType("decimal(11,2)")
                .IsRequired();

            builder.Property(i => i.Desconto)
                .HasColumnType("decimal(11,2)")
                .IsRequired();

            builder.Property(i => i.Subtotal)
                .HasColumnType("decimal(11,2)")
                .IsRequired();

            builder.Property(i => i.Total)
                .HasColumnType("decimal(11,2)")
                .IsRequired();

            builder.HasOne(i => i.Produto)
               .WithMany(p => p.ItensPedido)
               .HasForeignKey(i => i.ProdutoId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();

            builder.HasOne(i => i.Pedido)
               .WithMany(p => p.Itens)
               .HasForeignKey(i => i.PedidoId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();

            builder.Property(c => c.Ativo)
                .IsRequired();

            builder.Property(c => c.Versao)
                .IsRowVersion()
                .IsRequired();

            builder.ToTable("ItensPedido");
        }
    }
}
