using ProjetoBase.Application.ViewModels.Menus;
using System;
using System.Collections.Generic;

namespace ProjetoBase.Application.Interfaces
{
    public interface IMenuAppService : IDisposable
    {
        IEnumerable<AccessMenuViewModel> ObterMenusUsuarioAutenticado();
    }
}
