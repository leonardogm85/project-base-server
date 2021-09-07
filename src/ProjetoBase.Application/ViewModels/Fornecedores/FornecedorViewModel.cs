using ProjetoBase.Application.ViewModels.Enderecos;
using System;

namespace ProjetoBase.Application.ViewModels.Fornecedores
{
    public class FornecedorViewModel : ViewModel
    {
        public Guid? Id { get; set; }
        public int TipoPessoa { get; set; }
        public string Apelido { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Telefone { get; set; }
        public EnderecoViewModel Endereco { get; set; }
        public bool Ativo { get; set; }
        public byte[] Versao { get; set; }
    }
}
