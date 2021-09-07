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
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ProjetoBaseContext context)
            : base(context)
        {
        }

        public Cliente ObterPorDocumento(string documento) => DbEntity.AsNoTracking().FirstOrDefault(c => c.Documento == documento);

        public bool ExisteDocumento(string documento) => DbEntity.AsNoTracking().Any(c => c.Documento == documento);

        public bool ExisteDocumentoEmOutroCliente(Guid id, string documento) => DbEntity.AsNoTracking().Any(c => c.Id != id && c.Documento == documento);

        public TableResult<ClienteTable> ObterTabela(TableFilter filtro)
        {
            var clientes = DbEntity.AsNoTracking();

            var recordsTotal = clientes.Count();

            filtro.Searches?.ForEach(s =>
            {
                switch (s.Column)
                {
                    case nameof(ClienteTable.Nome):
                        clientes = clientes.Where(c => c.Nome.ToLower().Contains(s.Value.ToLower()));
                        break;
                    case nameof(ClienteTable.Documento):
                        clientes = clientes.Where(c => c.Documento.Contains(s.Value));
                        break;
                    case nameof(ClienteTable.Email):
                        clientes = clientes.Where(c => c.Email.ToLower().Contains(s.Value.ToLower()));
                        break;
                    case nameof(ClienteTable.Celular):
                        clientes = clientes.Where(c => c.Celular.Contains(s.Value));
                        break;
                }
            });

            var recordsFiltered = clientes.Count();

            switch (filtro.Sort?.Direction)
            {
                case SortDirection.Ascending:
                    switch (filtro.Sort.Column)
                    {
                        case nameof(ClienteTable.Nome):
                            clientes = clientes.OrderBy(c => c.Nome);
                            break;
                        case nameof(ClienteTable.Documento):
                            clientes = clientes.OrderBy(c => c.Documento);
                            break;
                        case nameof(ClienteTable.Email):
                            clientes = clientes.OrderBy(c => c.Email);
                            break;
                        case nameof(ClienteTable.Celular):
                            clientes = clientes.OrderBy(c => c.Celular);
                            break;
                    }
                    break;
                case SortDirection.Descending:
                    switch (filtro.Sort.Column)
                    {
                        case nameof(ClienteTable.Nome):
                            clientes = clientes.OrderByDescending(c => c.Nome);
                            break;
                        case nameof(ClienteTable.Documento):
                            clientes = clientes.OrderByDescending(c => c.Documento);
                            break;
                        case nameof(ClienteTable.Email):
                            clientes = clientes.OrderByDescending(c => c.Email);
                            break;
                        case nameof(ClienteTable.Celular):
                            clientes = clientes.OrderByDescending(c => c.Celular);
                            break;
                    }
                    break;
            }

            var data = clientes
                .Skip(filtro.Length * (filtro.Start - 1)).Take(filtro.Length)
                .Select(c => new ClienteTable(c.Id, c.Nome, c.Documento, c.Email, c.Celular, c.Ativo))
                .ToList();

            return new TableResult<ClienteTable>(recordsTotal, recordsFiltered, data);
        }

        public SelectResult<Guid, string> ObterSelecao(params Guid[] identidades)
        {
            var data = DbEntity.AsNoTracking().Where(c => identidades.Any(i => i == c.Id))
                .Select(c => new KeyValuePair<Guid, string>(c.Id, $"{c.Documento} - {c.Nome}"))
                .ToList();

            var recordsFiltered = data.Count();

            return new SelectResult<Guid, string>(recordsFiltered, data);
        }

        public SelectResult<Guid, string> ObterSelecao(SelectFilter filtro)
        {
            var clientes = DbEntity.AsNoTracking().Where(c => c.Ativo);

            if (!string.IsNullOrWhiteSpace(filtro.Search))
            {
                clientes = clientes.Where(c => c.Documento.Contains(filtro.Search) || c.Nome.ToLower().Contains(filtro.Search.ToLower()));
            }

            var recordsFiltered = clientes.Count();

            var data = clientes.OrderBy(c => c.Nome)
                .Skip(filtro.Length * (filtro.Start - 1)).Take(filtro.Length)
                .Select(c => new KeyValuePair<Guid, string>(c.Id, $"{c.Documento} - {c.Nome}"))
                .ToList();

            return new SelectResult<Guid, string>(recordsFiltered, data);
        }
    }
}
