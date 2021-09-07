using ProjetoBase.Domain.DataTransferObjects;
using ProjetoBase.Domain.Entities;
using System;

namespace ProjetoBase.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>, ITabelaRepository<ProdutoTable>, ISelecaoRepository<Guid, string>
    {
        Produto ObterPorNome(string nome);
        bool ExisteNome(string nome);
        bool ExisteNomeEmOutroProduto(Guid id, string nome);
        bool ExistemProdutosAssociadoFornecedor(Guid fornecedorId);
        bool ExistemProdutosAssociadoUnidadeMedida(Guid unidadeMedidaId);
    }
}
