using ProjetoBase.Infrastructure.CrossCutting.Common.Attributes;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Enums
{
    public enum Access : int
    {
        [Description("Adicionar")]
        Create = 1,

        [Description("Visualizar")]
        Read = 2,

        [Description("Atualizar")]
        Update = 3,

        [Description("Remover")]
        Delete = 4
    }
}
