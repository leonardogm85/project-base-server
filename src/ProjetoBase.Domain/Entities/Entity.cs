using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;

namespace ProjetoBase.Domain.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
        }

        protected Entity(Guid id, bool ativo, byte[] versao)
        {
            Id = id;
            Ativo = ativo;
            Versao = versao;
        }

        public Guid Id { get; private set; }
        public bool Ativo { get; private set; }
        public byte[] Versao { get; private set; }

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        public abstract NotificationResult Validar();
    }
}
