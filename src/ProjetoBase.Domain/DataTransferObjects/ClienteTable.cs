using System;

namespace ProjetoBase.Domain.DataTransferObjects
{
    public class ClienteTable : DataTransferObject
    {
        public ClienteTable(Guid id, string nome, string documento, string email, string celular, bool ativo)
        {
            Id = id;
            Nome = nome;
            Documento = documento;
            Email = email;
            Celular = celular;
            Ativo = ativo;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Documento { get; private set; }
        public string Email { get; private set; }
        public string Celular { get; private set; }
        public bool Ativo { get; private set; }
    }
}
