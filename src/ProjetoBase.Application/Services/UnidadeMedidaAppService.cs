using AutoMapper;
using ProjetoBase.Application.Interfaces;
using ProjetoBase.Application.ViewModels.UnidadesMedida;
using ProjetoBase.Domain.Entities;
using ProjetoBase.Domain.Interfaces.Services;
using ProjetoBase.Infrastructure.CrossCutting.Common.Selects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Tables;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;

namespace ProjetoBase.Application.Services
{
    public class UnidadeMedidaAppService : IUnidadeMedidaAppService
    {
        private readonly IMapper _mapper;
        private readonly IUnidadeMedidaService _unidadeMedidaService;

        public UnidadeMedidaAppService(IMapper mapper, IUnidadeMedidaService unidadeMedidaService)
        {
            _mapper = mapper;
            _unidadeMedidaService = unidadeMedidaService;
        }

        public NotificationResult Adicionar(UnidadeMedidaViewModel viewModel)
        {
            var unidadeMedida = new UnidadeMedida(
                Guid.NewGuid(),
                true,
                null,
                viewModel.Nome,
                viewModel.Sigla);

            return _unidadeMedidaService.Adicionar(unidadeMedida);
        }

        public NotificationResult Atualizar(UnidadeMedidaViewModel viewModel)
        {
            var unidadeMedida = new UnidadeMedida(
                viewModel.Id.GetValueOrDefault(),
                true,
                viewModel.Versao,
                viewModel.Nome,
                viewModel.Sigla);

            return _unidadeMedidaService.Atualizar(unidadeMedida);
        }

        public NotificationResult Remover(Guid id) => _unidadeMedidaService.Remover(id);

        public NotificationResult Ativar(Guid id) => _unidadeMedidaService.Ativar(id);

        public NotificationResult Desativar(Guid id) => _unidadeMedidaService.Desativar(id);

        public UnidadeMedidaViewModel ObterPorId(Guid id) => _mapper.Map<UnidadeMedidaViewModel>(_unidadeMedidaService.ObterPorId(id));

        public TableResult<UnidadeMedidaTableViewModel> ObterTabela(TableFilter filtro) =>
            _mapper.Map<TableResult<UnidadeMedidaTableViewModel>>(_unidadeMedidaService.ObterTabela(filtro));

        public SelectResult<Guid, string> ObterSelecao(params Guid[] identidades) => _unidadeMedidaService.ObterSelecao(identidades);

        public SelectResult<Guid, string> ObterSelecao(SelectFilter filtro) => _unidadeMedidaService.ObterSelecao(filtro);

        public void Dispose()
        {
            _unidadeMedidaService.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
