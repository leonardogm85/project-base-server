namespace ProjetoBase.Application.ViewModels.Contas
{
    public class SignInViewModel : ViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsPersistent { get; set; }
    }
}
