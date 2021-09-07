using Microsoft.AspNetCore.Authorization;
using ProjetoBase.Infrastructure.CrossCutting.Common.Enums;
using ProjetoBase.Infrastructure.CrossCutting.Common.Extensions;
using System.Linq;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Security
{
    public class ClaimRequirementAttribute : AuthorizeAttribute
    {
        public ClaimRequirementAttribute(Access access, params Item[] items) =>
            Policy = $"claim-{access.GetValue<int>()}-{string.Join(".", items.Select(i => i.GetValue<int>()))}";
    }
}
