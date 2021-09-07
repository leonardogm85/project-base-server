using ProjetoBase.Domain.DataTransferObjects;
using ProjetoBase.Domain.Entities;
using System;

namespace ProjetoBase.Domain.Interfaces.Repositories
{
    public interface IUnidadeMedidaRepository : IRepository<UnidadeMedida>, ITabelaRepository<UnidadeMedidaTable>, ISelecaoRepository<Guid, string>
    {
        UnidadeMedida ObterPorSigla(string sigla);
        bool ExisteSigla(string sigla);
        bool ExisteSiglaEmOutraUnidadeMedida(Guid id, string sigla);
    }
}
