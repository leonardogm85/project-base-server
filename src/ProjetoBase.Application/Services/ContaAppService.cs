using AutoMapper;
using ProjetoBase.Application.Interfaces;
using ProjetoBase.Application.ViewModels.Contas;
using ProjetoBase.Infrastructure.CrossCutting.Common.Login;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Interfaces;
using System;

namespace ProjetoBase.Application.Services
{
    public class ContaAppService : IContaAppService
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public ContaAppService(IMapper mapper, IAccountService accountService)
        {
            _mapper = mapper;
            _accountService = accountService;
        }

        public LoginResult Login(SignInViewModel viewModel) => _accountService.SignInAsync(_mapper.Map<SignIn>(viewModel)).Result;

        public AccountViewModel ObterContaUsuarioAutenticado() => _mapper.Map<AccountViewModel>(_accountService.GetAuthenticatedUserAccountAsync().Result);

        public NotificationResult AtualizarContaUsuarioAutenticado(AccountViewModel viewModel) =>
            _accountService.UpdateAuthenticatedUserAccountAsync(_mapper.Map<Account>(viewModel)).Result;

        public NotificationResult AtualizarSenhaUsuarioAutenticado(UpdatePasswordViewModel viewModel) =>
            _accountService.UpdateAuthenticatedUserPasswordAsync(_mapper.Map<UpdatePassword>(viewModel)).Result;

        public void Dispose()
        {
            _accountService.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
