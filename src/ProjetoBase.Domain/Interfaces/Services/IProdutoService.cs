using ProjetoBase.Domain.DataTransferObjects;
using ProjetoBase.Domain.Entities;
using System;

namespace ProjetoBase.Domain.Interfaces.Services
{
    public interface IProdutoService : IService<Produto>, ITabelaService<ProdutoTable>, ISelecaoService<Guid, string>
    {
    }
}
