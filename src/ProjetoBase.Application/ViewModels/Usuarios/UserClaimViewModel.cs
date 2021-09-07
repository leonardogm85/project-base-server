﻿using ProjetoBase.Application.ViewModels.Direitos;
using System;
using System.Collections.Generic;

namespace ProjetoBase.Application.ViewModels.Usuarios
{
    public class UserClaimViewModel : ViewModel
    {
        public Guid Id { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Name { get; set; }
        public IEnumerable<MenuClaimViewModel> Menus { get; set; }
    }
}
