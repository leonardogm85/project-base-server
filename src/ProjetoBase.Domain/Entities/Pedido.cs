using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Constants;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Helpers;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoBase.Domain.Entities
{
    public class Pedido : Entity
    {
        protected Pedido()
        {
        }

        public Pedido(Guid id, bool ativo, byte[] versao, DateTime dataPedido, DateTime dataEntrega, decimal desconto, decimal subtotal, decimal total,
                      string observacao, Guid clienteId)
            : base(id, ativo, versao)
        {
            DataPedido = dataPedido;
            DataEntrega = dataEntrega;
            Desconto = desconto;
            Subtotal = subtotal;
            Total = total;
            Observacao = observacao;
            ClienteId = clienteId;

            Itens = Enumerable.Empty<ItemPedido>();
        }

        public DateTime DataPedido { get; private set; }
        public DateTime DataEntrega { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal Subtotal { get; private set; }
        public decimal Total { get; private set; }
        public string Observacao { get; private set; }
        public Guid ClienteId { get; private set; }
        public virtual Cliente Cliente { get; private set; }
        public virtual IEnumerable<ItemPedido> Itens { get; private set; }

        public void AdicionarItem(ItemPedido item) => Itens = Itens.Append(item);

        public void AdicionarItens(IEnumerable<ItemPedido> itens) => Itens = Itens.Concat(itens);

        public void RemoverItem(Guid id) => Itens = Itens.SkipWhile(i => i.Id == id);

        public override NotificationResult Validar()
        {
            var result = new NotificationResult();

            var calculoSubtotal = Itens.Sum(item => item.Total);
            var calculoTotal = Subtotal - Desconto;

            result.AddNotifications(new NotificationContract()
                .IsntEmpty(Id, ValidationMessages.EntidadeCodigoDeveSerPreenchido)

                .IsGreaterOrEqualsThan(DataPedido, DateTimeValidation.DbMinValue, ValidationMessages.PedidoDataPedidoDeveTerUmValorMinimo)
                .IsLowerOrEqualsThan(DataPedido, DateTime.Today, ValidationMessages.PedidoDataPedidoDeveTerUmValorMaximo)

                .IsGreaterOrEqualsThan(DataEntrega, DataPedido, ValidationMessages.PedidoDataEntregaDeveTerUmValorMinimo)

                .IsBetween(Desconto, 0, Subtotal, ValidationMessages.PedidoDescontoDeveEstarEmUmIntervalo)

                .AreEquals(Subtotal, calculoSubtotal, ValidationMessages.PedidoSubtotalDeveTerUmTamanhoExato)

                .AreEquals(Total, calculoTotal, ValidationMessages.PedidoTotalDeveTerUmTamanhoExato)

                .HasMaxLength(Observacao, 900, ValidationMessages.PedidoObservacaoDeveTerUmTamanhoMaximo)

                .IsntEmpty(ClienteId, ValidationMessages.PedidoClienteDeveSerPreenchido)

                .IsntEmpty(Itens, ValidationMessages.PedidoItensPedidoDeveSerPreenchido));

            return result;
        }
    }
}
