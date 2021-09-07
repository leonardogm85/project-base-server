using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Interfaces
{
    public interface IAuthService
    {
        Guid Id { get; }
        string Name { get; }
        string Email { get; }
        bool IsAdministrator { get; }
        bool IsAuthenticated { get; }
        bool HasClaim(string type, string value);
        bool IsInRole(string role);
        bool IsAuthorized(string value, params string[] types);
    }
}
