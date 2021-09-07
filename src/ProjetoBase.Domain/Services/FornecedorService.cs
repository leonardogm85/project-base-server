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
    public class FornecedorService : IFornecedorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IProdutoRepository _produtoRepository;

        public FornecedorService(IUnitOfWork unitOfWork, IFornecedorRepository fornecedorRepository, IProdutoRepository produtoRepository)
        {
            _unitOfWork = unitOfWork;
            _fornecedorRepository = fornecedorRepository;
            _produtoRepository = produtoRepository;
        }

        public NotificationResult Adicionar(Fornecedor fornecedor)
        {
            var result = fornecedor.Validar();

            if (result.Invalid)
            {
                return result;
            }

            var existeDocumento = _fornecedorRepository.ExisteDocumento(fornecedor.Documento);

            result.AddNotifications(new NotificationContract().IsFalse(existeDocumento, ValidationMessages.PessoaDocumentoDeveSerUnico));

            if (result.Valid)
            {
                _fornecedorRepository.Adicionar(fornecedor);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public NotificationResult Atualizar(Fornecedor fornecedor)
        {
            var result = fornecedor.Validar();

            if (result.Invalid)
            {
                return result;
            }

            var existeEntidade = _fornecedorRepository.Existe(fornecedor.Id);

            result.AddNotifications(new NotificationContract().IsTrue(existeEntidade, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            var existeDocumento = _fornecedorRepository.ExisteDocumentoEmOutroFornecedor(fornecedor.Id, fornecedor.Documento);
            var entidadeAtivo = _fornecedorRepository.Ativo(fornecedor.Id);

            result.AddNotifications(new NotificationContract()
                .IsFalse(existeDocumento, ValidationMessages.PessoaDocumentoDeveSerUnico)
                .IsTrue(entidadeAtivo, ValidationMessages.RegistroInativoNaoPodeSerAtualizado));

            if (result.Valid)
            {
                _fornecedorRepository.Atualizar(fornecedor);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public NotificationResult Remover(Guid id)
        {
            var result = new NotificationResult();

            var fornecedor = _fornecedorRepository.ObterPorId(id);

            result.AddNotifications(new NotificationContract().IsntNull(fornecedor, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            var existemProdutosAssociado = _produtoRepository.ExistemProdutosAssociadoFornecedor(fornecedor.Id);

            result.AddNotifications(new NotificationContract().IsFalse(existemProdutosAssociado, ValidationMessages.FornecedorNaoDevemExistirProdutosAssociado));

            if (result.Valid)
            {
                _fornecedorRepository.Remover(fornecedor);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public NotificationResult Ativar(Guid id)
        {
            var result = new NotificationResult();

            var fornecedor = _fornecedorRepository.ObterPorId(id);

            result.AddNotifications(new NotificationContract().IsntNull(fornecedor, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract().IsFalse(fornecedor.Ativo, ValidationMessages.RegistroAtivoNaoPodeSerAtivado));

            if (result.Valid)
            {
                fornecedor.Ativar();

                _fornecedorRepository.Atualizar(fornecedor);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public NotificationResult Desativar(Guid id)
        {
            var result = new NotificationResult();

            var fornecedor = _fornecedorRepository.ObterPorId(id);

            result.AddNotifications(new NotificationContract().IsntNull(fornecedor, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract().IsTrue(fornecedor.Ativo, ValidationMessages.RegistroInativoNaoPodeSerDesativado));

            if (result.Valid)
            {
                fornecedor.Desativar();

                _fornecedorRepository.Atualizar(fornecedor);

                return _unitOfWork.Commit();
            }

            return result;
        }

        public Fornecedor ObterPorId(Guid id) => _fornecedorRepository.ObterPorId(id);

        public TableResult<FornecedorTable> ObterTabela(TableFilter filtro) => _fornecedorRepository.ObterTabela(filtro);

        public SelectResult<Guid, string> ObterSelecao(params Guid[] identidades) => _fornecedorRepository.ObterSelecao(identidades);

        public SelectResult<Guid, string> ObterSelecao(SelectFilter filtro) => _fornecedorRepository.ObterSelecao(filtro);

        public void Dispose()
        {
            _unitOfWork.Dispose();
            _fornecedorRepository.Dispose();
            _produtoRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
