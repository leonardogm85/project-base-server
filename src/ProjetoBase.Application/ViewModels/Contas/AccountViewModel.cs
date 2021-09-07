namespace ProjetoBase.Application.ViewModels.Contas
{
    public class AccountViewModel : ViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
