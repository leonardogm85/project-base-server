using Microsoft.Extensions.Options;
using ProjetoBase.Infrastructure.CrossCutting.Common.Settings;
using ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProjetoBase.Application.Data
{
    public class DataInitializer : IDisposable
    {
        private readonly IUserService _userService;
        private readonly AdministratorUserSettings _administratorUserSettings;

        public DataInitializer(IUserService userService, IOptions<AdministratorUserSettings> administratorUserSettings)
        {
            _userService = userService;
            _administratorUserSettings = administratorUserSettings.Value;
        }

        public async Task InitializeDataAsync()
        {
            if (!await _userService.ExistsAdministratorAsync())
            {
                var user = new User(
                    Guid.Empty,
                    true,
                    null,
                    _administratorUserSettings.Name,
                    _administratorUserSettings.Email,
                    _administratorUserSettings.PhoneNumber,
                    _administratorUserSettings.Password,
                    true);

                await _userService.AddAsync(user);
            }
        }

        public void Dispose()
        {
            _userService.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
