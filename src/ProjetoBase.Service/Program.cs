using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ProjetoBase.Service.Extensions;
using System.Threading.Tasks;

namespace ProjetoBase.Service
{
    public class Program
    {
        public static async Task Main(string[] args) => await (await CreateHostBuilder(args).Build().DataInitializerAsync()).RunAsync();

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(c => c.UseStartup<Startup>());
    }
}
