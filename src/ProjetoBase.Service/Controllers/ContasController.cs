using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoBase.Application.Interfaces;
using ProjetoBase.Application.ViewModels.Contas;
using ProjetoBase.Infrastructure.CrossCutting.Common.Login;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;

namespace ProjetoBase.Service.Controllers
{
    [Route("api/contas")]
    [ApiController]
    public class ContasController : ControllerBase
    {
        private readonly IContaAppService _contaAppService;

        public ContasController(IContaAppService contaAppService) => _contaAppService = contaAppService;

        [AllowAnonymous]
        [HttpPost("login")]
        public LoginResult Login([FromBody] SignInViewModel viewModel) => _contaAppService.Login(viewModel);

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("obter-conta-usuario-autenticado")]
        public AccountViewModel ObterContaUsuarioAutenticado() => _contaAppService.ObterContaUsuarioAutenticado();

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("atualizar-conta-usuario-autenticado")]
        public NotificationResult AtualizarContaUsuarioAutenticado([FromBody] AccountViewModel viewModel) =>
            _contaAppService.AtualizarContaUsuarioAutenticado(viewModel);

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("atualizar-senha-usuario-autenticado")]
        public NotificationResult AtualizarSenhaUsuarioAutenticado([FromBody] UpdatePasswordViewModel viewModel) =>
            _contaAppService.AtualizarSenhaUsuarioAutenticado(viewModel);
    }
}
