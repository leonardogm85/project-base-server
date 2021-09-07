using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjetoBase.Infrastructure.CrossCutting.Common.Extensions;
using ProjetoBase.Infrastructure.CrossCutting.Common.Interfaces;
using ProjetoBase.Infrastructure.CrossCutting.Common.Login;
using ProjetoBase.Infrastructure.CrossCutting.Common.Security;
using ProjetoBase.Infrastructure.CrossCutting.Common.Settings;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Constants;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Entities;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Interfaces;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SigningConfiguration _signingConfiguration;
        private readonly AuthorizationTokenSettings _authorizationTokenSettings;
        private readonly IAuthService _authService;

        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
                              SigningConfiguration signingConfiguration,
                              IOptions<AuthorizationTokenSettings> optionsAuthorizationTokenSettings,
                              IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signingConfiguration = signingConfiguration;
            _authorizationTokenSettings = optionsAuthorizationTokenSettings.Value;
            _authService = authService;
        }

        private async Task<LoginSuccess> GenerateTokenAsync(ApplicationUser applicationUser)
        {
            var claimsIdentity = await CreateClaimsIdentityAsync(applicationUser);

            var created = DateTime.Now;
            var expires = created + TimeSpan.FromMilliseconds(_authorizationTokenSettings.Expires);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _authorizationTokenSettings.Audience,
                Issuer = _authorizationTokenSettings.Issuer,
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = claimsIdentity,
                NotBefore = created,
                Expires = expires
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return new LoginSuccess(expires, token);
        }

        private async Task<ClaimsIdentity> CreateClaimsIdentityAsync(ApplicationUser applicationUser)
        {
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ApplicationClaimTypes.Id, applicationUser.Id.ToString()),
                new Claim(ApplicationClaimTypes.Name, applicationUser.Name),
                new Claim(ApplicationClaimTypes.Email, applicationUser.Email),
                new Claim(ApplicationClaimTypes.Administrator, applicationUser.Administrator.ToString())
            });

            var claims = await _userManager.GetClaimsAsync(applicationUser);

            var roles = new List<Claim>();

            foreach (var roleName in await _userManager.GetRolesAsync(applicationUser))
            {
                var applicationRole = await _roleManager.FindByNameAsync(roleName);

                if (applicationRole.Active)
                {
                    roles.Add(new Claim(ClaimTypes.Role, applicationRole.Name));

                    (await _roleManager.GetClaimsAsync(applicationRole))
                        .Except(claims, new ClaimComparer())
                        .ForEach(c => claims.Add(c));
                }
            }

            claimsIdentity.AddClaims(roles);
            claimsIdentity.AddClaims(claims);

            return claimsIdentity;
        }

        private async Task<bool> CanSignInAsync(ApplicationUser applicationUser)
        {
            //if (applicationUser.Administrator)
            //{
            //    return applicationUser.Active;
            //}

            //if (_userManager.Options.SignIn.RequireConfirmedEmail && !await _userManager.IsEmailConfirmedAsync(applicationUser))
            //{
            //    return false;
            //}

            //if (_userManager.Options.SignIn.RequireConfirmedPhoneNumber && !(await _userManager.IsPhoneNumberConfirmedAsync(applicationUser)))
            //{
            //    return false;
            //}

            return applicationUser.Active;
        }

        private async Task<bool> IsLockedOut(ApplicationUser applicationUser) =>
            _userManager.SupportsUserLockout && await _userManager.IsLockedOutAsync(applicationUser);

        public async Task<LoginResult> SignInAsync(SignIn signIn)
        {
            if (string.IsNullOrWhiteSpace(signIn.Email) || string.IsNullOrWhiteSpace(signIn.Password))
            {
                return new LoginFailed(ValidationMessages.ContaAcessoInvalido);
            }

            var applicationUser = await _userManager.FindByEmailAsync(signIn.Email);

            if (applicationUser == null)
            {
                return new LoginFailed(ValidationMessages.ContaAcessoInvalido);
            }

            if (!await CanSignInAsync(applicationUser))
            {
                return new LoginFailed(ValidationMessages.ContaUsuarioSemPremissaoAcesso);
            }

            if (await IsLockedOut(applicationUser))
            {
                return new LoginFailed(ValidationMessages.ContaUsuarioBloqueado);
            }

            if (await _userManager.CheckPasswordAsync(applicationUser, signIn.Password))
            {
                await _userManager.ResetAccessFailedCountAsync(applicationUser);

                return await GenerateTokenAsync(applicationUser);
            }

            await _userManager.AccessFailedAsync(applicationUser);

            if (await _userManager.IsLockedOutAsync(applicationUser))
            {
                return new LoginFailed(ValidationMessages.ContaUsuarioBloqueado);
            }

            return new LoginFailed(ValidationMessages.ContaAcessoInvalido);
        }

        public async Task<Account> GetAuthenticatedUserAccountAsync()
        {
            var user = await _userManager.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == _authService.Id);
            return new Account(user.ConcurrencyStamp, user.Name, user.Email, user.PhoneNumber);
        }

        public async Task<NotificationResult> UpdateAuthenticatedUserAccountAsync(Account account)
        {
            var result = new NotificationResult();

            var applicationUser = await _userManager.FindByIdAsync(_authService.Id.ToString());

            result.AddNotifications(new NotificationContract().IsntNull(applicationUser, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract()
                .IsTrue(applicationUser.Active, ValidationMessages.RegistroInativoNaoPodeSerAtualizado)
                .AreEquals(applicationUser.ConcurrencyStamp, account.ConcurrencyStamp, ValidationMessages.RegistroConflitouAoExecutarComando));

            if (result.Invalid)
            {
                return result;
            }

            applicationUser.ChangeName(account.Name);
            applicationUser.ChangeEmail(account.Email);
            applicationUser.ChangePhoneNumber(account.PhoneNumber);

            result = applicationUser.Validate();

            if (result.Valid)
            {
                var identityResult = await _userManager.UpdateAsync(applicationUser);

                if (identityResult.Succeeded)
                {
                    return result;
                }

                result.AddNotifications(identityResult.Errors.Select(e => e.Description));
            }

            return result;
        }

        public async Task<NotificationResult> UpdateAuthenticatedUserPasswordAsync(UpdatePassword updatePassword)
        {
            var result = new NotificationResult();

            var applicationUser = await _userManager.FindByIdAsync(_authService.Id.ToString());

            result.AddNotifications(new NotificationContract().IsntNull(applicationUser, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract().IsTrue(applicationUser.Active, ValidationMessages.RegistroInativoNaoPodeSerAtualizado));

            if (result.Invalid)
            {
                return result;
            }

            var identityResult = await _userManager.ChangePasswordAsync(applicationUser, updatePassword.OldPassword, updatePassword.NewPassword);

            if (identityResult.Succeeded)
            {
                return result;
            }

            result.AddNotifications(identityResult.Errors.Select(e => e.Description));

            return result;
        }

        public void Dispose()
        {
            _userManager.Dispose();
            _roleManager.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
