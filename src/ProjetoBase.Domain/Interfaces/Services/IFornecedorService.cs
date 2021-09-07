using ProjetoBase.Domain.DataTransferObjects;
using ProjetoBase.Domain.Entities;
using System;

namespace ProjetoBase.Domain.Interfaces.Services
{
    public interface IFornecedorService : IService<Fornecedor>, ITabelaService<FornecedorTable>, ISelecaoService<Guid, string>
    {
    }
}
