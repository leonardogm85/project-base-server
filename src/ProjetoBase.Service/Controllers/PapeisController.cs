using Microsoft.AspNetCore.Mvc;
using ProjetoBase.Application.Interfaces;
using ProjetoBase.Application.ViewModels.Direitos;
using ProjetoBase.Application.ViewModels.Papeis;
using ProjetoBase.Infrastructure.CrossCutting.Common.Enums;
using ProjetoBase.Infrastructure.CrossCutting.Common.Security;
using ProjetoBase.Infrastructure.CrossCutting.Common.Selects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Tables;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;
using System.Collections.Generic;

namespace ProjetoBase.Service.Controllers
{
    [Route("api/papeis")]
    [ApiController]
    public class PapeisController : ControllerBase
    {
        private readonly IPapelAppService _papelAppService;

        public PapeisController(IPapelAppService papelAppService) => _papelAppService = papelAppService;

        [ClaimRequirement(Access.Read, Item.Papeis)]
        [HttpGet("obter/{id:guid}")]
        public RoleViewModel Obter([FromRoute] Guid id) => _papelAppService.ObterPorId(id);

        [ClaimRequirement(Access.Create, Item.Papeis)]
        [HttpPost("adicionar")]
        public NotificationResult Adicionar([FromBody] RoleViewModel viewModel) => _papelAppService.Adicionar(viewModel);

        [ClaimRequirement(Access.Update, Item.Papeis)]
        [HttpPut("atualizar")]
        public NotificationResult Atualizar([FromBody] RoleViewModel viewModel) => _papelAppService.Atualizar(viewModel);

        [ClaimRequirement(Access.Delete, Item.Papeis)]
        [HttpDelete("remover/{id:guid}")]
        public NotificationResult Remover([FromRoute] Guid id) => _papelAppService.Remover(id);

        [ClaimRequirement(Access.Update, Item.Papeis)]
        [HttpPatch("ativar/{id:guid}")]
        public NotificationResult Ativar([FromRoute] Guid id) => _papelAppService.Ativar(id);

        [ClaimRequirement(Access.Update, Item.Papeis)]
        [HttpPatch("desativar/{id:guid}")]
        public NotificationResult Desativar([FromRoute] Guid id) => _papelAppService.Desativar(id);

        [ClaimRequirement(Access.Read, Item.Papeis)]
        [HttpPost("obter-tabela")]
        public TableResult<RoleTableViewModel> ObterTabela([FromBody] TableFilter filtro) => _papelAppService.ObterTabela(filtro);

        [ClaimRequirement(Access.Update, Item.Papeis)]
        [HttpPost("adicionar-autorizacoes")]
        public NotificationResult AdicionarAutorizacoes([FromBody] RoleClaimViewModel viewModel) => _papelAppService.AdicionarAutorizacoes(viewModel);

        [ClaimRequirement(Access.Read, Item.Papeis)]
        [HttpGet("obter-autorizacoes/{id:guid}")]
        public RoleClaimViewModel ObterAutorizacoes([FromRoute] Guid id) => _papelAppService.ObterAutorizacoes(id);

        [ClaimRequirement(Access.Read, Item.Papeis)]
        [HttpGet("obter-menus-autorizados/{id:guid}")]
        public IEnumerable<MenuClaimViewModel> ObterMenusAutorizados([FromRoute] Guid id) => _papelAppService.ObterMenusAutorizados(id);

        [ClaimRequirement(Access.Read, Item.Papeis, Item.Usuarios)]
        [HttpPost("obter-selecao")]
        public SelectResult<Guid, string> ObterSelecao([FromBody] SelectFilter filtro) => _papelAppService.ObterSelecao(filtro);

        [ClaimRequirement(Access.Read, Item.Papeis, Item.Usuarios)]
        [HttpGet("obter-selecao")]
        public SelectResult<Guid, string> ObterSelecao([FromQuery] Guid[] identidades) => _papelAppService.ObterSelecao(identidades);
    }
}
