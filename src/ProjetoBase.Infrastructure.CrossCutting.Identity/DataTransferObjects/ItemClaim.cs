using System.Collections.Generic;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects
{
    public class ItemClaim : DataTransferObject
    {
        public ItemClaim(int id, string description, IEnumerable<AccessClaim> accesses)
        {
            Id = id;
            Description = description;
            Accesses = accesses;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public IEnumerable<AccessClaim> Accesses { get; private set; }

        public void ChangeAccesses(IEnumerable<AccessClaim> accesses) => Accesses = accesses;
    }
}
