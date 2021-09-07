using System.Collections.Generic;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects
{
    public class MenuClaim : DataTransferObject
    {
        public MenuClaim(int id, string description, IEnumerable<ItemClaim> items)
        {
            Id = id;
            Description = description;
            Items = items;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public IEnumerable<ItemClaim> Items { get; private set; }

        public void ChangeItems(IEnumerable<ItemClaim> items) => Items = items;
    }
}
