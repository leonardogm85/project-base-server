using Microsoft.AspNetCore.Identity;
using ProjetoBase.Infrastructure.CrossCutting.Common.Attributes;
using ProjetoBase.Infrastructure.CrossCutting.Common.Enums;
using ProjetoBase.Infrastructure.CrossCutting.Common.Extensions;
using ProjetoBase.Infrastructure.CrossCutting.Common.Interfaces;
using ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Entities;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Services
{
    public class MenuService : IMenuService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _authService;

        public MenuService(UserManager<ApplicationUser> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<IEnumerable<AccessMenu>> GetMenusByUserIdAsync(Guid userId)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId.ToString());

            var items = typeof(Item).GetAll<Item>();

            if (!applicationUser.Administrator)
            {
                var types = (await _userManager.GetClaimsAsync(applicationUser))
                    .Select(c => c.Type)
                    .Distinct();
                items = items.Where(i => types.Any(t => i.GetValue<int>().ToString() == t));
            }

            return items
                .GroupBy(g => (Menu)g.GetAttribute<AttachmentAttribute>().Attachment)
                .OrderBy(g => g.Key.GetAttribute<OrderAttribute>().Order)
                .Select(g =>
                    new AccessMenu(
                        g.Key.GetValue<int>(),
                        g.Key.GetAttribute<DescriptionAttribute>().Description,
                        g.Key.GetAttribute<OrderAttribute>().Order,
                        g
                            .OrderBy(i => i.GetAttribute<OrderAttribute>().Order)
                            .Select(i =>
                                new AccessItem(
                                    i.GetValue<int>(),
                                    i.GetAttribute<DescriptionAttribute>().Description,
                                    i.GetAttribute<OrderAttribute>().Order,
                                    i.GetAttribute<AddressAttribute>().Address
                                    )
                                )
                        )
                    );
        }

        public async Task<IEnumerable<AccessMenu>> GetAuthenticatedUserMenusAsync()
        {
            if (_authService.IsAuthenticated)
            {
                return await GetMenusByUserIdAsync(_authService.Id);
            }

            return await Task.FromResult(Enumerable.Empty<AccessMenu>());
        }

        public void Dispose()
        {
            _userManager.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
