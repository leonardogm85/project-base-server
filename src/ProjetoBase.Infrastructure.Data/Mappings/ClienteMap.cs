using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoBase.Domain.Entities;

namespace ProjetoBase.Infrastructure.Data.Mappings
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.TipoPessoa);

            builder.Property(c => c.Apelido)
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Nome)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder.Property(c => c.Documento)
                .HasColumnType("varchar(18)")
                .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder.Property(c => c.Celular)
                .HasColumnType("varchar(15)")
                .IsRequired();

            builder.Property(c => c.Telefone)
                .HasColumnType("varchar(15)");

            builder.OwnsOne(c => c.Endereco, endereco =>
            {
                endereco.Property(e => e.Cep)
                    .HasColumnType("varchar(10)")
                    .HasColumnName("Cep")
                    .IsRequired();

                endereco.Property(e => e.Logradouro)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("Logradouro")
                    .IsRequired();

                endereco.Property(e => e.Numero)
                    .HasColumnName("Numero")
                    .IsRequired();

                endereco.Property(e => e.Complemento)
                    .HasColumnType("varchar(50)")
                    .HasColumnName("Complemento");

                endereco.Property(e => e.Bairro)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("Bairro")
                    .IsRequired();

                endereco.Property(e => e.Cidade)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("Cidade")
                    .IsRequired();

                endereco.Property(e => e.Estado)
                    .HasColumnType("varchar(2)")
                    .HasColumnName("Estado")
                    .IsRequired();
            });

            builder.Property(c => c.Ativo)
                .IsRequired();

            builder.Property(c => c.Versao)
                .IsRowVersion()
                .IsRequired();

            builder.HasIndex(c => c.Nome);

            builder.HasIndex(c => c.Documento)
                .IsUnique();

            builder.HasIndex(c => c.Email);

            builder.ToTable("Clientes");
        }
    }
}
