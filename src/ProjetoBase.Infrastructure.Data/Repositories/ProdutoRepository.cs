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
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ProjetoBaseContext context)
            : base(context)
        {
        }

        public Produto ObterPorNome(string nome) => DbEntity.AsNoTracking().FirstOrDefault(p => p.Nome == nome);

        public bool ExisteNome(string nome) => DbEntity.AsNoTracking().Any(p => p.Nome == nome);

        public bool ExisteNomeEmOutroProduto(Guid id, string nome) => DbEntity.AsNoTracking().Any(p => p.Id != id && p.Nome == nome);

        public bool ExistemProdutosAssociadoFornecedor(Guid fornecedorId) => DbEntity.AsNoTracking().Any(p => p.FornecedorId == fornecedorId);

        public bool ExistemProdutosAssociadoUnidadeMedida(Guid unidadeMedidaId) => DbEntity.AsNoTracking().Any(p => p.UnidadeMedidaId == unidadeMedidaId);

        public TableResult<ProdutoTable> ObterTabela(TableFilter filtro)
        {
            var produtos = DbEntity.Include(p => p.Fornecedor).Include(p => p.UnidadeMedida).AsNoTracking();

            var recordsTotal = produtos.Count();

            filtro.Searches?.ForEach(s =>
            {
                switch (s.Column)
                {
                    case nameof(ProdutoTable.Nome):
                        produtos = produtos.Where(p => p.Nome.ToLower().Contains(s.Value.ToLower()));
                        break;
                    case nameof(ProdutoTable.NomeFornecedor):
                        produtos = produtos.Where(p => p.Fornecedor.Nome.ToLower().Contains(s.Value.ToLower()) || p.Fornecedor.Documento.Contains(s.Value));
                        break;
                    case nameof(ProdutoTable.SiglaUnidadeMedida):
                        produtos = produtos.Where(p => p.UnidadeMedida.Sigla.ToLower().Contains(s.Value.ToLower()));
                        break;
                    case nameof(ProdutoTable.Valor):
                        produtos = produtos.Where(p => p.Valor.ToString().Contains(s.Value));
                        break;
                }
            });

            var recordsFiltered = produtos.Count();

            switch (filtro.Sort?.Direction)
            {
                case SortDirection.Ascending:
                    switch (filtro.Sort.Column)
                    {
                        case nameof(ProdutoTable.Nome):
                            produtos = produtos.OrderBy(p => p.Nome);
                            break;
                        case nameof(ProdutoTable.NomeFornecedor):
                            produtos = produtos.OrderBy(p => p.Fornecedor.Nome).ThenBy(p => p.Fornecedor.Documento);
                            break;
                        case nameof(ProdutoTable.SiglaUnidadeMedida):
                            produtos = produtos.OrderBy(p => p.UnidadeMedida.Sigla);
                            break;
                        case nameof(ProdutoTable.Valor):
                            produtos = produtos.OrderBy(p => p.Valor);
                            break;
                    }
                    break;
                case SortDirection.Descending:
                    switch (filtro.Sort.Column)
                    {
                        case nameof(ProdutoTable.Nome):
                            produtos = produtos.OrderByDescending(p => p.Nome);
                            break;
                        case nameof(ProdutoTable.NomeFornecedor):
                            produtos = produtos.OrderByDescending(p => p.Fornecedor.Nome).ThenByDescending(p => p.Fornecedor.Documento);
                            break;
                        case nameof(ProdutoTable.SiglaUnidadeMedida):
                            produtos = produtos.OrderByDescending(p => p.UnidadeMedida.Sigla);
                            break;
                        case nameof(ProdutoTable.Valor):
                            produtos = produtos.OrderByDescending(p => p.Valor);
                            break;
                    }
                    break;
            }

            var data = produtos
                .Skip(filtro.Length * (filtro.Start - 1)).Take(filtro.Length)
                .Select(p => new ProdutoTable(p.Id, p.Nome, $"{p.Fornecedor.Documento} - {p.Fornecedor.Nome}", p.UnidadeMedida.Sigla, p.Valor, p.Ativo))
                .ToList();

            return new TableResult<ProdutoTable>(recordsTotal, recordsFiltered, data);
        }

        public SelectResult<Guid, string> ObterSelecao(params Guid[] identidades)
        {
            var data = DbEntity.AsNoTracking().Where(p => identidades.Any(i => i == p.Id))
                .Select(p => new KeyValuePair<Guid, string>(p.Id, p.Nome))
                .ToList();

            var recordsFiltered = data.Count();

            return new SelectResult<Guid, string>(recordsFiltered, data);
        }

        public SelectResult<Guid, string> ObterSelecao(SelectFilter filtro)
        {
            var produtos = DbEntity.AsNoTracking().Where(p => p.Ativo);

            if (!string.IsNullOrWhiteSpace(filtro.Search))
            {
                produtos = produtos.Where(p => p.Nome.ToLower().Contains(filtro.Search.ToLower()));
            }

            var recordsFiltered = produtos.Count();

            var data = produtos.OrderBy(f => f.Nome)
                .Skip(filtro.Length * (filtro.Start - 1)).Take(filtro.Length)
                .Select(p => new KeyValuePair<Guid, string>(p.Id, p.Nome))
                .ToList();

            return new SelectResult<Guid, string>(recordsFiltered, data);
        }
    }
}
