using System.Collections.Generic;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Tables
{
    public class TableResult<TDataTransferObject>
        where TDataTransferObject : class
    {
        public TableResult(int recordsTotal, int recordsFiltered, IEnumerable<TDataTransferObject> data)
        {
            RecordsTotal = recordsTotal;
            RecordsFiltered = recordsFiltered;
            Data = data;
        }

        public int RecordsTotal { get; private set; }
        public int RecordsFiltered { get; private set; }
        public IEnumerable<TDataTransferObject> Data { get; private set; }
    }
}
