namespace ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects
{
    public class UpdatePassword : DataTransferObject
    {
        public UpdatePassword(string oldPassword, string newPassword)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }

        public string OldPassword { get; private set; }
        public string NewPassword { get; private set; }
    }
}
