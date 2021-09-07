using System.Collections.Generic;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects
{
    public class AccessMenu : DataTransferObject
    {
        public AccessMenu(int id, string description, int order, IEnumerable<AccessItem> items)
        {
            Id = id;
            Description = description;
            Order = order;
            Items = items;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public int Order { get; private set; }
        public IEnumerable<AccessItem> Items { get; private set; }
    }
}
