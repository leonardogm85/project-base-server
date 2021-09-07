namespace ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects
{
    public class AccessClaim : DataTransferObject
    {
        public AccessClaim(int id, string description, bool enabled)
        {
            Id = id;
            Description = description;
            Enabled = enabled;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public bool Enabled { get; private set; }
    }
}
