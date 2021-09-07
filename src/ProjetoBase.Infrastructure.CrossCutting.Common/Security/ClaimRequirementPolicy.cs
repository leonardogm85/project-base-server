using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Security
{
    public class ClaimRequirementPolicy : DefaultAuthorizationPolicyProvider
    {
        public ClaimRequirementPolicy(IOptions<AuthorizationOptions> options)
            : base(options)
        {
        }

        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            var pattern = @"^claim\-(\d+)\-(\d+(\.\d+)*)$";

            if (Regex.IsMatch(policyName, pattern))
            {
                var value = Regex.Replace(policyName, pattern, "$1");
                var types = Regex.Replace(policyName, pattern, "$2").Split(".");

                var policy = new AuthorizationPolicyBuilder();

                policy.RequireAuthenticatedUser();

                policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);

                policy.AddRequirements(new ClaimRequirement(value, types));

                return await Task.FromResult(policy.Build());
            }

            return await base.GetPolicyAsync(policyName);
        }
    }
}
