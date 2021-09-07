using ProjetoBase.Infrastructure.CrossCutting.Common.Attributes;

namespace ProjetoBase.Domain.Enums
{
    public enum TipoPessoa : int
    {
        [Description("Pessoa física")]
        PessoaFisica = 1,

        [Description("Pessoa jurídica")]
        PessoaJuridica = 2
    }
}
