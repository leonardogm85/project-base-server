using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjetoBase.Infrastructure.CrossCutting.InversionOfControl;

namespace ProjetoBase.Service
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers()
                .AddNewtonsoftJson();
            services.AddInversionOfControl(_configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(c => c
                .WithOrigins(_configuration["PresentationSettings:UrlBase"])
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseAuthorization();
            app.UseEndpoints(c => c.MapControllers());
        }
    }
}
