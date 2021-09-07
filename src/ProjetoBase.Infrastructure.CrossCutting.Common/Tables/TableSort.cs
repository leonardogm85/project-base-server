using ProjetoBase.Infrastructure.CrossCutting.Common.Enums;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Tables
{
    public class TableSort
    {
        public TableSort(string column, SortDirection direction)
        {
            Column = column;
            Direction = direction;
        }

        public string Column { get; private set; }
        public SortDirection Direction { get; private set; }
    }
}
