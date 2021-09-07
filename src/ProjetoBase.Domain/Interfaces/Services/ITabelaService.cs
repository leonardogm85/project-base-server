using ProjetoBase.Domain.DataTransferObjects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Tables;

namespace ProjetoBase.Domain.Interfaces.Services
{
    public interface ITabelaService<TDataTransferObject>
        where TDataTransferObject : DataTransferObject
    {
        TableResult<TDataTransferObject> ObterTabela(TableFilter filtro);
    }
}
