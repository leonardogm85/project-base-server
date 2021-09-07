using Microsoft.AspNetCore.Http;
using ProjetoBase.Infrastructure.CrossCutting.Common.Interfaces;
using ProjetoBase.Infrastructure.CrossCutting.Common.Security;
using System;
using System.Linq;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        public Guid Id => Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ApplicationClaimTypes.Id).Value);

        public string Name => _httpContextAccessor.HttpContext.User.FindFirst(ApplicationClaimTypes.Name).Value;

        public string Email => _httpContextAccessor.HttpContext.User.FindFirst(ApplicationClaimTypes.Email).Value;

        public bool IsAdministrator => HasClaim(ApplicationClaimTypes.Administrator, bool.TrueString);

        public bool IsAuthenticated => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public bool HasClaim(string type, string value) => _httpContextAccessor.HttpContext.User.HasClaim(type, value);

        public bool IsInRole(string role) => _httpContextAccessor.HttpContext.User.IsInRole(role);

        public bool IsAuthorized(string value, params string[] types) => IsAuthenticated && (IsAdministrator || types.Any(t => HasClaim(t, value)));
    }
}
