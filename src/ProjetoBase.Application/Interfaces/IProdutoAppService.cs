using ProjetoBase.Application.ViewModels.Produtos;
using System;

namespace ProjetoBase.Application.Interfaces
{
    public interface IProdutoAppService : IAppService<ProdutoViewModel>, ITabelaAppService<ProdutoTableViewModel>, ISelecaoAppService<Guid, string>
    {
    }
}
