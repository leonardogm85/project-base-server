using ProjetoBase.Application.ViewModels.Clientes;
using System;

namespace ProjetoBase.Application.Interfaces
{
    public interface IClienteAppService : IAppService<ClienteViewModel>, ITabelaAppService<ClienteTableViewModel>, ISelecaoAppService<Guid, string>
    {
    }
}
