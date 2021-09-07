using ProjetoBase.Domain.Entities;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;

namespace ProjetoBase.Domain.Interfaces.Services
{
    public interface IService<TEntity> : IDisposable
        where TEntity : Entity
    {
        NotificationResult Adicionar(TEntity entidade);
        NotificationResult Atualizar(TEntity entidade);
        NotificationResult Remover(Guid id);
        NotificationResult Ativar(Guid id);
        NotificationResult Desativar(Guid id);
        TEntity ObterPorId(Guid id);
    }
}
