using Microsoft.EntityFrameworkCore;
using ProjetoBase.Domain.Entities;
using ProjetoBase.Domain.Interfaces.Repositories;
using ProjetoBase.Infrastructure.Data.Context;
using System;
using System.Linq;

namespace ProjetoBase.Infrastructure.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        protected readonly ProjetoBaseContext DbContext;
        protected readonly DbSet<TEntity> DbEntity;

        protected Repository(ProjetoBaseContext context)
        {
            DbContext = context;
            DbEntity = DbContext.Set<TEntity>();
        }

        public void Adicionar(TEntity entidade) => DbEntity.Add(entidade);

        public void Atualizar(TEntity entidade) => DbEntity.Update(entidade);

        public void Remover(TEntity entidade) => DbEntity.Remove(entidade);

        public TEntity ObterPorId(Guid id) => DbEntity.AsNoTracking().FirstOrDefault(e => e.Id == id);

        public bool Ativo(Guid id) => DbEntity.AsNoTracking().Where(e => e.Id == id).Select(e => e.Ativo).FirstOrDefault();

        public bool Existe(Guid id) => DbEntity.AsNoTracking().Any(e => e.Id == id);

        public void Dispose()
        {
            DbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
