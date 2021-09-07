using System;

namespace ProjetoBase.Application.ViewModels.Fornecedores
{
    public class FornecedorTableViewModel : ViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public bool Ativo { get; set; }
    }
}
