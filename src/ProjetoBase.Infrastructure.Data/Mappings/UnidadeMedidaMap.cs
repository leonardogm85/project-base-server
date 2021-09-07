using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoBase.Domain.Entities;

namespace ProjetoBase.Infrastructure.Data.Mappings
{
    public class UnidadeMedidaMap : IEntityTypeConfiguration<UnidadeMedida>
    {
        public void Configure(EntityTypeBuilder<UnidadeMedida> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Nome)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(u => u.Sigla)
                .HasColumnType("varchar(5)")
                .IsRequired();

            builder.Property(u => u.Ativo)
                .IsRequired();

            builder.Property(u => u.Versao)
                .IsRowVersion()
                .IsRequired();

            builder.HasIndex(u => u.Sigla)
                .IsUnique();

            builder.ToTable("UnidadesMedida");
        }
    }
}
