using System.Collections.Generic;

namespace ProjetoBase.Application.ViewModels.Direitos
{
    public class MenuClaimViewModel : ViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public IEnumerable<ItemClaimViewModel> Items { get; set; }
    }
}
