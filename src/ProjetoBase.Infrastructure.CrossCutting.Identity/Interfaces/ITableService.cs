using ProjetoBase.Infrastructure.CrossCutting.Common.Tables;
using ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects;
using System.Threading.Tasks;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Interfaces
{
    public interface ITableService<TDataTransferObject>
        where TDataTransferObject : DataTransferObject
    {
        Task<TableResult<TDataTransferObject>> GetTableAsync(TableFilter filter);
    }
}
