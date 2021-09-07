using System.Collections.Generic;

namespace ProjetoBase.Application.ViewModels.Direitos
{
    public class ItemClaimViewModel : ViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public IEnumerable<AccessClaimViewModel> Accesses { get; set; }
    }
}
