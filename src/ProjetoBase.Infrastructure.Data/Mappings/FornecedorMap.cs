using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoBase.Domain.Entities;

namespace ProjetoBase.Infrastructure.Data.Mappings
{
    public class FornecedorMap : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.TipoPessoa);

            builder.Property(f => f.Apelido)
                .HasColumnType("varchar(250)");

            builder.Property(f => f.Nome)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder.Property(f => f.Documento)
                .HasColumnType("varchar(18)")
                .IsRequired();

            builder.Property(f => f.Email)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder.Property(f => f.Celular)
                .HasColumnType("varchar(15)")
                .IsRequired();

            builder.Property(f => f.Telefone)
                .HasColumnType("varchar(15)");

            builder.OwnsOne(f => f.Endereco, endereco =>
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

            builder.Property(f => f.Ativo)
                .IsRequired();

            builder.Property(f => f.Versao)
                .IsRowVersion()
                .IsRequired();

            builder.HasIndex(f => f.Nome);

            builder.HasIndex(f => f.Documento)
                .IsUnique();

            builder.HasIndex(f => f.Email);

            builder.ToTable("Fornecedores");
        }
    }
}
