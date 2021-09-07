using ProjetoBase.Domain.DataTransferObjects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Tables;

namespace ProjetoBase.Domain.Interfaces.Repositories
{
    public interface ITabelaRepository<TDataTransferObject>
        where TDataTransferObject : DataTransferObject
    {
        TableResult<TDataTransferObject> ObterTabela(TableFilter filtro);
    }
}
