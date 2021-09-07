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
    public class UnidadeMedidaService : IUnidadeMedidaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnidadeMedidaRepository _unidadeMedidaRepository;
        private readonly IProdutoRepository _produtoRepository;

        public UnidadeMedidaService(IUnitOfWork unitOfWork, IUnidadeMedidaRepository unidadeMedidaRepository, IProdutoRepository produtoRepository)
        {
            _unitOfWork = unitOfWork;
            _unidadeMedidaRepository = unidadeMedidaRepository;
            _produtoRepository = produtoRepository;
        }

        public NotificationResult Adicionar(UnidadeMedida unidadeMedida)
        {
            var result = unidadeMedida.Validar();

            if (result.Invalid)
            {
                return result;
            }

            var existeSigla = _unidadeMedidaRepository.ExisteSigla(unidadeMedida.Sigla);

            result.AddNotifications(new NotificationContract().IsFalse(existeSigla, ValidationMessages.UnidadeMedidaSiglaDeveSerUnica));

            if (result.Valid)
            {
                _unidadeMedidaRepository.Adicionar(unidadeMedida);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public NotificationResult Atualizar(UnidadeMedida unidadeMedida)
        {
            var result = unidadeMedida.Validar();

            if (result.Invalid)
            {
                return result;
            }

            var existeEntidade = _unidadeMedidaRepository.Existe(unidadeMedida.Id);

            result.AddNotifications(new NotificationContract().IsTrue(existeEntidade, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            var existeSigla = _unidadeMedidaRepository.ExisteSiglaEmOutraUnidadeMedida(unidadeMedida.Id, unidadeMedida.Sigla);
            var entidadeAtivo = _unidadeMedidaRepository.Ativo(unidadeMedida.Id);

            result.AddNotifications(new NotificationContract()
                .IsFalse(existeSigla, ValidationMessages.UnidadeMedidaSiglaDeveSerUnica)
                .IsTrue(entidadeAtivo, ValidationMessages.RegistroInativoNaoPodeSerAtualizado));

            if (result.Valid)
            {
                _unidadeMedidaRepository.Atualizar(unidadeMedida);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public NotificationResult Remover(Guid id)
        {
            var result = new NotificationResult();

            var unidadeMedida = _unidadeMedidaRepository.ObterPorId(id);

            result.AddNotifications(new NotificationContract().IsntNull(unidadeMedida, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            var existemProdutosAssociado = _produtoRepository.ExistemProdutosAssociadoUnidadeMedida(unidadeMedida.Id);

            result.AddNotifications(new NotificationContract().IsFalse(existemProdutosAssociado, ValidationMessages.UnidadeMedidaNaoDevemExistirProdutosAssociado));

            if (result.Valid)
            {
                _unidadeMedidaRepository.Remover(unidadeMedida);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public NotificationResult Ativar(Guid id)
        {
            var result = new NotificationResult();

            var unidadeMedida = _unidadeMedidaRepository.ObterPorId(id);

            result.AddNotifications(new NotificationContract().IsntNull(unidadeMedida, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract().IsFalse(unidadeMedida.Ativo, ValidationMessages.RegistroAtivoNaoPodeSerAtivado));

            if (result.Valid)
            {
                unidadeMedida.Ativar();

                _unidadeMedidaRepository.Atualizar(unidadeMedida);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public NotificationResult Desativar(Guid id)
        {
            var result = new NotificationResult();

            var unidadeMedida = _unidadeMedidaRepository.ObterPorId(id);

            result.AddNotifications(new NotificationContract().IsntNull(unidadeMedida, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract().IsTrue(unidadeMedida.Ativo, ValidationMessages.RegistroInativoNaoPodeSerDesativado));

            if (result.Valid)
            {
                unidadeMedida.Desativar();

                _unidadeMedidaRepository.Atualizar(unidadeMedida);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public UnidadeMedida ObterPorId(Guid id) => _unidadeMedidaRepository.ObterPorId(id);

        public TableResult<UnidadeMedidaTable> ObterTabela(TableFilter filtro) => _unidadeMedidaRepository.ObterTabela(filtro);

        public SelectResult<Guid, string> ObterSelecao(params Guid[] identidades) => _unidadeMedidaRepository.ObterSelecao(identidades);

        public SelectResult<Guid, string> ObterSelecao(SelectFilter filtro) => _unidadeMedidaRepository.ObterSelecao(filtro);

        public void Dispose()
        {
            _unitOfWork.Dispose();
            _unidadeMedidaRepository.Dispose();
            _produtoRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
