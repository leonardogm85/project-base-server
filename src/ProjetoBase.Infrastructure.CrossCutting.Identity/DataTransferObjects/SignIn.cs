namespace ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects
{
    public class SignIn : DataTransferObject
    {
        public SignIn(string email, string password, bool isPersistent)
        {
            Email = email;
            Password = password;
            IsPersistent = isPersistent;
        }

        public string Email { get; private set; }
        public string Password { get; private set; }
        public bool IsPersistent { get; private set; }
    }
}
