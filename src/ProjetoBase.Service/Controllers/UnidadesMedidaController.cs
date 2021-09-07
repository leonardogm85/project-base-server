using Microsoft.AspNetCore.Mvc;
using ProjetoBase.Application.Interfaces;
using ProjetoBase.Application.ViewModels.UnidadesMedida;
using ProjetoBase.Infrastructure.CrossCutting.Common.Enums;
using ProjetoBase.Infrastructure.CrossCutting.Common.Security;
using ProjetoBase.Infrastructure.CrossCutting.Common.Selects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Tables;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;

namespace ProjetoBase.Service.Controllers
{
    [Route("api/unidades-medida")]
    [ApiController]
    public class UnidadesMedidaController : ControllerBase
    {
        private readonly IUnidadeMedidaAppService _unidadeMedidaAppService;

        public UnidadesMedidaController(IUnidadeMedidaAppService unidadeMedidaAppService) => _unidadeMedidaAppService = unidadeMedidaAppService;

        [ClaimRequirement(Access.Read, Item.UnidadesMedida)]
        [HttpGet("obter/{id:guid}")]
        public UnidadeMedidaViewModel Obter([FromRoute] Guid id) => _unidadeMedidaAppService.ObterPorId(id);

        [ClaimRequirement(Access.Create, Item.UnidadesMedida)]
        [HttpPost("adicionar")]
        public NotificationResult Adicionar([FromBody] UnidadeMedidaViewModel viewModel) => _unidadeMedidaAppService.Adicionar(viewModel);

        [ClaimRequirement(Access.Update, Item.UnidadesMedida)]
        [HttpPut("atualizar")]
        public NotificationResult Atualizar([FromBody] UnidadeMedidaViewModel viewModel) => _unidadeMedidaAppService.Atualizar(viewModel);

        [ClaimRequirement(Access.Delete, Item.UnidadesMedida)]
        [HttpDelete("remover/{id:guid}")]
        public NotificationResult Remover([FromRoute] Guid id) => _unidadeMedidaAppService.Remover(id);

        [ClaimRequirement(Access.Update, Item.UnidadesMedida)]
        [HttpPatch("ativar/{id:guid}")]
        public NotificationResult Ativar([FromRoute] Guid id) => _unidadeMedidaAppService.Ativar(id);

        [ClaimRequirement(Access.Update, Item.UnidadesMedida)]
        [HttpPatch("desativar/{id:guid}")]
        public NotificationResult Desativar([FromRoute] Guid id) => _unidadeMedidaAppService.Desativar(id);

        [ClaimRequirement(Access.Read, Item.UnidadesMedida)]
        [HttpPost("obter-tabela")]
        public TableResult<UnidadeMedidaTableViewModel> ObterTabela([FromBody] TableFilter filtro) => _unidadeMedidaAppService.ObterTabela(filtro);

        [ClaimRequirement(Access.Read, Item.UnidadesMedida, Item.Produtos)]
        [HttpPost("obter-selecao")]
        public SelectResult<Guid, string> ObterSelecao([FromBody] SelectFilter filtro) => _unidadeMedidaAppService.ObterSelecao(filtro);

        [ClaimRequirement(Access.Read, Item.UnidadesMedida, Item.Produtos)]
        [HttpGet("obter-selecao")]
        public SelectResult<Guid, string> ObterSelecao([FromQuery] Guid[] identidades) => _unidadeMedidaAppService.ObterSelecao(identidades);
    }
}
