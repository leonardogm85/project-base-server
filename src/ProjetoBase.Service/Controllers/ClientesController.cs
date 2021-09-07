using Microsoft.AspNetCore.Mvc;
using ProjetoBase.Application.Interfaces;
using ProjetoBase.Application.ViewModels.Clientes;
using ProjetoBase.Infrastructure.CrossCutting.Common.Enums;
using ProjetoBase.Infrastructure.CrossCutting.Common.Security;
using ProjetoBase.Infrastructure.CrossCutting.Common.Selects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Tables;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;

namespace ProjetoBase.Service.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteAppService _clienteAppService;

        public ClientesController(IClienteAppService clienteAppService) => _clienteAppService = clienteAppService;

        [ClaimRequirement(Access.Read, Item.Clientes)]
        [HttpGet("obter/{id:guid}")]
        public ClienteViewModel Obter([FromRoute] Guid id) => _clienteAppService.ObterPorId(id);

        [ClaimRequirement(Access.Create, Item.Clientes)]
        [HttpPost("adicionar")]
        public NotificationResult Adicionar([FromBody] ClienteViewModel viewModel) => _clienteAppService.Adicionar(viewModel);

        [ClaimRequirement(Access.Update, Item.Clientes)]
        [HttpPut("atualizar")]
        public NotificationResult Atualizar([FromBody] ClienteViewModel viewModel) => _clienteAppService.Atualizar(viewModel);

        [ClaimRequirement(Access.Delete, Item.Clientes)]
        [HttpDelete("remover/{id:guid}")]
        public NotificationResult Remover([FromRoute] Guid id) => _clienteAppService.Remover(id);

        [ClaimRequirement(Access.Update, Item.Clientes)]
        [HttpPatch("ativar/{id:guid}")]
        public NotificationResult Ativar([FromRoute] Guid id) => _clienteAppService.Ativar(id);

        [ClaimRequirement(Access.Update, Item.Clientes)]
        [HttpPatch("desativar/{id:guid}")]
        public NotificationResult Desativar([FromRoute] Guid id) => _clienteAppService.Desativar(id);

        [ClaimRequirement(Access.Read, Item.Clientes)]
        [HttpPost("obter-tabela")]
        public TableResult<ClienteTableViewModel> ObterTabela([FromBody] TableFilter filtro) => _clienteAppService.ObterTabela(filtro);

        [ClaimRequirement(Access.Read, Item.Clientes, Item.Pedidos)]
        [HttpPost("obter-selecao")]
        public SelectResult<Guid, string> ObterSelecao([FromBody] SelectFilter filtro) => _clienteAppService.ObterSelecao(filtro);

        [ClaimRequirement(Access.Read, Item.Clientes, Item.Pedidos)]
        [HttpGet("obter-selecao")]
        public SelectResult<Guid, string> ObterSelecao([FromQuery] Guid[] identidades) => _clienteAppService.ObterSelecao(identidades);
    }
}
