using ProjetoBase.Domain.DataTransferObjects;
using ProjetoBase.Domain.Entities;
using System;

namespace ProjetoBase.Domain.Interfaces.Repositories
{
    public interface IFornecedorRepository : IRepository<Fornecedor>, ITabelaRepository<FornecedorTable>, ISelecaoRepository<Guid, string>
    {
        Fornecedor ObterPorDocumento(string documento);
        bool ExisteDocumento(string documento);
        bool ExisteDocumentoEmOutroFornecedor(Guid id, string documento);
    }
}
