using System;

namespace ProjetoBase.Application.ViewModels.Usuarios
{
    public class UserTableViewModel : ViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Administrator { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool Active { get; set; }
    }
}
