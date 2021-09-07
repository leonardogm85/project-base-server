using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects
{
    public class Role : DataTransferObject
    {
        public Role(Guid id, bool active, string concurrencyStamp, string name, string description)
        {
            Id = id;
            Active = active;
            ConcurrencyStamp = concurrencyStamp;
            Name = name;
            Description = description;
        }

        public Guid Id { get; private set; }
        public bool Active { get; private set; }
        public string ConcurrencyStamp { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}
