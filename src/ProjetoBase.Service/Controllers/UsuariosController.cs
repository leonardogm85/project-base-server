using Microsoft.AspNetCore.Mvc;
using ProjetoBase.Application.Interfaces;
using ProjetoBase.Infrastructure.CrossCutting.Common.Security;
using ProjetoBase.Application.ViewModels.Direitos;
using ProjetoBase.Application.ViewModels.Usuarios;
using ProjetoBase.Infrastructure.CrossCutting.Common.Enums;
using ProjetoBase.Infrastructure.CrossCutting.Common.Selects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Tables;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;
using System.Collections.Generic;

namespace ProjetoBase.Service.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public UsuariosController(IUsuarioAppService usuarioAppService) => _usuarioAppService = usuarioAppService;

        [ClaimRequirement(Access.Read, Item.Usuarios)]
        [HttpGet("obter/{id:guid}")]
        public UserViewModel Obter([FromRoute] Guid id) => _usuarioAppService.ObterPorId(id);

        [ClaimRequirement(Access.Create, Item.Usuarios)]
        [HttpPost("adicionar")]
        public NotificationResult Adicionar([FromBody] UserViewModel viewModel) => _usuarioAppService.Adicionar(viewModel);

        [ClaimRequirement(Access.Update, Item.Usuarios)]
        [HttpPut("atualizar")]
        public NotificationResult Atualizar([FromBody] UserViewModel viewModel) => _usuarioAppService.Atualizar(viewModel);

        [ClaimRequirement(Access.Update, Item.Usuarios)]
        [HttpPatch("ativar/{id:guid}")]
        public NotificationResult Ativar([FromRoute] Guid id) => _usuarioAppService.Ativar(id);

        [ClaimRequirement(Access.Update, Item.Usuarios)]
        [HttpPatch("desativar/{id:guid}")]
        public NotificationResult Desativar([FromRoute] Guid id) => _usuarioAppService.Desativar(id);

        [ClaimRequirement(Access.Read, Item.Usuarios)]
        [HttpPost("obter-tabela")]
        public TableResult<UserTableViewModel> ObterTabela([FromBody] TableFilter filtro) => _usuarioAppService.ObterTabela(filtro);

        [ClaimRequirement(Access.Update, Item.Usuarios)]
        [HttpPost("adicionar-autorizacoes")]
        public NotificationResult AdicionarAutorizacoes([FromBody] UserClaimViewModel viewModel) => _usuarioAppService.AdicionarAutorizacoes(viewModel);

        [ClaimRequirement(Access.Read, Item.Usuarios)]
        [HttpGet("obter-autorizacoes/{id:guid}")]
        public UserClaimViewModel ObterAutorizacoes([FromRoute] Guid id) => _usuarioAppService.ObterAutorizacoes(id);

        [ClaimRequirement(Access.Read, Item.Usuarios)]
        [HttpGet("obter-menus-autorizados/{id:guid}")]
        public IEnumerable<MenuClaimViewModel> ObterMenusAutorizados([FromRoute] Guid id) => _usuarioAppService.ObterMenusAutorizados(id);

        [ClaimRequirement(Access.Update, Item.Usuarios)]
        [HttpPost("adicionar-papeis")]
        public NotificationResult AdicionarPapeis([FromBody] UserRoleViewModel viewModel) => _usuarioAppService.AdicionarPapeis(viewModel);

        [ClaimRequirement(Access.Read, Item.Usuarios)]
        [HttpGet("obter-papeis/{id:guid}")]
        public UserRoleViewModel ObterPapeis([FromRoute] Guid id) => _usuarioAppService.ObterPapeis(id);

        [ClaimRequirement(Access.Read, Item.Usuarios)]
        [HttpGet("obter-nomes-papeis/{id:guid}")]
        public IEnumerable<string> ObterNomesPapeis([FromRoute] Guid id) => _usuarioAppService.ObterNomesPapeis(id);

        [ClaimRequirement(Access.Read, Item.Usuarios)]
        [HttpPost("obter-selecao")]
        public SelectResult<Guid, string> ObterSelecao([FromBody] SelectFilter filtro) => _usuarioAppService.ObterSelecao(filtro);

        [ClaimRequirement(Access.Read, Item.Usuarios)]
        [HttpGet("obter-selecao")]
        public SelectResult<Guid, string> ObterSelecao([FromQuery] Guid[] identidades) => _usuarioAppService.ObterSelecao(identidades);

        [ClaimRequirement(Access.Read, Item.Usuarios)]
        [HttpPost("enviar-token-confirmacao-por-email/{id:guid}")]
        public NotificationResult EnviarTokenConfirmacaoPorEmail([FromRoute] Guid id) => _usuarioAppService.EnviarTokenConfirmacaoPorEmail(id);
    }
}
