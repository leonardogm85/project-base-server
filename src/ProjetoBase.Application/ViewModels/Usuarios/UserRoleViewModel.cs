using System;
using System.Collections.Generic;

namespace ProjetoBase.Application.ViewModels.Usuarios
{
    public class UserRoleViewModel : ViewModel
    {
        public Guid Id { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Name { get; set; }
        public IEnumerable<Guid> Roles { get; set; }
    }
}
