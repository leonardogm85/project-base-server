using ProjetoBase.Infrastructure.CrossCutting.Common.Attributes;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Enums
{
    public enum Item : int
    {
        [Description("Unidades de medida")]
        [Attachment(Menu.Cadastros)]
        [Order(1)]
        [Address("unidades-medida")]
        UnidadesMedida = 101,

        [Description("Produtos")]
        [Attachment(Menu.Cadastros)]
        [Order(2)]
        [Address("produtos")]
        Produtos = 102,

        [Description("Clientes")]
        [Attachment(Menu.Cadastros)]
        [Order(3)]
        [Address("clientes")]
        Clientes = 103,

        [Description("Fornecedores")]
        [Attachment(Menu.Cadastros)]
        [Order(4)]
        [Address("fornecedores")]
        Fornecedores = 104,

        [Description("Pedidos")]
        [Attachment(Menu.Controles)]
        [Order(1)]
        [Address("pedidos")]
        Pedidos = 201,

        [Description("Usuários")]
        [Attachment(Menu.Seguranca)]
        [Order(1)]
        [Address("usuarios")]
        Usuarios = 401,

        [Description("Papéis")]
        [Attachment(Menu.Seguranca)]
        [Order(2)]
        [Address("papeis")]
        Papeis = 402
    }
}
