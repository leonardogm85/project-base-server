using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoBase.Application.Interfaces;
using ProjetoBase.Application.ViewModels.Menus;
using System.Collections.Generic;

namespace ProjetoBase.Service.Controllers
{
    [Route("api/menus")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenuAppService _menuAppService;

        public MenusController(IMenuAppService menuAppService) => _menuAppService = menuAppService;

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("obter-menus-usuario-autenticado")]
        public IEnumerable<AccessMenuViewModel> ObterMenusUsuarioAutenticado() => _menuAppService.ObterMenusUsuarioAutenticado();
    }
}
