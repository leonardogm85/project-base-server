using ProjetoBase.Domain.Entities;
using System;

namespace ProjetoBase.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : Entity
    {
        void Adicionar(TEntity entidade);
        void Atualizar(TEntity entidade);
        void Remover(TEntity entidade);
        TEntity ObterPorId(Guid id);
        bool Ativo(Guid id);
        bool Existe(Guid id);
    }
}
