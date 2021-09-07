using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Security
{
    public class SigningConfiguration
    {
        public SigningConfiguration()
        {
            using var provider = new RSACryptoServiceProvider(2048);
            SecurityKey = new RsaSecurityKey(provider.ExportParameters(true));
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.RsaSha256Signature);
        }

        public SecurityKey SecurityKey { get; private set; }
        public SigningCredentials SigningCredentials { get; private set; }
    }
}
