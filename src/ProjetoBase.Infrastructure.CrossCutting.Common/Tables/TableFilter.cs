using System.Collections.Generic;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Tables
{
    public class TableFilter
    {
        public TableFilter(int start, int length, TableSort sort, IEnumerable<TableSearch> searches)
        {
            Start = start;
            Length = length;
            Sort = sort;
            Searches = searches;
        }

        public int Start { get; private set; }
        public int Length { get; private set; }
        public TableSort Sort { get; private set; }
        public IEnumerable<TableSearch> Searches { get; private set; }
    }
}
