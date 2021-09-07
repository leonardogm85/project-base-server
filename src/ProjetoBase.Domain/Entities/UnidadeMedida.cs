using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Constants;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;
using System.Collections.Generic;

namespace ProjetoBase.Domain.Entities
{
    public class UnidadeMedida : Entity
    {
        protected UnidadeMedida()
        {
        }

        public UnidadeMedida(Guid id, bool ativo, byte[] versao, string nome, string sigla)
            : base(id, ativo, versao)
        {
            Nome = nome;
            Sigla = sigla;
        }

        public string Nome { get; private set; }
        public string Sigla { get; private set; }
        public virtual IEnumerable<Produto> Produtos { get; private set; }

        public override NotificationResult Validar()
        {
            var result = new NotificationResult();

            result.AddNotifications(new NotificationContract()
                .IsntEmpty(Id, ValidationMessages.EntidadeCodigoDeveSerPreenchido)

                .IsntNullOrWhiteSpace(Nome, ValidationMessages.UnidadeMedidaNomeDeveSerPreenchido)
                .HasMaxLength(Nome, 50, ValidationMessages.UnidadeMedidaNomeDeveTerUmTamanhoMaximo)

                .IsntNullOrWhiteSpace(Sigla, ValidationMessages.UnidadeMedidaSiglaDeveSerPreenchido)
                .HasMaxLength(Sigla, 5, ValidationMessages.UnidadeMedidaSiglaDeveTerUmTamanhoMaximo));

            return result;
        }
    }
}
