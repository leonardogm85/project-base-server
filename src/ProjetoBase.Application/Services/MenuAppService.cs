using AutoMapper;
using ProjetoBase.Application.Interfaces;
using ProjetoBase.Application.ViewModels.Menus;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Interfaces;
using System;
using System.Collections.Generic;

namespace ProjetoBase.Application.Services
{
    public class MenuAppService : IMenuAppService
    {
        private readonly IMapper _mapper;
        private readonly IMenuService _menuService;

        public MenuAppService(IMapper mapper, IMenuService menuService)
        {
            _mapper = mapper;
            _menuService = menuService;
        }

        public IEnumerable<AccessMenuViewModel> ObterMenusUsuarioAutenticado() =>
            _mapper.Map<IEnumerable<AccessMenuViewModel>>(_menuService.GetAuthenticatedUserMenusAsync().Result);

        public void Dispose()
        {
            _menuService.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
