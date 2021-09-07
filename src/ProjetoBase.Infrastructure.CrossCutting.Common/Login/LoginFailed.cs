namespace ProjetoBase.Infrastructure.CrossCutting.Common.Login
{
    public class LoginFailed : LoginResult
    {
        public LoginFailed(string message)
        {
            Authenticated = false;
            Message = message;
        }
    }
}
