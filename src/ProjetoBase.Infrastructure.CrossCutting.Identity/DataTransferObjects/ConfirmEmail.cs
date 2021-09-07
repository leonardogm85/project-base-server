using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects
{
    public class ConfirmEmail : DataTransferObject
    {
        public ConfirmEmail(Guid id, string token)
        {
            Id = id;
            Token = token;
        }

        public Guid Id { get; private set; }
        public string Token { get; private set; }
    }
}
