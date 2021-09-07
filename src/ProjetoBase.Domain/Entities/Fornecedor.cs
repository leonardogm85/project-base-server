using ProjetoBase.Domain.Enums;
using ProjetoBase.Domain.ValueObjects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;
using System.Collections.Generic;

namespace ProjetoBase.Domain.Entities
{
    public class Fornecedor : Pessoa
    {
        protected Fornecedor()
        {
        }

        public Fornecedor(Guid id, bool ativo, byte[] versao, TipoPessoa tipoPessoa, string apelido, string nome, string documento, string email, string celular,
                          string telefone, Endereco endereco)
            : base(id, ativo, versao, tipoPessoa, apelido, nome, documento, email, celular, telefone, endereco)
        {
        }

        public virtual IEnumerable<Produto> Produtos { get; private set; }

        public override NotificationResult Validar() => base.Validar();
    }
}
