namespace ProjetoBase.Application.ViewModels.Usuarios
{
    public class ResetPasswordViewModel : ViewModel
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; }
    }
}
