using Microsoft.AspNetCore.Mvc;
using ProjetoBase.Application.Interfaces;
using ProjetoBase.Application.ViewModels.Fornecedores;
using ProjetoBase.Infrastructure.CrossCutting.Common.Enums;
using ProjetoBase.Infrastructure.CrossCutting.Common.Security;
using ProjetoBase.Infrastructure.CrossCutting.Common.Selects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Tables;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;

namespace ProjetoBase.Service.Controllers
{
    [Route("api/fornecedores")]
    [ApiController]
    public class FornecedoresController : ControllerBase
    {
        private readonly IFornecedorAppService _fornecedorAppService;

        public FornecedoresController(IFornecedorAppService fornecedorAppService) => _fornecedorAppService = fornecedorAppService;

        [ClaimRequirement(Access.Read, Item.Fornecedores)]
        [HttpGet("obter/{id:guid}")]
        public FornecedorViewModel Obter([FromRoute] Guid id) => _fornecedorAppService.ObterPorId(id);

        [ClaimRequirement(Access.Create, Item.Fornecedores)]
        [HttpPost("adicionar")]
        public NotificationResult Adicionar([FromBody] FornecedorViewModel viewModel) => _fornecedorAppService.Adicionar(viewModel);

        [ClaimRequirement(Access.Update, Item.Fornecedores)]
        [HttpPut("atualizar")]
        public NotificationResult Atualizar([FromBody] FornecedorViewModel viewModel) => _fornecedorAppService.Atualizar(viewModel);

        [ClaimRequirement(Access.Delete, Item.Fornecedores)]
        [HttpDelete("remover/{id:guid}")]
        public NotificationResult Remover([FromRoute] Guid id) => _fornecedorAppService.Remover(id);

        [ClaimRequirement(Access.Update, Item.Fornecedores)]
        [HttpPatch("ativar/{id:guid}")]
        public NotificationResult Ativar([FromRoute] Guid id) => _fornecedorAppService.Ativar(id);

        [ClaimRequirement(Access.Update, Item.Fornecedores)]
        [HttpPatch("desativar/{id:guid}")]
        public NotificationResult Desativar([FromRoute] Guid id) => _fornecedorAppService.Desativar(id);

        [ClaimRequirement(Access.Read, Item.Fornecedores)]
        [HttpPost("obter-tabela")]
        public TableResult<FornecedorTableViewModel> ObterTabela([FromBody] TableFilter filtro) => _fornecedorAppService.ObterTabela(filtro);

        [ClaimRequirement(Access.Read, Item.Fornecedores, Item.Produtos)]
        [HttpPost("obter-selecao")]
        public SelectResult<Guid, string> ObterSelecao([FromBody] SelectFilter filtro) => _fornecedorAppService.ObterSelecao(filtro);

        [ClaimRequirement(Access.Read, Item.Fornecedores, Item.Produtos)]
        [HttpGet("obter-selecao")]
        public SelectResult<Guid, string> ObterSelecao([FromQuery] Guid[] identidades) => _fornecedorAppService.ObterSelecao(identidades);
    }
}
