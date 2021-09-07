using AutoMapper;
using ProjetoBase.Application.Interfaces;
using ProjetoBase.Application.ViewModels.Fornecedores;
using ProjetoBase.Domain.Entities;
using ProjetoBase.Domain.Enums;
using ProjetoBase.Domain.Interfaces.Services;
using ProjetoBase.Domain.ValueObjects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Selects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Tables;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;

namespace ProjetoBase.Application.Services
{
    public class FornecedorAppService : IFornecedorAppService
    {
        private readonly IMapper _mapper;
        private readonly IFornecedorService _fornecedorService;

        public FornecedorAppService(IMapper mapper, IFornecedorService fornecedorService)
        {
            _mapper = mapper;
            _fornecedorService = fornecedorService;
        }

        public NotificationResult Adicionar(FornecedorViewModel viewModel)
        {
            var fornecedor = new Fornecedor(
                Guid.NewGuid(),
                true,
                null,
                (TipoPessoa)viewModel.TipoPessoa,
                viewModel.Apelido,
                viewModel.Nome,
                viewModel.Documento,
                viewModel.Email,
                viewModel.Celular,
                viewModel.Telefone,
                viewModel.Endereco == null ? default : new Endereco(
                    viewModel.Endereco.Cep,
                    viewModel.Endereco.Logradouro,
                    viewModel.Endereco.Numero,
                    viewModel.Endereco.Complemento,
                    viewModel.Endereco.Bairro,
                    viewModel.Endereco.Cidade,
                    viewModel.Endereco.Estado));

            return _fornecedorService.Adicionar(fornecedor);
        }

        public NotificationResult Atualizar(FornecedorViewModel viewModel)
        {
            var fornecedor = new Fornecedor(
                viewModel.Id.GetValueOrDefault(),
                true,
                viewModel.Versao,
                (TipoPessoa)viewModel.TipoPessoa,
                viewModel.Apelido,
                viewModel.Nome,
                viewModel.Documento,
                viewModel.Email,
                viewModel.Celular,
                viewModel.Telefone,
                viewModel.Endereco == null ? default : new Endereco(
                    viewModel.Endereco.Cep,
                    viewModel.Endereco.Logradouro,
                    viewModel.Endereco.Numero,
                    viewModel.Endereco.Complemento,
                    viewModel.Endereco.Bairro,
                    viewModel.Endereco.Cidade,
                    viewModel.Endereco.Estado));

            return _fornecedorService.Atualizar(fornecedor);
        }

        public NotificationResult Remover(Guid id) => _fornecedorService.Remover(id);

        public NotificationResult Ativar(Guid id) => _fornecedorService.Ativar(id);

        public NotificationResult Desativar(Guid id) => _fornecedorService.Desativar(id);

        public FornecedorViewModel ObterPorId(Guid id) => _mapper.Map<FornecedorViewModel>(_fornecedorService.ObterPorId(id));

        public TableResult<FornecedorTableViewModel> ObterTabela(TableFilter filtro) =>
            _mapper.Map<TableResult<FornecedorTableViewModel>>(_fornecedorService.ObterTabela(filtro));

        public SelectResult<Guid, string> ObterSelecao(params Guid[] identidades) => _fornecedorService.ObterSelecao(identidades);

        public SelectResult<Guid, string> ObterSelecao(SelectFilter filtro) => _fornecedorService.ObterSelecao(filtro);

        public void Dispose()
        {
            _fornecedorService.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
