using ProjetoBase.Domain.DataTransferObjects;
using ProjetoBase.Domain.Entities;
using ProjetoBase.Domain.Interfaces.Repositories;
using ProjetoBase.Domain.Interfaces.Services;
using ProjetoBase.Domain.Interfaces.UnitOfWork;
using ProjetoBase.Infrastructure.CrossCutting.Common.Selects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Tables;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Constants;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;

namespace ProjetoBase.Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClienteRepository _clienteRepository;
        private readonly IPedidoRepository _pedidoRepository;

        public ClienteService(IUnitOfWork unitOfWork, IClienteRepository clienteRepository, IPedidoRepository pedidoRepository)
        {
            _unitOfWork = unitOfWork;
            _clienteRepository = clienteRepository;
            _pedidoRepository = pedidoRepository;
        }

        public NotificationResult Adicionar(Cliente cliente)
        {
            var result = cliente.Validar();

            if (result.Invalid)
            {
                return result;
            }

            var existeDocumento = _clienteRepository.ExisteDocumento(cliente.Documento);

            result.AddNotifications(new NotificationContract().IsFalse(existeDocumento, ValidationMessages.PessoaDocumentoDeveSerUnico));

            if (result.Valid)
            {
                _clienteRepository.Adicionar(cliente);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public NotificationResult Atualizar(Cliente cliente)
        {
            var result = cliente.Validar();

            if (result.Invalid)
            {
                return result;
            }

            var existeEntidade = _clienteRepository.Existe(cliente.Id);

            result.AddNotifications(new NotificationContract().IsTrue(existeEntidade, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            var existeDocumento = _clienteRepository.ExisteDocumentoEmOutroCliente(cliente.Id, cliente.Documento);
            var entidadeAtivo = _clienteRepository.Ativo(cliente.Id);

            result.AddNotifications(new NotificationContract()
                .IsFalse(existeDocumento, ValidationMessages.PessoaDocumentoDeveSerUnico)
                .IsTrue(entidadeAtivo, ValidationMessages.RegistroInativoNaoPodeSerAtualizado));

            if (result.Valid)
            {
                _clienteRepository.Atualizar(cliente);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public NotificationResult Remover(Guid id)
        {
            var result = new NotificationResult();

            var cliente = _clienteRepository.ObterPorId(id);

            result.AddNotifications(new NotificationContract().IsntNull(cliente, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            var existemPedidosAssociado = _pedidoRepository.ExistemPedidosAssociadoCliente(cliente.Id);

            result.AddNotifications(new NotificationContract().IsFalse(existemPedidosAssociado, ValidationMessages.ClienteNaoDevemExistirPedidosAssociado));

            if (result.Valid)
            {
                _clienteRepository.Remover(cliente);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public NotificationResult Ativar(Guid id)
        {
            var result = new NotificationResult();

            var cliente = _clienteRepository.ObterPorId(id);

            result.AddNotifications(new NotificationContract().IsntNull(cliente, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract().IsFalse(cliente.Ativo, ValidationMessages.RegistroAtivoNaoPodeSerAtivado));

            if (result.Valid)
            {
                cliente.Ativar();

                _clienteRepository.Atualizar(cliente);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public NotificationResult Desativar(Guid id)
        {
            var result = new NotificationResult();

            var cliente = _clienteRepository.ObterPorId(id);

            result.AddNotifications(new NotificationContract().IsntNull(cliente, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract().IsTrue(cliente.Ativo, ValidationMessages.RegistroInativoNaoPodeSerDesativado));

            if (result.Valid)
            {
                cliente.Desativar();

                _clienteRepository.Atualizar(cliente);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public Cliente ObterPorId(Guid id) => _clienteRepository.ObterPorId(id);

        public TableResult<ClienteTable> ObterTabela(TableFilter filtro) => _clienteRepository.ObterTabela(filtro);

        public SelectResult<Guid, string> ObterSelecao(params Guid[] identidades) => _clienteRepository.ObterSelecao(identidades);

        public SelectResult<Guid, string> ObterSelecao(SelectFilter filtro) => _clienteRepository.ObterSelecao(filtro);

        public void Dispose()
        {
            _unitOfWork.Dispose();
            _clienteRepository.Dispose();
            _pedidoRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
