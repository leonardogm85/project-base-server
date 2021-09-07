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
    public class ProdutoService : IProdutoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPedidoRepository _pedidoRepository;

        public ProdutoService(IUnitOfWork unitOfWork, IProdutoRepository produtoRepository, IPedidoRepository pedidoRepository)
        {
            _unitOfWork = unitOfWork;
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
        }

        public NotificationResult Adicionar(Produto produto)
        {
            var result = produto.Validar();

            if (result.Invalid)
            {
                return result;
            }

            var existeNome = _produtoRepository.ExisteNome(produto.Nome);

            result.AddNotifications(new NotificationContract().IsFalse(existeNome, ValidationMessages.ProdutoNomeDeveSerUnico));

            if (result.Valid)
            {
                _produtoRepository.Adicionar(produto);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public NotificationResult Atualizar(Produto produto)
        {
            var result = produto.Validar();

            if (result.Invalid)
            {
                return result;
            }

            var existeEntidade = _produtoRepository.Existe(produto.Id);

            result.AddNotifications(new NotificationContract().IsTrue(existeEntidade, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            var existeNome = _produtoRepository.ExisteNomeEmOutroProduto(produto.Id, produto.Nome);
            var entidadeAtivo = _produtoRepository.Ativo(produto.Id);

            result.AddNotifications(new NotificationContract()
                .IsFalse(existeNome, ValidationMessages.ProdutoNomeDeveSerUnico)
                .IsTrue(entidadeAtivo, ValidationMessages.RegistroInativoNaoPodeSerAtualizado));

            if (result.Valid)
            {
                _produtoRepository.Atualizar(produto);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public NotificationResult Remover(Guid id)
        {
            var result = new NotificationResult();

            var produto = _produtoRepository.ObterPorId(id);

            result.AddNotifications(new NotificationContract().IsntNull(produto, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            var existemItensPedidoAssociado = _pedidoRepository.ExistemItensPedidoAssociadoProduto(produto.Id);

            result.AddNotifications(new NotificationContract().IsFalse(existemItensPedidoAssociado, ValidationMessages.ProdutoNaoDevemExistirItensPedidoAssociado));

            if (result.Valid)
            {
                _produtoRepository.Remover(produto);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public NotificationResult Ativar(Guid id)
        {
            var result = new NotificationResult();

            var produto = _produtoRepository.ObterPorId(id);

            result.AddNotifications(new NotificationContract().IsntNull(produto, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract().IsFalse(produto.Ativo, ValidationMessages.RegistroAtivoNaoPodeSerAtivado));

            if (result.Valid)
            {
                produto.Ativar();

                _produtoRepository.Atualizar(produto);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public NotificationResult Desativar(Guid id)
        {
            var result = new NotificationResult();

            var produto = _produtoRepository.ObterPorId(id);

            result.AddNotifications(new NotificationContract().IsntNull(produto, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract().IsTrue(produto.Ativo, ValidationMessages.RegistroInativoNaoPodeSerDesativado));

            if (result.Valid)
            {
                produto.Desativar();

                _produtoRepository.Atualizar(produto);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public Produto ObterPorId(Guid id) => _produtoRepository.ObterPorId(id);

        public TableResult<ProdutoTable> ObterTabela(TableFilter filtro) => _produtoRepository.ObterTabela(filtro);

        public SelectResult<Guid, string> ObterSelecao(params Guid[] identidades) => _produtoRepository.ObterSelecao(identidades);

        public SelectResult<Guid, string> ObterSelecao(SelectFilter filtro) => _produtoRepository.ObterSelecao(filtro);

        public void Dispose()
        {
            _unitOfWork.Dispose();
            _produtoRepository.Dispose();
            _pedidoRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
