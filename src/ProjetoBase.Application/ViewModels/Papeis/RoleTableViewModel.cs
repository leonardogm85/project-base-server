using System;

namespace ProjetoBase.Application.ViewModels.Papeis
{
    public class RoleTableViewModel : ViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
