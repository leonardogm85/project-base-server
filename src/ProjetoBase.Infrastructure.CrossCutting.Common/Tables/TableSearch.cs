namespace ProjetoBase.Infrastructure.CrossCutting.Common.Tables
{
    public class TableSearch
    {
        public TableSearch(string column, string value)
        {
            Column = column;
            Value = value;
        }

        public string Column { get; private set; }
        public string Value { get; private set; }
    }
}
