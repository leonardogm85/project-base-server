using ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Interfaces
{
    public interface IMenuService : IDisposable
    {
        Task<IEnumerable<AccessMenu>> GetAuthenticatedUserMenusAsync();
    }
}
