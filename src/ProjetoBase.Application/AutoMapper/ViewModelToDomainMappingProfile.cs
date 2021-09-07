using AutoMapper;
using ProjetoBase.Application.ViewModels.Contas;
using ProjetoBase.Application.ViewModels.Direitos;
using ProjetoBase.Application.ViewModels.Papeis;
using ProjetoBase.Application.ViewModels.Usuarios;
using ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects;

namespace ProjetoBase.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ResetPasswordViewModel, ResetPassword>();

            CreateMap<ForgotPasswordViewModel, ForgotPassword>();

            CreateMap<ConfirmEmailViewModel, ConfirmEmail>();

            CreateMap<SignInViewModel, SignIn>();

            CreateMap<AccessClaimViewModel, AccessClaim>();

            CreateMap<ItemClaimViewModel, ItemClaim>();

            CreateMap<MenuClaimViewModel, MenuClaim>();

            CreateMap<RoleClaimViewModel, RoleClaim>();

            CreateMap<UserClaimViewModel, UserClaim>();

            CreateMap<UserRoleViewModel, UserRole>();

            CreateMap<AccountViewModel, Account>();

            CreateMap<UpdatePasswordViewModel, UpdatePassword>();
        }
    }
}
