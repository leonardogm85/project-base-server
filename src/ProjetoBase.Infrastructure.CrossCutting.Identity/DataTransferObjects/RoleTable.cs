using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects
{
    public class RoleTable : DataTransferObject
    {
        public RoleTable(Guid id, string name, string description, bool active)
        {
            Id = id;
            Name = name;
            Description = description;
            Active = active;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
    }
}
