namespace ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects
{
    public class ForgotPassword : DataTransferObject
    {
        public ForgotPassword(string email) => Email = email;

        public string Email { get; private set; }
    }
}
