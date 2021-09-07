using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Constants;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;
using System.Collections.Generic;

namespace ProjetoBase.Domain.Entities
{
    public class Produto : Entity
    {
        protected Produto()
        {
        }

        public Produto(Guid id, bool ativo, byte[] versao, string nome, string descricao, decimal valor, Guid unidadeMedidaId, Guid fornecedorId)
            : base(id, ativo, versao)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            UnidadeMedidaId = unidadeMedidaId;
            FornecedorId = fornecedorId;
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public Guid UnidadeMedidaId { get; private set; }
        public Guid FornecedorId { get; private set; }
        public virtual UnidadeMedida UnidadeMedida { get; private set; }
        public virtual Fornecedor Fornecedor { get; private set; }
        public virtual IEnumerable<ItemPedido> ItensPedido { get; private set; }

        public override NotificationResult Validar()
        {
            var result = new NotificationResult();

            result.AddNotifications(new NotificationContract()
                .IsntEmpty(Id, ValidationMessages.EntidadeCodigoDeveSerPreenchido)

                .IsntNullOrWhiteSpace(Nome, ValidationMessages.ProdutoNomeDeveSerPreenchido)
                .HasMaxLength(Nome, 50, ValidationMessages.ProdutoNomeDeveTerUmTamanhoMaximo)

                .IsntNullOrWhiteSpace(Descricao, ValidationMessages.ProdutoDescricaoDeveSerPreenchido)
                .HasMaxLength(Descricao, 900, ValidationMessages.ProdutoDescricaoDeveTerUmTamanhoMaximo)

                .IsBetween(Valor, 0, 999999999, ValidationMessages.ProdutoValorDeveEstarEmUmIntervalo)

                .IsntEmpty(UnidadeMedidaId, ValidationMessages.ProdutoUnidadeMedidaDeveSerPreenchido)

                .IsntEmpty(FornecedorId, ValidationMessages.ProdutoFornecedorDeveSerPreenchido));

            return result;
        }
    }
}
