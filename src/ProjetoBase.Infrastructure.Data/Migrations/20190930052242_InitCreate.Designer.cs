﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetoBase.Infrastructure.Data.Context;

namespace ProjetoBase.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ProjetoBaseContext))]
    [Migration("20190930052242_InitCreate")]
    partial class InitCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjetoBase.Domain.Entities.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Apelido")
                        .HasColumnType("varchar(250)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Documento")
                        .IsRequired()
                        .HasColumnType("varchar(18)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Telefone")
                        .HasColumnType("varchar(15)");

                    b.Property<int>("TipoPessoa")
                        .HasColumnType("int");

                    b.Property<byte[]>("Versao")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("Documento")
                        .IsUnique();

                    b.HasIndex("Email");

                    b.HasIndex("Nome");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("ProjetoBase.Domain.Entities.Fornecedor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Apelido")
                        .HasColumnType("varchar(250)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Documento")
                        .IsRequired()
                        .HasColumnType("varchar(18)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Telefone")
                        .HasColumnType("varchar(15)");

                    b.Property<int>("TipoPessoa")
                        .HasColumnType("int");

                    b.Property<byte[]>("Versao")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("Documento")
                        .IsUnique();

                    b.HasIndex("Email");

                    b.HasIndex("Nome");

                    b.ToTable("Fornecedores");
                });

            modelBuilder.Entity("ProjetoBase.Domain.Entities.ItemPedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<decimal>("Desconto")
                        .HasColumnType("decimal(11,2)");

                    b.Property<Guid>("PedidoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProdutoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Quantidade")
                        .HasColumnType("decimal(11,2)");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(11,2)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(11,2)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(11,2)");

                    b.Property<byte[]>("Versao")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ItensPedido");
                });

            modelBuilder.Entity("ProjetoBase.Domain.Entities.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataEntrega")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataPedido")
                        .HasColumnType("datetime");

                    b.Property<decimal>("Desconto")
                        .HasColumnType("decimal(11,2)");

                    b.Property<string>("Observacao")
                        .HasColumnType("varchar(900)");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(11,2)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(11,2)");

                    b.Property<byte[]>("Versao")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("ProjetoBase.Domain.Entities.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(900)");

                    b.Property<Guid>("FornecedorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("UnidadeMedidaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(11,2)");

                    b.Property<byte[]>("Versao")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.HasIndex("UnidadeMedidaId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("ProjetoBase.Domain.Entities.UnidadeMedida", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasColumnType("varchar(5)");

                    b.Property<byte[]>("Versao")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("Sigla")
                        .IsUnique();

                    b.ToTable("UnidadesMedida");
                });

            modelBuilder.Entity("ProjetoBase.Domain.Entities.Cliente", b =>
                {
                    b.OwnsOne("ProjetoBase.Domain.ValueObjects.Endereco", "Endereco", b1 =>
                        {
                            b1.Property<Guid>("ClienteId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Bairro")
                                .IsRequired()
                                .HasColumnName("Bairro")
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Cep")
                                .IsRequired()
                                .HasColumnName("Cep")
                                .HasColumnType("varchar(10)");

                            b1.Property<string>("Cidade")
                                .IsRequired()
                                .HasColumnName("Cidade")
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Complemento")
                                .HasColumnName("Complemento")
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("Estado")
                                .IsRequired()
                                .HasColumnName("Estado")
                                .HasColumnType("varchar(2)");

                            b1.Property<string>("Logradouro")
                                .IsRequired()
                                .HasColumnName("Logradouro")
                                .HasColumnType("varchar(100)");

                            b1.Property<int>("Numero")
                                .HasColumnName("Numero")
                                .HasColumnType("int");

                            b1.HasKey("ClienteId");

                            b1.ToTable("Clientes");

                            b1.WithOwner()
                                .HasForeignKey("ClienteId");
                        });
                });

            modelBuilder.Entity("ProjetoBase.Domain.Entities.Fornecedor", b =>
                {
                    b.OwnsOne("ProjetoBase.Domain.ValueObjects.Endereco", "Endereco", b1 =>
                        {
                            b1.Property<Guid>("FornecedorId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Bairro")
                                .IsRequired()
                                .HasColumnName("Bairro")
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Cep")
                                .IsRequired()
                                .HasColumnName("Cep")
                                .HasColumnType("varchar(10)");

                            b1.Property<string>("Cidade")
                                .IsRequired()
                                .HasColumnName("Cidade")
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Complemento")
                                .HasColumnName("Complemento")
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("Estado")
                                .IsRequired()
                                .HasColumnName("Estado")
                                .HasColumnType("varchar(2)");

                            b1.Property<string>("Logradouro")
                                .IsRequired()
                                .HasColumnName("Logradouro")
                                .HasColumnType("varchar(100)");

                            b1.Property<int>("Numero")
                                .HasColumnName("Numero")
                                .HasColumnType("int");

                            b1.HasKey("FornecedorId");

                            b1.ToTable("Fornecedores");

                            b1.WithOwner()
                                .HasForeignKey("FornecedorId");
                        });
                });

            modelBuilder.Entity("ProjetoBase.Domain.Entities.ItemPedido", b =>
                {
                    b.HasOne("ProjetoBase.Domain.Entities.Pedido", "Pedido")
                        .WithMany("ItensPedido")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjetoBase.Domain.Entities.Produto", "Produto")
                        .WithMany("ItensPedido")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjetoBase.Domain.Entities.Pedido", b =>
                {
                    b.HasOne("ProjetoBase.Domain.Entities.Cliente", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjetoBase.Domain.Entities.Produto", b =>
                {
                    b.HasOne("ProjetoBase.Domain.Entities.Fornecedor", "Fornecedor")
                        .WithMany("Produtos")
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjetoBase.Domain.Entities.UnidadeMedida", "UnidadeMedida")
                        .WithMany("Produtos")
                        .HasForeignKey("UnidadeMedidaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
