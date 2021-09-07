using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjetoBase.Application.Data;
using System;
using System.Threading.Tasks;

namespace ProjetoBase.Service.Extensions
{
    public static class HostExtensions
    {
        public static async Task<IHost> DataInitializerAsync(this IHost host)
        {
            using var serviceScope = host.Services.CreateScope();
            var services = serviceScope.ServiceProvider;

            try
            {
                await services.GetRequiredService<DataInitializer>().InitializeDataAsync();
            }
            catch (Exception exception)
            {
                services.GetRequiredService<ILogger<Program>>().LogError(exception, "An error occurred.");
            }

            return host;
        }
    }
}
