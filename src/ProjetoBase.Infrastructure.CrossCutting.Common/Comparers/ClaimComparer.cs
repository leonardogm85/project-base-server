using System.Collections.Generic;
using System.Security.Claims;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Security
{
    public class ClaimComparer : IEqualityComparer<Claim>
    {
        public bool Equals(Claim x, Claim y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            if (x.Type == y.Type && x.Value == y.Value)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(Claim claim)
        {
            if (claim == null)
            {
                return 0;
            }

            var hashType = claim.Type == null ?
                0 :
                claim.Type.GetHashCode();

            var hashValue = claim.Value == null ?
                0 :
                claim.Value.GetHashCode();

            return hashType ^ hashValue;
        }
    }
}
