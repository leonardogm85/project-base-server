using ProjetoBase.Domain.Enums;
using ProjetoBase.Domain.ValueObjects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;
using System.Collections.Generic;

namespace ProjetoBase.Domain.Entities
{
    public class Cliente : Pessoa
    {
        protected Cliente()
        {
        }

        public Cliente(Guid id, bool ativo, byte[] versao, TipoPessoa tipoPessoa, string apelido, string nome, string documento, string email, string celular,
                       string telefone, Endereco endereco)
            : base(id, ativo, versao, tipoPessoa, apelido, nome, documento, email, celular, telefone, endereco)
        {
        }

        public virtual IEnumerable<Pedido> Pedidos { get; private set; }

        public override NotificationResult Validar() => base.Validar();
    }
}
