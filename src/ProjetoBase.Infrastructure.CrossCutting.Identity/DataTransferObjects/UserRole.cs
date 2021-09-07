using System;
using System.Collections.Generic;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects
{
    public class UserRole : DataTransferObject
    {
        public UserRole(Guid id, string concurrencyStamp, string name, IEnumerable<Guid> roles)
        {
            Id = id;
            ConcurrencyStamp = concurrencyStamp;
            Name = name;
            Roles = roles;
        }

        public Guid Id { get; private set; }
        public string ConcurrencyStamp { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<Guid> Roles { get; private set; }
    }
}
