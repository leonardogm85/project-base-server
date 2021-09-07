namespace ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects
{
    public class AccessItem : DataTransferObject
    {
        public AccessItem(int id, string description, int order, string address)
        {
            Id = id;
            Description = description;
            Order = order;
            Address = address;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public int Order { get; private set; }
        public string Address { get; private set; }
    }
}
