using ProjetoBase.Domain.DataTransferObjects;
using ProjetoBase.Domain.Entities;
using System;

namespace ProjetoBase.Domain.Interfaces.Services
{
    public interface IClienteService : IService<Cliente>, ITabelaService<ClienteTable>, ISelecaoService<Guid, string>
    {
    }
}
