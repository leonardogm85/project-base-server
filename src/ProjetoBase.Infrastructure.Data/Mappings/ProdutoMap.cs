using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoBase.Domain.Entities;

namespace ProjetoBase.Infrastructure.Data.Mappings
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(p => p.Descricao)
                .HasColumnType("varchar(900)");

            builder.Property(p => p.Valor)
                .HasColumnType("decimal(11,2)");

            builder.HasOne(p => p.UnidadeMedida)
               .WithMany(u => u.Produtos)
               .HasForeignKey(p => p.UnidadeMedidaId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();

            builder.HasOne(p => p.Fornecedor)
               .WithMany(f => f.Produtos)
               .HasForeignKey(p => p.FornecedorId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();

            builder.Property(p => p.Ativo)
                .IsRequired();

            builder.Property(p => p.Versao)
                .IsRowVersion()
                .IsRequired();

            builder.HasIndex(p => p.Nome)
                .IsUnique();

            builder.ToTable("Produtos");
        }
    }
}
