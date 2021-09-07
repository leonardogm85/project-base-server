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
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(ProjetoBaseContext context)
            : base(context)
        {
        }

        public Fornecedor ObterPorDocumento(string documento) => DbEntity.AsNoTracking().FirstOrDefault(f => f.Documento == documento);

        public bool ExisteDocumento(string documento) => DbEntity.AsNoTracking().Any(f => f.Documento == documento);

        public bool ExisteDocumentoEmOutroFornecedor(Guid id, string documento) => DbEntity.AsNoTracking().Any(f => f.Id != id && f.Documento == documento);

        public TableResult<FornecedorTable> ObterTabela(TableFilter filtro)
        {
            var fornecedores = DbEntity.AsNoTracking();

            var recordsTotal = fornecedores.Count();

            filtro.Searches?.ForEach(s =>
            {
                switch (s.Column)
                {
                    case nameof(FornecedorTable.Nome):
                        fornecedores = fornecedores.Where(f => f.Nome.ToLower().Contains(s.Value.ToLower()));
                        break;
                    case nameof(FornecedorTable.Documento):
                        fornecedores = fornecedores.Where(f => f.Documento.Contains(s.Value));
                        break;
                    case nameof(FornecedorTable.Email):
                        fornecedores = fornecedores.Where(f => f.Email.ToLower().Contains(s.Value.ToLower()));
                        break;
                    case nameof(FornecedorTable.Celular):
                        fornecedores = fornecedores.Where(f => f.Celular.Contains(s.Value));
                        break;
                }
            });

            var recordsFiltered = fornecedores.Count();

            switch (filtro.Sort?.Direction)
            {
                case SortDirection.Ascending:
                    switch (filtro.Sort.Column)
                    {
                        case nameof(FornecedorTable.Nome):
                            fornecedores = fornecedores.OrderBy(f => f.Nome);
                            break;
                        case nameof(FornecedorTable.Documento):
                            fornecedores = fornecedores.OrderBy(f => f.Documento);
                            break;
                        case nameof(FornecedorTable.Email):
                            fornecedores = fornecedores.OrderBy(f => f.Email);
                            break;
                        case nameof(FornecedorTable.Celular):
                            fornecedores = fornecedores.OrderBy(f => f.Celular);
                            break;
                    }
                    break;
                case SortDirection.Descending:
                    switch (filtro.Sort.Column)
                    {
                        case nameof(FornecedorTable.Nome):
                            fornecedores = fornecedores.OrderByDescending(f => f.Nome);
                            break;
                        case nameof(FornecedorTable.Documento):
                            fornecedores = fornecedores.OrderByDescending(f => f.Documento);
                            break;
                        case nameof(FornecedorTable.Email):
                            fornecedores = fornecedores.OrderByDescending(f => f.Email);
                            break;
                        case nameof(FornecedorTable.Celular):
                            fornecedores = fornecedores.OrderByDescending(f => f.Celular);
                            break;
                    }
                    break;
            }

            var data = fornecedores
                .Skip(filtro.Length * (filtro.Start - 1)).Take(filtro.Length)
                .Select(f => new FornecedorTable(f.Id, f.Nome, f.Documento, f.Email, f.Celular, f.Ativo))
                .ToList();

            return new TableResult<FornecedorTable>(recordsTotal, recordsFiltered, data);
        }

        public SelectResult<Guid, string> ObterSelecao(params Guid[] identidades)
        {
            var data = DbEntity.AsNoTracking().Where(f => identidades.Any(i => i == f.Id))
                .Select(f => new KeyValuePair<Guid, string>(f.Id, $"{f.Documento} - {f.Nome}"))
                .ToList();

            var recordsFiltered = data.Count();

            return new SelectResult<Guid, string>(recordsFiltered, data);
        }

        public SelectResult<Guid, string> ObterSelecao(SelectFilter filtro)
        {
            var fornecedores = DbEntity.AsNoTracking().Where(f => f.Ativo);

            if (!string.IsNullOrWhiteSpace(filtro.Search))
            {
                fornecedores = fornecedores.Where(f => f.Documento.Contains(filtro.Search) || f.Nome.ToLower().Contains(filtro.Search.ToLower()));
            }

            var recordsFiltered = fornecedores.Count();

            var data = fornecedores.OrderBy(f => f.Nome)
                .Skip(filtro.Length * (filtro.Start - 1)).Take(filtro.Length)
                .Select(f => new KeyValuePair<Guid, string>(f.Id, $"{f.Documento} - {f.Nome}"))
                .ToList();

            return new SelectResult<Guid, string>(recordsFiltered, data);
        }
    }
}
