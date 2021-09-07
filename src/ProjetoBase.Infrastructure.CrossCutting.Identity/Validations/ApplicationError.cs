using Microsoft.AspNetCore.Identity;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Validations
{
    public class ApplicationError : IdentityError
    {
        public ApplicationError(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
