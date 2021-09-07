using System;

namespace ProjetoBase.Application.ViewModels.Produtos
{
    public class ProdutoViewModel : ViewModel
    {
        public Guid? Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public Guid UnidadeMedidaId { get; set; }
        public Guid FornecedorId { get; set; }
        public bool Ativo { get; set; }
        public byte[] Versao { get; set; }
    }
}
