namespace ProjetoBase.Infrastructure.CrossCutting.Common.Selects
{
    public class SelectFilter
    {
        public SelectFilter(int start, int length, string search)
        {
            Start = start;
            Length = length;
            Search = search;
        }

        public int Start { get; private set; }
        public int Length { get; private set; }
        public string Search { get; private set; }
    }
}
