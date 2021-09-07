using AutoMapper;
using ProjetoBase.Application.Interfaces;
using ProjetoBase.Application.ViewModels.Direitos;
using ProjetoBase.Application.ViewModels.Papeis;
using ProjetoBase.Infrastructure.CrossCutting.Common.Selects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Tables;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects;
using ProjetoBase.Infrastructure.CrossCutting.Identity.Interfaces;
using System;
using System.Collections.Generic;

namespace ProjetoBase.Application.Services
{
    public class PapelAppService : IPapelAppService
    {
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public PapelAppService(IMapper mapper, IRoleService roleService)
        {
            _mapper = mapper;
            _roleService = roleService;
        }

        public NotificationResult Adicionar(RoleViewModel viewModel)
        {
            var role = new Role(
                Guid.Empty,
                true,
                null,
                viewModel.Name,
                viewModel.Description);

            return _roleService.AddAsync(role).Result;
        }

        public NotificationResult Atualizar(RoleViewModel viewModel)
        {
            var role = new Role(
                viewModel.Id.GetValueOrDefault(),
                true,
                viewModel.ConcurrencyStamp,
                viewModel.Name,
                viewModel.Description);

            return _roleService.UpdateAsync(role).Result;
        }

        public NotificationResult Remover(Guid id) => _roleService.RemoveAsync(id).Result;

        public NotificationResult Ativar(Guid id) => _roleService.ActivateAsync(id).Result;

        public NotificationResult Desativar(Guid id) => _roleService.DeactivateAsync(id).Result;

        public RoleViewModel ObterPorId(Guid id) => _mapper.Map<RoleViewModel>(_roleService.GetByIdAsync(id).Result);

        public TableResult<RoleTableViewModel> ObterTabela(TableFilter filtro) =>
            _mapper.Map<TableResult<RoleTableViewModel>>(_roleService.GetTableAsync(filtro).Result);

        public NotificationResult AdicionarAutorizacoes(RoleClaimViewModel viewModel) =>
            _roleService.AddRoleClaimsAsync(_mapper.Map<RoleClaim>(viewModel)).Result;

        public RoleClaimViewModel ObterAutorizacoes(Guid id) => _mapper.Map<RoleClaimViewModel>(_roleService.GetRoleClaimsAsync(id).Result);

        public IEnumerable<MenuClaimViewModel> ObterMenusAutorizados(Guid id) =>
            _mapper.Map<IEnumerable<MenuClaimViewModel>>(_roleService.GetMenuClaimsAsync(id).Result);

        public SelectResult<Guid, string> ObterSelecao(params Guid[] identidades) => _roleService.GetSelectAsync(identidades).Result;

        public SelectResult<Guid, string> ObterSelecao(SelectFilter filtro) => _roleService.GetSelectAsync(filtro).Result;

        public void Dispose()
        {
            _roleService.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
