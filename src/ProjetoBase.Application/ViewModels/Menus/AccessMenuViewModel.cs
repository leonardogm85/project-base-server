using System.Collections.Generic;

namespace ProjetoBase.Application.ViewModels.Menus
{
    public class AccessMenuViewModel : ViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public IEnumerable<AccessItemViewModel> Items { get; set; }
    }
}
