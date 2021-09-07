using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Constants;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;

namespace ProjetoBase.Domain.Entities
{
    public class ItemPedido : Entity
    {
        protected ItemPedido()
        {
        }

        public ItemPedido(Guid id, bool ativo, byte[] versao, decimal quantidade, decimal valor, decimal desconto, decimal subtotal, decimal total, Guid produtoId,
                          Guid pedidoId)
            : base(id, ativo, versao)
        {
            Quantidade = quantidade;
            Valor = valor;
            Desconto = desconto;
            Subtotal = subtotal;
            Total = total;
            ProdutoId = produtoId;
            PedidoId = pedidoId;
        }

        public decimal Quantidade { get; private set; }
        public decimal Valor { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal Subtotal { get; private set; }
        public decimal Total { get; private set; }
        public Guid ProdutoId { get; private set; }
        public Guid PedidoId { get; private set; }
        public virtual Produto Produto { get; private set; }
        public virtual Pedido Pedido { get; private set; }

        public override NotificationResult Validar()
        {
            var calculoSubtotal = Quantidade * Valor;
            var calculoTotal = Subtotal - Desconto;

            var result = new NotificationResult();

            result.AddNotifications(new NotificationContract()
                .IsntEmpty(Id, ValidationMessages.EntidadeCodigoDeveSerPreenchido)

                .IsGreaterThan(Quantidade, 0, ValidationMessages.ItemPedidoQuantidadeDeveTerUmTamanhoMinimo)
                .IsLowerOrEqualsThan(Quantidade, 999999999, ValidationMessages.ItemPedidoQuantidadeDeveTerUmTamanhoMaximo)

                .IsBetween(Desconto, 0, Subtotal, ValidationMessages.ItemPedidoDescontoDeveEstarEmUmIntervalo)

                .AreEquals(Subtotal, calculoSubtotal, ValidationMessages.ItemPedidoSubtotalDeveTerUmTamanhoExato)

                .AreEquals(Total, calculoTotal, ValidationMessages.ItemPedidoTotalDeveTerUmTamanhoExato)

                .IsntEmpty(ProdutoId, ValidationMessages.ItemPedidoProdutoDeveSerPreenchido)

                .IsntEmpty(PedidoId, ValidationMessages.ItemPedidoPedidoDeveSerAssociado));

            return result;
        }
    }
}
