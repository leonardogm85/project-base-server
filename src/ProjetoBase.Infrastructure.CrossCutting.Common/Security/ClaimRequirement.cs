using Microsoft.AspNetCore.Authorization;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Security
{
    public class ClaimRequirement : IAuthorizationRequirement
    {
        public ClaimRequirement(string value, string[] types)
        {
            Value = value;
            Types = types;
        }

        public string Value { get; private set; }
        public string[] Types { get; private set; }
    }
}
