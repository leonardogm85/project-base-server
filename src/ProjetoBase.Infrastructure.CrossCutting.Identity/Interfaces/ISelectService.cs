using ProjetoBase.Infrastructure.CrossCutting.Common.Selects;
using System;
using System.Threading.Tasks;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Interfaces
{
    public interface ISelectService<TKey, TValue>
        where TKey : IEquatable<TKey>
        where TValue : IEquatable<TValue>
    {
        Task<SelectResult<TKey, TValue>> GetSelectAsync(params TKey[] identities);
        Task<SelectResult<TKey, TValue>> GetSelectAsync(SelectFilter filter);
    }
}
