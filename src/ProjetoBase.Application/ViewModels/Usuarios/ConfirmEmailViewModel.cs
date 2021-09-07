using System;

namespace ProjetoBase.Application.ViewModels.Usuarios
{
    public class ConfirmEmailViewModel : ViewModel
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
    }
}
