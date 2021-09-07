using ProjetoBase.Infrastructure.CrossCutting.Common.Attributes;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Enums
{
    public enum Menu : int
    {
        [Description("Cadastros")]
        [Order(1)]
        Cadastros = 100,

        [Description("Controles")]
        [Order(2)]
        Controles = 200,

        [Description("Relatórios")]
        [Order(3)]
        Relatorios = 300,

        [Description("Segurança")]
        [Order(4)]
        Seguranca = 400
    }
}
