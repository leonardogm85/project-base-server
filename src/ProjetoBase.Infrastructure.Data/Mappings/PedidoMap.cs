using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoBase.Domain.Entities;

namespace ProjetoBase.Infrastructure.Data.Mappings
{
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.DataPedido)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(p => p.DataEntrega)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(p => p.Desconto)
                .HasColumnType("decimal(11,2)")
                .IsRequired();

            builder.Property(p => p.Subtotal)
                .HasColumnType("decimal(11,2)")
                .IsRequired();

            builder.Property(p => p.Total)
                .HasColumnType("decimal(11,2)")
                .IsRequired();

            builder.Property(p => p.Observacao)
                .HasColumnType("varchar(900)");

            builder.HasOne(p => p.Cliente)
               .WithMany(c => c.Pedidos)
               .HasForeignKey(p => p.ClienteId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();

            builder.Property(c => c.Ativo)
                .IsRequired();

            builder.Property(c => c.Versao)
                .IsRowVersion()
                .IsRequired();

            builder.ToTable("Pedidos");
        }
    }
}
