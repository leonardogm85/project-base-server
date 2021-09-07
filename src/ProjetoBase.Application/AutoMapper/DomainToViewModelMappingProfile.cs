using AutoMapper;
using ProjetoBase.Application.ViewModels.Clientes;
using ProjetoBase.Application.ViewModels.Contas;
using ProjetoBase.Application.ViewModels.Direitos;
using ProjetoBase.Application.ViewModels.Enderecos;
using ProjetoBase.Application.ViewModels.Fornecedores;
using ProjetoBase.Application.ViewModels.Menus;
using ProjetoBase.Application.ViewModels.Papeis;
using ProjetoBase.Application.ViewModels.Pedidos;
using ProjetoBase.Application.ViewModels.Produtos;
using ProjetoBase.Application.ViewModels.UnidadesMedida;
using ProjetoBase.Application.ViewModels.Usuarios;
using ProjetoBase.Domain.DataTransferObjects;
using ProjetoBase.Domain.Entities;
using ProjetoBase.Domain.ValueObjects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Tables;
using ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects;

namespace ProjetoBase.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Endereco, EnderecoViewModel>();

            CreateMap<Cliente, ClienteViewModel>();

            CreateMap<Fornecedor, FornecedorViewModel>();

            CreateMap<Produto, ProdutoViewModel>();

            CreateMap<UnidadeMedida, UnidadeMedidaViewModel>();

            CreateMap<Pedido, PedidoViewModel>();

            CreateMap<ItemPedido, ItemPedidoViewModel>();

            CreateMap<Role, RoleViewModel>();

            CreateMap<User, UserViewModel>();

            CreateMap<AccessItem, AccessItemViewModel>();

            CreateMap<AccessMenu, AccessMenuViewModel>();

            CreateMap<AccessClaim, AccessClaimViewModel>();

            CreateMap<ItemClaim, ItemClaimViewModel>();

            CreateMap<MenuClaim, MenuClaimViewModel>();

            CreateMap<RoleClaim, RoleClaimViewModel>();

            CreateMap<UserClaim, UserClaimViewModel>();

            CreateMap<UserRole, UserRoleViewModel>();

            CreateMap<Account, AccountViewModel>();

            CreateMap(typeof(TableResult<>), typeof(TableResult<>));

            CreateMap<ClienteTable, ClienteTableViewModel>();

            CreateMap<FornecedorTable, FornecedorTableViewModel>();

            CreateMap<ProdutoTable, ProdutoTableViewModel>();

            CreateMap<UnidadeMedidaTable, UnidadeMedidaTableViewModel>();

            CreateMap<UserTable, UserTableViewModel>();

            CreateMap<RoleTable, RoleTableViewModel>();
        }
    }
}
