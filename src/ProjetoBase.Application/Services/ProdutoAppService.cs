using AutoMapper;
using ProjetoBase.Application.Interfaces;
using ProjetoBase.Application.ViewModels.Produtos;
using ProjetoBase.Domain.Entities;
using ProjetoBase.Domain.Interfaces.Services;
using ProjetoBase.Infrastructure.CrossCutting.Common.Selects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Tables;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;

namespace ProjetoBase.Application.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IMapper _mapper;
        private readonly IProdutoService _produtoService;

        public ProdutoAppService(IMapper mapper, IProdutoService produtoService)
        {
            _mapper = mapper;
            _produtoService = produtoService;
        }

        public NotificationResult Adicionar(ProdutoViewModel viewModel)
        {
            var produto = new Produto(
                Guid.NewGuid(),
                true,
                null,
                viewModel.Nome,
                viewModel.Descricao,
                viewModel.Valor,
                viewModel.UnidadeMedidaId,
                viewModel.FornecedorId);

            return _produtoService.Adicionar(produto);
        }

        public NotificationResult Atualizar(ProdutoViewModel viewModel)
        {
            var produto = new Produto(
                viewModel.Id.GetValueOrDefault(),
                true,
                viewModel.Versao,
                viewModel.Nome,
                viewModel.Descricao,
                viewModel.Valor,
                viewModel.UnidadeMedidaId,
                viewModel.FornecedorId);

            return _produtoService.Atualizar(produto);
        }

        public NotificationResult Remover(Guid id) => _produtoService.Remover(id);

        public NotificationResult Ativar(Guid id) => _produtoService.Ativar(id);

        public NotificationResult Desativar(Guid id) => _produtoService.Desativar(id);

        public ProdutoViewModel ObterPorId(Guid id) => _mapper.Map<ProdutoViewModel>(_produtoService.ObterPorId(id));

        public TableResult<ProdutoTableViewModel> ObterTabela(TableFilter filtro) =>
            _mapper.Map<TableResult<ProdutoTableViewModel>>(_produtoService.ObterTabela(filtro));

        public SelectResult<Guid, string> ObterSelecao(params Guid[] identidades) => _produtoService.ObterSelecao(identidades);

        public SelectResult<Guid, string> ObterSelecao(SelectFilter filtro) => _produtoService.ObterSelecao(filtro);

        public void Dispose()
        {
            _produtoService.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
