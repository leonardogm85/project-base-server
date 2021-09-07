using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Interfaces
{
    public interface IRoleService : IService<Role>, ITableService<RoleTable>, ISelectService<Guid, string>
    {
        Task<NotificationResult> AddRoleClaimsAsync(RoleClaim roleClaim);
        Task<RoleClaim> GetRoleClaimsAsync(Guid id);
        Task<IEnumerable<MenuClaim>> GetMenuClaimsAsync(Guid id);
    }
}
