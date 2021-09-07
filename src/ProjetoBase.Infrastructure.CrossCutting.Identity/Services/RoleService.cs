using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoBase.Infrastructure.CrossCutting.Common.Attributes;
using ProjetoBase.Infrastructure.CrossCutting.Common.Enums;
using ProjetoBase.Infrastructure.CrossCutting.Common.Extensions;
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
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<NotificationResult> AddAsync(Role role)
        {
            var applicationRole = new ApplicationRole(role.Name, role.Description);

            var result = applicationRole.Validate();

            if (result.Invalid)
            {
                return result;
            }

            var identityResult = await _roleManager.CreateAsync(applicationRole);

            if (identityResult.Succeeded)
            {
                return result;
            }

            result.AddNotifications(identityResult.Errors.Select(e => e.Description));

            return result;
        }

        public async Task<NotificationResult> UpdateAsync(Role role)
        {
            var result = new NotificationResult();

            var applicationRole = await _roleManager.FindByIdAsync(role.Id.ToString());

            result.AddNotifications(new NotificationContract().IsntNull(applicationRole, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract()
                .IsTrue(applicationRole.Active, ValidationMessages.RegistroInativoNaoPodeSerAtualizado)
                .AreEquals(applicationRole.ConcurrencyStamp, role.ConcurrencyStamp, ValidationMessages.RegistroConflitouAoExecutarComando));

            if (result.Invalid)
            {
                return result;
            }

            applicationRole.ChangeName(role.Name);
            applicationRole.ChangeDescription(role.Description);

            result = applicationRole.Validate();

            if (result.Valid)
            {
                var identityResult = await _roleManager.UpdateAsync(applicationRole);

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

            var applicationRole = await _roleManager.FindByIdAsync(id.ToString());

            result.AddNotifications(new NotificationContract().IsntNull(applicationRole, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            var existsUsersInRole = (await _userManager.GetUsersInRoleAsync(applicationRole.Name)).Any();

            result.AddNotifications(new NotificationContract().IsFalse(existsUsersInRole, ValidationMessages.PapelNaoDevemExistirUsuariosAssociado));

            if (result.Valid)
            {
                var identityResult = await _roleManager.DeleteAsync(applicationRole);

                if (identityResult.Succeeded)
                {
                    return result;
                }

                result.AddNotifications(identityResult.Errors.Select(e => e.Description));
            }

            return result;
        }

        public async Task<NotificationResult> ActivateAsync(Guid id)
        {
            var result = new NotificationResult();

            var applicationRole = await _roleManager.FindByIdAsync(id.ToString());

            result.AddNotifications(new NotificationContract().IsntNull(applicationRole, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract().IsFalse(applicationRole.Active, ValidationMessages.RegistroAtivoNaoPodeSerAtivado));

            if (result.Valid)
            {
                applicationRole.Activate();

                var identityResult = await _roleManager.UpdateAsync(applicationRole);

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

            var applicationRole = await _roleManager.FindByIdAsync(id.ToString());

            result.AddNotifications(new NotificationContract().IsntNull(applicationRole, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract().IsTrue(applicationRole.Active, ValidationMessages.RegistroInativoNaoPodeSerDesativado));

            if (result.Valid)
            {
                applicationRole.Deactivate();

                var identityResult = await _roleManager.UpdateAsync(applicationRole);

                if (identityResult.Succeeded)
                {
                    return result;
                }

                result.AddNotifications(identityResult.Errors.Select(e => e.Description));
            }

            return result;
        }

        public async Task<Role> GetByIdAsync(Guid id)
        {
            var role = await _roleManager.Roles.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
            return new Role(role.Id, role.Active, role.ConcurrencyStamp, role.Name, role.Description);
        }

        public async Task<TableResult<RoleTable>> GetTableAsync(TableFilter filter)
        {
            var roles = _roleManager.Roles.AsNoTracking();

            var recordsTotal = await roles.CountAsync();

            filter.Searches?.ForEach(s =>
            {
                switch (s.Column)
                {
                    case nameof(RoleTable.Name):
                        roles = roles.Where(r => r.Name.ToLower().Contains(s.Value.ToLower()));
                        break;
                    case nameof(RoleTable.Description):
                        roles = roles.Where(r => r.Description.ToLower().Contains(s.Value.ToLower()));
                        break;
                }
            });

            var recordsFiltered = await roles.CountAsync();

            switch (filter.Sort?.Direction)
            {
                case SortDirection.Ascending:
                    switch (filter.Sort.Column)
                    {
                        case nameof(RoleTable.Name):
                            roles = roles.OrderBy(r => r.Name);
                            break;
                        case nameof(RoleTable.Description):
                            roles = roles.OrderBy(r => r.Description);
                            break;
                    }
                    break;
                case SortDirection.Descending:
                    switch (filter.Sort.Column)
                    {
                        case nameof(RoleTable.Name):
                            roles = roles.OrderByDescending(r => r.Name);
                            break;
                        case nameof(RoleTable.Description):
                            roles = roles.OrderByDescending(r => r.Description);
                            break;
                    }
                    break;
            }

            var data = await roles
                .Skip(filter.Length * (filter.Start - 1)).Take(filter.Length)
                .Select(r => new RoleTable(r.Id, r.Name, r.Description, r.Active))
                .ToListAsync();

            return new TableResult<RoleTable>(recordsTotal, recordsFiltered, data);
        }

        public async Task<SelectResult<Guid, string>> GetSelectAsync(params Guid[] identities)
        {
            var data = await _roleManager.Roles.AsNoTracking()
                .Where(r => identities.Any(i => i == r.Id))
                .Select(r => new KeyValuePair<Guid, string>(r.Id, r.Name))
                .ToListAsync();

            var recordsFiltered = data.Count();

            return new SelectResult<Guid, string>(recordsFiltered, data);
        }

        public async Task<SelectResult<Guid, string>> GetSelectAsync(SelectFilter filter)
        {
            var applicationRoles = _roleManager.Roles.AsNoTracking().Where(r => r.Active);

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                applicationRoles = applicationRoles.Where(r => r.Name.ToLower().Contains(filter.Search.ToLower()));
            }

            var recordsFiltered = await applicationRoles.CountAsync();

            var data = await applicationRoles.OrderBy(r => r.Name)
                .Skip(filter.Length * (filter.Start - 1)).Take(filter.Length)
                .Select(r => new KeyValuePair<Guid, string>(r.Id, r.Name))
                .ToListAsync();

            return new SelectResult<Guid, string>(recordsFiltered, data);
        }

        public async Task<NotificationResult> AddRoleClaimsAsync(RoleClaim roleClaim)
        {
            var result = new NotificationResult();

            var applicationRole = await _roleManager.FindByIdAsync(roleClaim.Id.ToString());

            result.AddNotifications(new NotificationContract().IsntNull(applicationRole, ValidationMessages.RegistroInexistente));

            if (result.Invalid)
            {
                return result;
            }

            result.AddNotifications(new NotificationContract()
                .IsTrue(applicationRole.Active, ValidationMessages.RegistroInativoNaoPodeSerAtualizado)
                .AreEquals(applicationRole.ConcurrencyStamp, roleClaim.ConcurrencyStamp, ValidationMessages.RegistroConflitouAoExecutarComando));

            if (result.Invalid)
            {
                return result;
            }

            var oldClaims = await _roleManager.GetClaimsAsync(applicationRole);

            var newClaims = Enumerable.Empty<Claim>();

            roleClaim.Menus.SelectMany(m => m.Items).ForEach(i =>
                i.Accesses.Where(a => a.Enabled).ForEach(a =>
                    newClaims = newClaims.Append(new Claim(i.Id.ToString(), a.Id.ToString()))
                    )
                );

            foreach (var claim in oldClaims.Except(newClaims, new ClaimComparer()))
            {
                var removeResult = await _roleManager.RemoveClaimAsync(applicationRole, claim);

                if (!removeResult.Succeeded)
                {
                    result.AddNotifications(removeResult.Errors.Select(e => e.Description));

                    return result;
                }
            }

            foreach (var claim in newClaims.Except(oldClaims, new ClaimComparer()))
            {
                var addResult = await _roleManager.AddClaimAsync(applicationRole, claim);

                if (!addResult.Succeeded)
                {
                    result.AddNotifications(addResult.Errors.Select(e => e.Description));

                    return result;
                }
            }

            var identityResult = await _roleManager.UpdateAsync(applicationRole);

            if (identityResult.Succeeded)
            {
                return result;
            }

            result.AddNotifications(identityResult.Errors.Select(e => e.Description));

            return result;
        }

        public async Task<RoleClaim> GetRoleClaimsAsync(Guid id)
        {
            var applicationRole = await _roleManager.FindByIdAsync(id.ToString());

            var claims = await _roleManager.GetClaimsAsync(applicationRole);

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

            return new RoleClaim(applicationRole.Id, applicationRole.ConcurrencyStamp, applicationRole.Name, menus);
        }

        public async Task<IEnumerable<MenuClaim>> GetMenuClaimsAsync(Guid id)
        {
            var applicationRole = await _roleManager.FindByIdAsync(id.ToString());

            var claims = await _roleManager.GetClaimsAsync(applicationRole);

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

        public void Dispose()
        {
            _roleManager.Dispose();
            _userManager.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
