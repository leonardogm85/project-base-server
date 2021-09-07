using Microsoft.AspNetCore.Mvc;
using ProjetoBase.Application.Interfaces;
using ProjetoBase.Application.ViewModels.Produtos;
using ProjetoBase.Infrastructure.CrossCutting.Common.Enums;
using ProjetoBase.Infrastructure.CrossCutting.Common.Security;
using ProjetoBase.Infrastructure.CrossCutting.Common.Selects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Tables;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;

namespace ProjetoBase.Service.Controllers
{
    [Route("api/produtos")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoAppService _produtoAppService;

        public ProdutosController(IProdutoAppService produtoAppService) => _produtoAppService = produtoAppService;

        [ClaimRequirement(Access.Read, Item.Produtos)]
        [HttpGet("obter/{id:guid}")]
        public ProdutoViewModel Obter([FromRoute] Guid id) => _produtoAppService.ObterPorId(id);

        [ClaimRequirement(Access.Create, Item.Produtos)]
        [HttpPost("adicionar")]
        public NotificationResult Adicionar([FromBody] ProdutoViewModel viewModel) => _produtoAppService.Adicionar(viewModel);

        [ClaimRequirement(Access.Update, Item.Produtos)]
        [HttpPut("atualizar")]
        public NotificationResult Atualizar([FromBody] ProdutoViewModel viewModel) => _produtoAppService.Atualizar(viewModel);

        [ClaimRequirement(Access.Delete, Item.Produtos)]
        [HttpDelete("remover/{id:guid}")]
        public NotificationResult Remover([FromRoute] Guid id) => _produtoAppService.Remover(id);

        [ClaimRequirement(Access.Update, Item.Produtos)]
        [HttpPatch("ativar/{id:guid}")]
        public NotificationResult Ativar([FromRoute] Guid id) => _produtoAppService.Ativar(id);

        [ClaimRequirement(Access.Update, Item.Produtos)]
        [HttpPatch("desativar/{id:guid}")]
        public NotificationResult Desativar([FromRoute] Guid id) => _produtoAppService.Desativar(id);

        [ClaimRequirement(Access.Read, Item.Produtos)]
        [HttpPost("obter-tabela")]
        public TableResult<ProdutoTableViewModel> ObterTabela([FromBody] TableFilter filtro) => _produtoAppService.ObterTabela(filtro);

        [ClaimRequirement(Access.Read, Item.Produtos, Item.Pedidos)]
        [HttpPost("obter-selecao")]
        public SelectResult<Guid, string> ObterSelecao([FromBody] SelectFilter filtro) => _produtoAppService.ObterSelecao(filtro);

        [ClaimRequirement(Access.Read, Item.Produtos, Item.Pedidos)]
        [HttpGet("obter-selecao")]
        public SelectResult<Guid, string> ObterSelecao([FromQuery] Guid[] identidades) => _produtoAppService.ObterSelecao(identidades);
    }
}
