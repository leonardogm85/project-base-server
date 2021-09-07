using System;
using System.Collections.Generic;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Selects
{
    public class SelectResult<TKey, TValue>
        where TKey : IEquatable<TKey>
        where TValue : IEquatable<TValue>
    {
        public SelectResult(int recordsFiltered, IEnumerable<KeyValuePair<TKey, TValue>> data)
        {
            RecordsFiltered = recordsFiltered;
            Data = data;
        }

        public int RecordsFiltered { get; private set; }
        public IEnumerable<KeyValuePair<TKey, TValue>> Data { get; private set; }
    }
}
