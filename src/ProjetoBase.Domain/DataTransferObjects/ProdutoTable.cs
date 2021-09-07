using System;

namespace ProjetoBase.Domain.DataTransferObjects
{
    public class ProdutoTable : DataTransferObject
    {
        public ProdutoTable(Guid id, string nome, string nomeFornecedor, string siglaUnidadeMedida, decimal valor, bool ativo)
        {
            Id = id;
            Nome = nome;
            NomeFornecedor = nomeFornecedor;
            SiglaUnidadeMedida = siglaUnidadeMedida;
            Valor = valor;
            Ativo = ativo;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string NomeFornecedor { get; private set; }
        public string SiglaUnidadeMedida { get; private set; }
        public decimal Valor { get; private set; }
        public bool Ativo { get; private set; }
    }
}
