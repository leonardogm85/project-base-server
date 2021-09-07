using System;

namespace ProjetoBase.Application.ViewModels.Produtos
{
    public class ProdutoTableViewModel : ViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string NomeFornecedor { get; set; }
        public string SiglaUnidadeMedida { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
    }
}
