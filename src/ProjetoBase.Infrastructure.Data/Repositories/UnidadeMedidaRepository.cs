using Microsoft.EntityFrameworkCore;
using ProjetoBase.Domain.DataTransferObjects;
using ProjetoBase.Domain.Entities;
using ProjetoBase.Domain.Interfaces.Repositories;
using ProjetoBase.Infrastructure.CrossCutting.Common.Enums;
using ProjetoBase.Infrastructure.CrossCutting.Common.Extensions;
using ProjetoBase.Infrastructure.CrossCutting.Common.Selects;
using ProjetoBase.Infrastructure.CrossCutting.Common.Tables;
using ProjetoBase.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoBase.Infrastructure.Data.Repositories
{
    public class UnidadeMedidaRepository : Repository<UnidadeMedida>, IUnidadeMedidaRepository
    {
        public UnidadeMedidaRepository(ProjetoBaseContext context)
            : base(context)
        {
        }

        public UnidadeMedida ObterPorSigla(string sigla) => DbEntity.AsNoTracking().FirstOrDefault(u => u.Sigla == sigla);

        public bool ExisteSigla(string sigla) => DbEntity.AsNoTracking().Any(u => u.Sigla == sigla);

        public bool ExisteSiglaEmOutraUnidadeMedida(Guid id, string sigla) => DbEntity.AsNoTracking().Any(u => u.Id != id && u.Sigla == sigla);

        public TableResult<UnidadeMedidaTable> ObterTabela(TableFilter filtro)
        {
            var unidadesMedida = DbEntity.AsNoTracking();

            var recordsTotal = unidadesMedida.Count();

            filtro.Searches?.ForEach(s =>
            {
                switch (s.Column)
                {
                    case nameof(UnidadeMedidaTable.Nome):
                        unidadesMedida = unidadesMedida.Where(u => u.Nome.ToLower().Contains(s.Value.ToLower()));
                        break;
                    case nameof(UnidadeMedidaTable.Sigla):
                        unidadesMedida = unidadesMedida.Where(u => u.Sigla.ToLower().Contains(s.Value.ToLower()));
                        break;
                }
            });

            var recordsFiltered = unidadesMedida.Count();

            switch (filtro.Sort?.Direction)
            {
                case SortDirection.Ascending:
                    switch (filtro.Sort.Column)
                    {
                        case nameof(UnidadeMedidaTable.Nome):
                            unidadesMedida = unidadesMedida.OrderBy(u => u.Nome);
                            break;
                        case nameof(UnidadeMedidaTable.Sigla):
                            unidadesMedida = unidadesMedida.OrderBy(u => u.Sigla);
                            break;
                    }
                    break;
                case SortDirection.Descending:
                    switch (filtro.Sort.Column)
                    {
                        case nameof(UnidadeMedidaTable.Nome):
                            unidadesMedida = unidadesMedida.OrderByDescending(u => u.Nome);
                            break;
                        case nameof(UnidadeMedidaTable.Sigla):
                            unidadesMedida = unidadesMedida.OrderByDescending(u => u.Sigla);
                            break;
                    }
                    break;
            }

            var data = unidadesMedida
                .Skip(filtro.Length * (filtro.Start - 1)).Take(filtro.Length)
                .Select(u => new UnidadeMedidaTable(u.Id, u.Nome, u.Sigla, u.Ativo))
                .ToList();

            return new TableResult<UnidadeMedidaTable>(recordsTotal, recordsFiltered, data);
        }

        public SelectResult<Guid, string> ObterSelecao(params Guid[] identidades)
        {
            var data = DbEntity.AsNoTracking().Where(u => identidades.Any(i => i == u.Id))
                .Select(u => new KeyValuePair<Guid, string>(u.Id, u.Sigla))
                .ToList();

            var recordsFiltered = data.Count();

            return new SelectResult<Guid, string>(recordsFiltered, data);
        }

        public SelectResult<Guid, string> ObterSelecao(SelectFilter filtro)
        {
            var unidadesMedida = DbEntity.AsNoTracking().Where(u => u.Ativo);

            if (!string.IsNullOrWhiteSpace(filtro.Search))
            {
                unidadesMedida = unidadesMedida.Where(u => u.Sigla.ToLower().Contains(filtro.Search.ToLower()));
            }

            var recordsFiltered = unidadesMedida.Count();

            var data = unidadesMedida.OrderBy(u => u.Sigla)
                .Skip(filtro.Length * (filtro.Start - 1)).Take(filtro.Length)
                .Select(u => new KeyValuePair<Guid, string>(u.Id, u.Sigla))
                .ToList();

            return new SelectResult<Guid, string>(recordsFiltered, data);
        }
    }
}
