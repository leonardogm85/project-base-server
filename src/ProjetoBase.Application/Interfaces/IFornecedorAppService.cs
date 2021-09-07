using ProjetoBase.Application.ViewModels.Fornecedores;
using System;

namespace ProjetoBase.Application.Interfaces
{
    public interface IFornecedorAppService : IAppService<FornecedorViewModel>, ITabelaAppService<FornecedorTableViewModel>, ISelecaoAppService<Guid, string>
    {
    }
}
