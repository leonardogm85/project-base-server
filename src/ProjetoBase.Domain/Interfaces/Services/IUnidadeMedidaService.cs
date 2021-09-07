using ProjetoBase.Domain.DataTransferObjects;
using ProjetoBase.Domain.Entities;
using System;

namespace ProjetoBase.Domain.Interfaces.Services
{
    public interface IUnidadeMedidaService : IService<UnidadeMedida>, ITabelaService<UnidadeMedidaTable>, ISelecaoService<Guid, string>
    {
    }
}
