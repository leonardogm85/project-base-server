using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProjetoBase.Application.AutoMapper;
using ProjetoBase.Application.Data;
using ProjetoBase.Application.Interfaces;
using ProjetoBase.Application.Services;
using ProjetoBase.Domain.Interfaces.Repositories;
using ProjetoBase.Domain.Interfaces.Services;
using ProjetoBase.Domain.Interfaces.UnitOfWork;
using ProjetoBase.Domain.Services;
using ProjetoBase.Infrastructure.CrossCutting.Common.Interfaces;
using ProjetoBase.Infrastructure.CrossCutting.Common.Security;
using ProjetoBase.Infrastructure.CrossCutting.Common.Services;
using ProjetoBase.Infrastructure.CrossCutting.Common.Settings;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Context;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Entities;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Interfaces;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Services;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Validations;
using ProjetoBase.Infrastructure.Data.Configuration;
using ProjetoBase.Infrastructure.Data.Context;
using ProjetoBase.Infrastructure.Data.Repositories;
using ProjetoBase.Infrastructure.Data.UnitOfWork;
using System;

namespace ProjetoBase.Infrastructure.CrossCutting.InversionOfControl
{
    public static class InversionOfControlServiceCollectionExtensions
    {
        public static void AddInversionOfControl(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AutoMapperConfiguration));

            services.AddScoped<IClienteAppService, ClienteAppService>();
            services.AddScoped<IFornecedorAppService, FornecedorAppService>();
            services.AddScoped<IPedidoAppService, PedidoAppService>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IUnidadeMedidaAppService, UnidadeMedidaAppService>();
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            services.AddScoped<IPapelAppService, PapelAppService>();
            services.AddScoped<IContaAppService, ContaAppService>();
            services.AddScoped<IMenuAppService, MenuAppService>();

            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IUnidadeMedidaService, UnidadeMedidaService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IMenuService, MenuService>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IUnidadeMedidaRepository, UnidadeMedidaRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAuthService, AuthService>();

            services.AddScoped<DataInitializer>();

            services.AddSingleton<IAuthorizationPolicyProvider, ClaimRequirementPolicy>();
            services.AddSingleton<IAuthorizationHandler, ClaimRequirementHandler>();

            services.AddHttpContextAccessor();

            services.AddLogging(c => c.AddConsole());

            var emailSenderSection = configuration.GetSection(nameof(EmailSenderSettings));
            var administratorUserSection = configuration.GetSection(nameof(AdministratorUserSettings));
            var authorizationTokenSection = configuration.GetSection(nameof(AuthorizationTokenSettings));

            services.Configure<EmailSenderSettings>(emailSenderSection);
            services.Configure<AdministratorUserSettings>(administratorUserSection);
            services.Configure<AuthorizationTokenSettings>(authorizationTokenSection);

            services.AddDatabase(configuration);

            services.AddIdentity();

            var signingConfiguration = new SigningConfiguration();

            services.AddSingleton(signingConfiguration);

            var authorizationTokenSettings = authorizationTokenSection.Get<AuthorizationTokenSettings>();

            services.AddSecurity(signingConfiguration, authorizationTokenSettings);
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ProjetoBaseContext>(o => ConfigureDatabase.SetConnectionString(o, connectionString));
            services.AddDbContext<ApplicationContext>(o => ConfigureDatabase.SetConnectionString(o, connectionString));
        }

        private static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(o => o.SignIn.RequireConfirmedEmail = true)
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddErrorDescriber<ApplicationErrorDescriber>()
                .AddDefaultTokenProviders();
        }

        private static void AddSecurity(this IServiceCollection services, SigningConfiguration signingConfiguration,
                                        AuthorizationTokenSettings authorizationTokenSettings)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var tokenValidationParameters = o.TokenValidationParameters;

                tokenValidationParameters.IssuerSigningKey = signingConfiguration.SecurityKey;

                tokenValidationParameters.ValidAudience = authorizationTokenSettings.Audience;
                tokenValidationParameters.ValidIssuer = authorizationTokenSettings.Issuer;

                tokenValidationParameters.ValidateIssuerSigningKey = true;
                tokenValidationParameters.ValidateLifetime = true;

                tokenValidationParameters.ClockSkew = TimeSpan.Zero;
            });
        }
    }
}
