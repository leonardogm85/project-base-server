using AutoMapper;
using ProjetoBase.Application.Interfaces;
using ProjetoBase.Application.ViewModels.Direitos;
using ProjetoBase.Application.ViewModels.Usuarios;
using ProjetoBase.Infrastructure.CrossCutting.Common.Selects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Tables;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Interfaces;
using System;
using System.Collections.Generic;

namespace ProjetoBase.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsuarioAppService(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public NotificationResult Adicionar(UserViewModel viewModel)
        {
            var user = new User(
                Guid.Empty,
                true,
                null,
                viewModel.Name,
                viewModel.Email,
                viewModel.PhoneNumber,
                viewModel.Password);

            return _userService.AddAsync(user).Result;
        }

        public NotificationResult Atualizar(UserViewModel viewModel)
        {
            var user = new User(
                viewModel.Id.GetValueOrDefault(),
                true,
                viewModel.ConcurrencyStamp,
                viewModel.Name,
                viewModel.Email,
                viewModel.PhoneNumber);

            return _userService.UpdateAsync(user).Result;
        }

        public NotificationResult Remover(Guid id) => _userService.RemoveAsync(id).Result;

        public NotificationResult Ativar(Guid id) => _userService.ActivateAsync(id).Result;

        public NotificationResult Desativar(Guid id) => _userService.DeactivateAsync(id).Result;

        public UserViewModel ObterPorId(Guid id) => _mapper.Map<UserViewModel>(_userService.GetByIdAsync(id).Result);

        public NotificationResult RedefinirSenha(ResetPasswordViewModel viewModel) => _userService.ResetPasswordAsync(_mapper.Map<ResetPassword>(viewModel)).Result;

        public NotificationResult EsqueceuSenha(ForgotPasswordViewModel viewModel) => _userService.ForgotPasswordAsync(_mapper.Map<ForgotPassword>(viewModel)).Result;

        public NotificationResult ConfirmarEmail(ConfirmEmailViewModel viewModel) => _userService.ConfirmEmailAsync(_mapper.Map<ConfirmEmail>(viewModel)).Result;

        public NotificationResult EnviarTokenConfirmacaoPorEmail(Guid id) => _userService.SendEmailConfirmationTokenAsync(id).Result;

        public TableResult<UserTableViewModel> ObterTabela(TableFilter filtro) =>
            _mapper.Map<TableResult<UserTableViewModel>>(_userService.GetTableAsync(filtro).Result);

        public SelectResult<Guid, string> ObterSelecao(params Guid[] identidades) => _userService.GetSelectAsync(identidades).Result;

        public SelectResult<Guid, string> ObterSelecao(SelectFilter filtro) => _userService.GetSelectAsync(filtro).Result;

        public NotificationResult AdicionarAutorizacoes(UserClaimViewModel viewModel) =>
            _userService.AddUserClaimsAsync(_mapper.Map<UserClaim>(viewModel)).Result;

        public UserClaimViewModel ObterAutorizacoes(Guid id) => _mapper.Map<UserClaimViewModel>(_userService.GetUserClaimsAsync(id).Result);

        public IEnumerable<MenuClaimViewModel> ObterMenusAutorizados(Guid id) =>
            _mapper.Map<IEnumerable<MenuClaimViewModel>>(_userService.GetMenuClaimsAsync(id).Result);

        public NotificationResult AdicionarPapeis(UserRoleViewModel viewModel) => _userService.AddUserRolesAsync(_mapper.Map<UserRole>(viewModel)).Result;

        public UserRoleViewModel ObterPapeis(Guid id) => _mapper.Map<UserRoleViewModel>(_userService.GetUserRolesAsync(id).Result);

        public IEnumerable<string> ObterNomesPapeis(Guid id) => _userService.GetRoleNamesAsync(id).Result;

        public void Dispose()
        {
            _userService.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
