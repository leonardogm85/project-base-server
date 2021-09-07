using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoBase.Infrastructure.CrossCutting.Common.Attributes;
using ProjetoBase.Infrastructure.CrossCutting.Common.Enums;
using ProjetoBase.Infrastructure.CrossCutting.Common.Extensions;
using ProjetoBase.Infrastructure.CrossCutting.Common.Interfaces;
using ProjetoBase.Infrastructure.CrossCutting.Common.Selects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Tables;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Constants;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Entities;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Interfaces;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IEmailService _emailService;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        public async Task<NotificationResult> AddAsync(User user)
        {
            var applicationUser = new ApplicationUser(user.Name, user.Email, user.PhoneNumber, user.Administrator);

            var result = applicationUser.Validate();

            result.AddNotifications(new NotificationContract()
                .IsntNullOrWhiteSpace(user.Password, ValidationMessages.UsuarioSenhaDeveSerPreenchido)
                .HasMinLength(user.Password, 6, ValidationMessages.UsuarioSenhaDeveTerUmTamanhoMinimo)
                .HasMaxLength(user.Password, 100, ValidationMessages.UsuarioSenhaDeveTerUmTamanhoMaximo));

            if (result.Valid)
            {
                var identityResult = await _userManager.CreateAsync(applicationUser, user.Password);

                if (identityResult.Succeeded)
                {
                    await SendEmailConfirmationTokenAsync(applicationUser.Id);

                    return result;
                }

                result.AddNotifications(identityResult.Errors.Select(e => e.Description));
            }

            return result;
        }

        public async Task<NotificationResult> UpdateAsync(User user)
        {
            var result = new NotificationResult();

            var applicationUser = await _userManager.FindByIdAsync(user.Id.ToString());

            result.AddNotifications(new NotificationContract().IsntNull(applicationUser, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract()
                .IsTrue(applicationUser.Active, ValidationMessages.RegistroInativoNaoPodeSerAtualizado)
                .IsFalse(applicationUser.Administrator, ValidationMessages.UsuarioAdministradorNaoPodeSerAtualizado)
                .AreEquals(applicationUser.ConcurrencyStamp, user.ConcurrencyStamp, ValidationMessages.RegistroConflitouAoExecutarComando));

            if (result.Invalid)
            {
                return result;
            }

            applicationUser.ChangeName(user.Name);
            applicationUser.ChangeEmail(user.Email);
            applicationUser.ChangePhoneNumber(user.PhoneNumber);

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

        public async Task<NotificationResult> RemoveAsync(Guid id)
        {
            var result = new NotificationResult();

            result.AddNotification(ValidationMessages.UsuarioNaoPodeSerRemovido);

            return await Task.FromResult(result);
        }

        public async Task<NotificationResult> ActivateAsync(Guid id)
        {
            var result = new NotificationResult();

            var user = await _userManager.FindByIdAsync(id.ToString());

            result.AddNotifications(new NotificationContract().IsntNull(user, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract()
                .IsFalse(user.Active, ValidationMessages.RegistroAtivoNaoPodeSerAtivado)
                .IsFalse(user.Administrator, ValidationMessages.UsuarioAdministradorNaoPodeSerAtualizado));

            if (result.Valid)
            {
                user.Activate();

                var identityResult = await _userManager.UpdateAsync(user);

                if (identityResult.Succeeded)
                {
                    return result;
                }

                result.AddNotifications(identityResult.Errors.Select(e => e.Description));
            }

            return result;
        }

        public async Task<NotificationResult> DeactivateAsync(Guid id)
        {
            var result = new NotificationResult();

            var applicationUser = await _userManager.FindByIdAsync(id.ToString());

            result.AddNotifications(new NotificationContract().IsntNull(applicationUser, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract()
                .IsTrue(applicationUser.Active, ValidationMessages.RegistroInativoNaoPodeSerDesativado)
                .IsFalse(applicationUser.Administrator, ValidationMessages.UsuarioAdministradorNaoPodeSerAtualizado));

            if (result.Valid)
            {
                applicationUser.Deactivate();

                var identityResult = await _userManager.UpdateAsync(applicationUser);

                if (identityResult.Succeeded)
                {
                    return result;
                }

                result.AddNotifications(identityResult.Errors.Select(e => e.Description));
            }

            return result;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _userManager.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
            return new User(user.Id, user.Active, user.ConcurrencyStamp, user.Name, user.Email, user.PhoneNumber, user.Administrator);
        }

        public async Task<NotificationResult> ResetPasswordAsync(ResetPassword resetPassword)
        {
            var result = new NotificationResult();

            var applicationUser = await _userManager.FindByEmailAsync(resetPassword.Email);

            result.AddNotifications(new NotificationContract().IsntNull(applicationUser, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract().IsTrue(applicationUser.Active, ValidationMessages.RegistroInativoNaoPodeSerAtualizado));

            if (result.Valid)
            {
                var identityResult = await _userManager.ResetPasswordAsync(applicationUser, resetPassword.Token, resetPassword.NewPassword);

                if (identityResult.Succeeded)
                {
                    return result;
                }

                result.AddNotifications(identityResult.Errors.Select(e => e.Description));
            }

            return result;
        }

        public async Task<NotificationResult> ForgotPasswordAsync(ForgotPassword forgotPassword)
        {
            var result = new NotificationResult();

            var applicationUser = await _userManager.FindByEmailAsync(forgotPassword.Email);

            result.AddNotifications(new NotificationContract().IsntNull(applicationUser, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract().IsTrue(applicationUser.Active, ValidationMessages.RegistroInativoNaoPodeSerAtualizado));

            if (result.Valid)
            {
                return await SendPasswordResetTokenAsync(applicationUser.Id);
            }

            return result;
        }

        public async Task<NotificationResult> ConfirmEmailAsync(ConfirmEmail confirmEmail)
        {
            var result = new NotificationResult();

            var applicationUser = await _userManager.FindByIdAsync(confirmEmail.Id.ToString());

            result.AddNotifications(new NotificationContract().IsntNull(applicationUser, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract()
                .IsTrue(applicationUser.Active, ValidationMessages.RegistroInativoNaoPodeSerAtualizado)
                .IsFalse(applicationUser.EmailConfirmed, ValidationMessages.UsuarioEmailNaoDeveEstarConfirmado));

            if (result.Valid)
            {
                var identityResult = await _userManager.ConfirmEmailAsync(applicationUser, confirmEmail.Token);

                if (identityResult.Succeeded)
                {
                    return result;
                }

                result.AddNotifications(identityResult.Errors.Select(e => e.Description));
            }

            return result;
        }

        public async Task<NotificationResult> SendEmailConfirmationTokenAsync(Guid id)
        {
            var result = new NotificationResult();

            var applicationUser = await _userManager.FindByIdAsync(id.ToString());

            result.AddNotifications(new NotificationContract().IsntNull(applicationUser, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract()
                .IsTrue(applicationUser.Active, ValidationMessages.RegistroInativoNaoPodeSerAtualizado)
                .IsFalse(applicationUser.EmailConfirmed, ValidationMessages.UsuarioEmailNaoDeveEstarConfirmado));

            if (result.Valid)
            {
                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);

                await _emailService.SendEmailAsync(
                    applicationUser.Email,
                    "Confirme seu email",
                    $"Por favor, confirme sua conta clicando aqui. Id: {applicationUser.Id} e Token: {emailConfirmationToken}.");
            }

            return result;
        }

        public async Task<NotificationResult> SendPasswordResetTokenAsync(Guid id)
        {
            var result = new NotificationResult();

            var applicationUser = await _userManager.FindByIdAsync(id.ToString());

            result.AddNotifications(new NotificationContract().IsntNull(applicationUser, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract()
                .IsTrue(applicationUser.Active, ValidationMessages.RegistroInativoNaoPodeSerAtualizado)
                .IsTrue(applicationUser.EmailConfirmed, ValidationMessages.UsuarioEmailDeveEstarConfirmado));

            if (result.Valid)
            {
                var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);

                await _emailService.SendEmailAsync(
                    applicationUser.Email,
                    "Redefina sua senha",
                    $"Por favor, redefina sua senha clicando aqui. Token: {passwordResetToken}.");
            }

            return result;
        }

        public async Task<bool> ExistsAdministratorAsync() => await _userManager.Users.AsNoTracking().AnyAsync(u => u.Administrator);

        public async Task<TableResult<UserTable>> GetTableAsync(TableFilter filter)
        {
            var users = _userManager.Users.AsNoTracking();

            var recordsTotal = await users.CountAsync();

            filter.Searches?.ForEach(s =>
            {
                switch (s.Column)
                {
                    case nameof(UserTable.Name):
                        users = users.Where(u => u.Name.ToLower().Contains(s.Value.ToLower()));
                        break;
                    case nameof(UserTable.Email):
                        users = users.Where(u => u.Email.ToLower().Contains(s.Value.ToLower()));
                        break;
                    case nameof(UserTable.PhoneNumber):
                        users = users.Where(u => u.PhoneNumber.Contains(s.Value));
                        break;
                }
            });

            var recordsFiltered = await users.CountAsync();

            switch (filter.Sort?.Direction)
            {
                case SortDirection.Ascending:
                    switch (filter.Sort.Column)
                    {
                        case nameof(UserTable.Name):
                            users = users.OrderBy(u => u.Name);
                            break;
                        case nameof(UserTable.Email):
                            users = users.OrderBy(u => u.Email);
                            break;
                        case nameof(UserTable.PhoneNumber):
                            users = users.OrderBy(u => u.PhoneNumber);
                            break;
                    }
                    break;
                case SortDirection.Descending:
                    switch (filter.Sort.Column)
                    {
                        case nameof(UserTable.Name):
                            users = users.OrderByDescending(u => u.Name);
                            break;
                        case nameof(UserTable.Email):
                            users = users.OrderByDescending(u => u.Email);
                            break;
                        case nameof(UserTable.PhoneNumber):
                            users = users.OrderByDescending(u => u.PhoneNumber);
                            break;
                    }
                    break;
            }

            var data = await users
                .Skip(filter.Length * (filter.Start - 1)).Take(filter.Length)
                .Select(u => new UserTable(u.Id, u.Name, u.Email, u.PhoneNumber, u.Administrator, u.EmailConfirmed, u.Active))
                .ToListAsync();

            return new TableResult<UserTable>(recordsTotal, recordsFiltered, data);
        }

        public async Task<NotificationResult> AddUserClaimsAsync(UserClaim userClaim)
        {
            var result = new NotificationResult();

            var applicationUser = await _userManager.FindByIdAsync(userClaim.Id.ToString());

            result.AddNotifications(new NotificationContract().IsntNull(applicationUser, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract()
                .IsTrue(applicationUser.Active, ValidationMessages.RegistroInativoNaoPodeSerAtualizado)
                .IsFalse(applicationUser.Administrator, ValidationMessages.UsuarioAdministradorNaoPodeSerAtualizado)
                .AreEquals(applicationUser.ConcurrencyStamp, userClaim.ConcurrencyStamp, ValidationMessages.RegistroConflitouAoExecutarComando));

            if (result.Invalid)
            {
                return result;
            }

            var oldClaims = await _userManager.GetClaimsAsync(applicationUser);

            var newClaims = Enumerable.Empty<Claim>();

            userClaim.Menus.SelectMany(m => m.Items).ForEach(i =>
                i.Accesses.Where(a => a.Enabled).ForEach(a =>
                    newClaims = newClaims.Append(new Claim(i.Id.ToString(), a.Id.ToString()))
                    )
                );

            var identityResult = await _userManager.RemoveClaimsAsync(applicationUser, oldClaims.Except(newClaims, new ClaimComparer()));

            if (!identityResult.Succeeded)
            {
                result.AddNotifications(identityResult.Errors.Select(e => e.Description));

                return result;
            }

            identityResult = await _userManager.AddClaimsAsync(applicationUser, newClaims.Except(oldClaims, new ClaimComparer()));

            if (!identityResult.Succeeded)
            {
                result.AddNotifications(identityResult.Errors.Select(e => e.Description));

                return result;
            }

            identityResult = await _userManager.UpdateAsync(applicationUser);

            if (identityResult.Succeeded)
            {
                return result;
            }

            result.AddNotifications(identityResult.Errors.Select(e => e.Description));

            return result;
        }

        public async Task<UserClaim> GetUserClaimsAsync(Guid id)
        {
            var applicationUser = await _userManager.FindByIdAsync(id.ToString());

            var claims = await _userManager.GetClaimsAsync(applicationUser);

            var accesses = typeof(Access).GetAll<Access>();

            var menus = typeof(Item).GetAll<Item>()
                .GroupBy(g => (Menu)g.GetAttribute<AttachmentAttribute>().Attachment)
                .OrderBy(g => g.Key.GetAttribute<OrderAttribute>().Order)
                .Select(g =>
                    new MenuClaim(
                        g.Key.GetValue<int>(),
                        g.Key.GetAttribute<DescriptionAttribute>().Description,
                        g
                            .OrderBy(i => i.GetAttribute<OrderAttribute>().Order)
                            .Select(i =>
                                new ItemClaim(
                                    i.GetValue<int>(),
                                    i.GetAttribute<DescriptionAttribute>().Description,
                                    accesses
                                        .Select(a =>
                                            new AccessClaim(
                                                a.GetValue<int>(),
                                                a.GetAttribute<DescriptionAttribute>().Description,
                                                claims.Any(c => c.Type == i.GetValue<int>().ToString() && c.Value == a.GetValue<int>().ToString())
                                                )
                                            )
                                    )
                                )
                        )
                    );

            return new UserClaim(applicationUser.Id, applicationUser.ConcurrencyStamp, applicationUser.Name, menus);
        }

        public async Task<IEnumerable<MenuClaim>> GetMenuClaimsAsync(Guid id)
        {
            var applicationUser = await _userManager.FindByIdAsync(id.ToString());

            var claims = await _userManager.GetClaimsAsync(applicationUser);

            var types = claims.Select(c => c.Type).Distinct();

            var accesses = typeof(Access).GetAll<Access>();

            return typeof(Item).GetAll<Item>()
                .Where(i => types.Any(t => i.GetValue<int>().ToString() == t))
                .GroupBy(g => (Menu)g.GetAttribute<AttachmentAttribute>().Attachment)
                .OrderBy(g => g.Key.GetAttribute<OrderAttribute>().Order)
                .Select(g =>
                    new MenuClaim(
                        g.Key.GetValue<int>(),
                        g.Key.GetAttribute<DescriptionAttribute>().Description,
                        g
                            .OrderBy(i => i.GetAttribute<OrderAttribute>().Order)
                            .Select(i =>
                                new ItemClaim(
                                    i.GetValue<int>(),
                                    i.GetAttribute<DescriptionAttribute>().Description,
                                    accesses
                                        .Select(a =>
                                            new AccessClaim(
                                                a.GetValue<int>(),
                                                a.GetAttribute<DescriptionAttribute>().Description,
                                                claims.Any(c => c.Type == i.GetValue<int>().ToString() && c.Value == a.GetValue<int>().ToString())
                                                )
                                            )
                                    )
                                )
                        )
                    );
        }

        public async Task<NotificationResult> AddUserRolesAsync(UserRole userRole)
        {
            var result = new NotificationResult();

            var applicationUser = await _userManager.FindByIdAsync(userRole.Id.ToString());

            result.AddNotifications(new NotificationContract().IsntNull(applicationUser, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract()
                .IsTrue(applicationUser.Active, ValidationMessages.RegistroInativoNaoPodeSerAtualizado)
                .IsFalse(applicationUser.Administrator, ValidationMessages.UsuarioAdministradorNaoPodeSerAtualizado)
                .AreEquals(applicationUser.ConcurrencyStamp, userRole.ConcurrencyStamp, ValidationMessages.RegistroConflitouAoExecutarComando));

            if (result.Invalid)
            {
                return result;
            }

            var oldRole = await _userManager.GetRolesAsync(applicationUser);

            var newRole = await _roleManager.Roles.AsNoTracking()
                .Where(r => userRole.Roles.Any(i => i == r.Id))
                .Select(r => r.Name)
                .ToListAsync();

            var identityResult = await _userManager.RemoveFromRolesAsync(applicationUser, oldRole.Except(newRole));

            if (!identityResult.Succeeded)
            {
                result.AddNotifications(identityResult.Errors.Select(e => e.Description));

                return result;
            }

            identityResult = await _userManager.AddToRolesAsync(applicationUser, newRole.Except(oldRole));

            if (!identityResult.Succeeded)
            {
                result.AddNotifications(identityResult.Errors.Select(e => e.Description));

                return result;
            }

            identityResult = await _userManager.UpdateAsync(applicationUser);

            if (identityResult.Succeeded)
            {
                return result;
            }

            result.AddNotifications(identityResult.Errors.Select(e => e.Description));

            return result;
        }

        public async Task<UserRole> GetUserRolesAsync(Guid id)
        {
            var applicationUser = await _userManager.FindByIdAsync(id.ToString());

            var roleNames = await _userManager.GetRolesAsync(applicationUser);

            var roles = await _roleManager.Roles.AsNoTracking()
                .Where(r => roleNames.Any(n => n == r.Name))
                .Select(r => r.Id)
                .ToListAsync();

            return new UserRole(applicationUser.Id, applicationUser.ConcurrencyStamp, applicationUser.Name, roles);
        }

        public async Task<IEnumerable<string>> GetRoleNamesAsync(Guid id) => await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(id.ToString()));

        public async Task<SelectResult<Guid, string>> GetSelectAsync(params Guid[] identities)
        {
            var data = await _userManager.Users.AsNoTracking()
                .Where(u => identities.Any(i => i == u.Id))
                .Select(u => new KeyValuePair<Guid, string>(u.Id, $"{u.Name} - {u.Email}"))
                .ToListAsync();

            var recordsFiltered = data.Count();

            return new SelectResult<Guid, string>(recordsFiltered, data);
        }

        public async Task<SelectResult<Guid, string>> GetSelectAsync(SelectFilter filter)
        {
            var applicationUsers = _userManager.Users.AsNoTracking().Where(u => u.Active);

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                applicationUsers = applicationUsers.Where(u => u.Name.ToLower().Contains(filter.Search.ToLower()));
            }

            var recordsFiltered = await applicationUsers.CountAsync();

            var data = await applicationUsers.OrderBy(u => u.Name)
                .Skip(filter.Length * (filter.Start - 1)).Take(filter.Length)
                .Select(u => new KeyValuePair<Guid, string>(u.Id, $"{u.Name} - {u.Email}"))
                .ToListAsync();

            return new SelectResult<Guid, string>(recordsFiltered, data);
        }

        public void Dispose()
        {
            _userManager.Dispose();
            _roleManager.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
