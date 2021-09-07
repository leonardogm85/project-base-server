using System;

namespace ProjetoBase.Domain.DataTransferObjects
{
    public class UnidadeMedidaTable : DataTransferObject
    {
        public UnidadeMedidaTable(Guid id, string nome, string sigla, bool ativo)
        {
            Id = id;
            Nome = nome;
            Sigla = sigla;
            Ativo = ativo;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Sigla { get; private set; }
        public bool Ativo { get; private set; }
    }
}
