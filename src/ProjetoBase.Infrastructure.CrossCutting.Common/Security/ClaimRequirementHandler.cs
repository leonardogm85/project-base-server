using Microsoft.AspNetCore.Authorization;
using ProjetoBase.Infrastructure.CrossCutting.Common.Interfaces;
using System.Threading.Tasks;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Security
{
    public class ClaimRequirementHandler : AuthorizationHandler<ClaimRequirement>
    {
        private readonly IAuthService _authService;

        public ClaimRequirementHandler(IAuthService authService) => _authService = authService;

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimRequirement requirement)
        {
            if (_authService.IsAuthorized(requirement.Value, requirement.Types))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
