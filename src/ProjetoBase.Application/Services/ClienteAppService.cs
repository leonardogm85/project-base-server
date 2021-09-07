using AutoMapper;
using ProjetoBase.Application.Interfaces;
using ProjetoBase.Application.ViewModels.Clientes;
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
    public class ClienteAppService : IClienteAppService
    {
        private readonly IMapper _mapper;
        private readonly IClienteService _clienteService;

        public ClienteAppService(IMapper mapper, IClienteService clienteService)
        {
            _mapper = mapper;
            _clienteService = clienteService;
        }

        public NotificationResult Adicionar(ClienteViewModel viewModel)
        {
            var cliente = new Cliente(
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

            return _clienteService.Adicionar(cliente);
        }

        public NotificationResult Atualizar(ClienteViewModel viewModel)
        {
            var cliente = new Cliente(
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

            return _clienteService.Atualizar(cliente);
        }

        public NotificationResult Remover(Guid id) => _clienteService.Remover(id);

        public NotificationResult Ativar(Guid id) => _clienteService.Ativar(id);

        public NotificationResult Desativar(Guid id) => _clienteService.Desativar(id);

        public ClienteViewModel ObterPorId(Guid id) => _mapper.Map<ClienteViewModel>(_clienteService.ObterPorId(id));

        public TableResult<ClienteTableViewModel> ObterTabela(TableFilter filtro) =>
            _mapper.Map<TableResult<ClienteTableViewModel>>(_clienteService.ObterTabela(filtro));

        public SelectResult<Guid, string> ObterSelecao(params Guid[] identidades) => _clienteService.ObterSelecao(identidades);

        public SelectResult<Guid, string> ObterSelecao(SelectFilter filtro) => _clienteService.ObterSelecao(filtro);

        public void Dispose()
        {
            _clienteService.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
