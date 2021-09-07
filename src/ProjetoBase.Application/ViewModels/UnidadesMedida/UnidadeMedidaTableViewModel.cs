using System;

namespace ProjetoBase.Application.ViewModels.UnidadesMedida
{
    public class UnidadeMedidaTableViewModel : ViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public bool Ativo { get; set; }
    }
}
