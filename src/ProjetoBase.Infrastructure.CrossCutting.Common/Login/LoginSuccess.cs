using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Login
{
    public class LoginSuccess : LoginResult
    {
        public LoginSuccess(DateTime expires, string token)
        {
            Authenticated = true;
            Created = DateTime.Now;
            Expires = expires;
            Token = token;
        }
    }
}
