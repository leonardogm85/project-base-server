using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Login
{
    public class LoginResult
    {
        protected LoginResult()
        {
        }

        public LoginResult(bool authenticated, DateTime? created, DateTime? expires, string token, string message)
        {
            Authenticated = authenticated;
            Created = created;
            Expires = expires;
            Token = token;
            Message = message;
        }

        public bool Authenticated { get; protected set; }
        public DateTime? Created { get; protected set; }
        public DateTime? Expires { get; protected set; }
        public string Token { get; protected set; }
        public string Message { get; protected set; }
    }
}
