namespace ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects
{
    public class ResetPassword : DataTransferObject
    {
        public ResetPassword(string email, string newPassword, string token)
        {
            Email = email;
            NewPassword = newPassword;
            Token = token;
        }

        public string Email { get; private set; }
        public string NewPassword { get; private set; }
        public string Token { get; private set; }
    }
}
