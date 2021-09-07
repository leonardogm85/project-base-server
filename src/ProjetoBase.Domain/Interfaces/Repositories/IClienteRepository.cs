using ProjetoBase.Domain.DataTransferObjects;
using ProjetoBase.Domain.Entities;
using System;

namespace ProjetoBase.Domain.Interfaces.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>, ITabelaRepository<ClienteTable>, ISelecaoRepository<Guid, string>
    {
        Cliente ObterPorDocumento(string documento);
        bool ExisteDocumento(string documento);
        bool ExisteDocumentoEmOutroCliente(Guid id, string documento);
    }
}
