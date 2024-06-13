using ms_spa.Api.Contract.Produto;

namespace ms_spa.Api.Domain.Services.Interfaces
{
    public interface IProdutoService : IService<ProdutoRequestContract, ProdutoResponseContract, int>
    {
        Task<IEnumerable<ProdutoResponseContract>> ObterProdutosComMaiorEstoque(int quantidade);
        Task<IEnumerable<ProdutoResponseContract>> ObterProdutosComEstoqueZeradoOuNegativo();
        Task<int> ObterQuantidadeTotalDeProdutos();
    }
}