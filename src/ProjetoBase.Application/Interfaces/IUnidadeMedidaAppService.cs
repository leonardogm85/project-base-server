using ProjetoBase.Application.ViewModels.UnidadesMedida;
using System;

namespace ProjetoBase.Application.Interfaces
{
    public interface IUnidadeMedidaAppService : IAppService<UnidadeMedidaViewModel>, ITabelaAppService<UnidadeMedidaTableViewModel>, ISelecaoAppService<Guid, string>
    {
    }
}
