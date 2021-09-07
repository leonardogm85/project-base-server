using System;
using System.Collections.Generic;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects
{
    public class RoleClaim : DataTransferObject
    {
        public RoleClaim(Guid id, string concurrencyStamp, string name, IEnumerable<MenuClaim> menus)
        {
            Id = id;
            ConcurrencyStamp = concurrencyStamp;
            Name = name;
            Menus = menus;
        }

        public Guid Id { get; private set; }
        public string ConcurrencyStamp { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<MenuClaim> Menus { get; private set; }

        public void ChangeMenus(IEnumerable<MenuClaim> menus) => Menus = menus;
    }
}
