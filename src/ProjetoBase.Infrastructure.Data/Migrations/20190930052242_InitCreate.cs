using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoBase.Infrastructure.Data.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Versao = table.Column<byte[]>(rowVersion: true, nullable: false),
                    TipoPessoa = table.Column<int>(nullable: false),
                    Apelido = table.Column<string>(type: "varchar(250)", nullable: true),
                    Nome = table.Column<string>(type: "varchar(250)", nullable: false),
                    Documento = table.Column<string>(type: "varchar(18)", nullable: false),
                    Email = table.Column<string>(type: "varchar(250)", nullable: false),
                    Celular = table.Column<string>(type: "varchar(15)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(15)", nullable: true),
                    Cep = table.Column<string>(type: "varchar(10)", nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(100)", nullable: true),
                    Numero = table.Column<int>(nullable: true),
                    Complemento = table.Column<string>(type: "varchar(50)", nullable: true),
                    Bairro = table.Column<string>(type: "varchar(100)", nullable: true),
                    Cidade = table.Column<string>(type: "varchar(100)", nullable: true),
                    Estado = table.Column<string>(type: "varchar(2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Versao = table.Column<byte[]>(rowVersion: true, nullable: false),
                    TipoPessoa = table.Column<int>(nullable: false),
                    Apelido = table.Column<string>(type: "varchar(250)", nullable: true),
                    Nome = table.Column<string>(type: "varchar(250)", nullable: false),
                    Documento = table.Column<string>(type: "varchar(18)", nullable: false),
                    Email = table.Column<string>(type: "varchar(250)", nullable: false),
                    Celular = table.Column<string>(type: "varchar(15)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(15)", nullable: true),
                    Cep = table.Column<string>(type: "varchar(10)", nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(100)", nullable: true),
                    Numero = table.Column<int>(nullable: true),
                    Complemento = table.Column<string>(type: "varchar(50)", nullable: true),
                    Bairro = table.Column<string>(type: "varchar(100)", nullable: true),
                    Cidade = table.Column<string>(type: "varchar(100)", nullable: true),
                    Estado = table.Column<string>(type: "varchar(2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadesMedida",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Versao = table.Column<byte[]>(rowVersion: true, nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    Sigla = table.Column<string>(type: "varchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesMedida", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Versao = table.Column<byte[]>(rowVersion: true, nullable: false),
                    DataPedido = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataEntrega = table.Column<DateTime>(type: "datetime", nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    Observacao = table.Column<string>(type: "varchar(900)", nullable: true),
                    ClienteId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Versao = table.Column<byte[]>(rowVersion: true, nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(900)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    UnidadeMedidaId = table.Column<Guid>(nullable: false),
                    FornecedorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produtos_UnidadesMedida_UnidadeMedidaId",
                        column: x => x.UnidadeMedidaId,
                        principalTable: "UnidadesMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItensPedido",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Versao = table.Column<byte[]>(rowVersion: true, nullable: false),
                    Quantidade = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    ProdutoId = table.Column<Guid>(nullable: false),
                    PedidoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensPedido_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItensPedido_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Documento",
                table: "Clientes",
                column: "Documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Email",
                table: "Clientes",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Nome",
                table: "Clientes",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_Documento",
                table: "Fornecedores",
                column: "Documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_Email",
                table: "Fornecedores",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_Nome",
                table: "Fornecedores",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedido_PedidoId",
                table: "ItensPedido",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedido_ProdutoId",
                table: "ItensPedido",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedidos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_FornecedorId",
                table: "Produtos",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_Nome",
                table: "Produtos",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_UnidadeMedidaId",
                table: "Produtos",
                column: "UnidadeMedidaId");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadesMedida_Sigla",
                table: "UnidadesMedida",
                column: "Sigla",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensPedido");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Fornecedores");

            migrationBuilder.DropTable(
                name: "UnidadesMedida");
        }
    }
}
