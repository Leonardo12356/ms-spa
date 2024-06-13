using AutoMapper;
using ms_spa.Api.Contract.Produto;
using ms_spa.Api.Domain.Models;
using ms_spa.Api.Domain.Repository.Interfaces;
using ms_spa.Api.Domain.Services.Interfaces;
using ms_spa.Api.Exceptions;

namespace ms_spa.Api.Domain.Services.Classes
{
    public class ProdutoService(IProdutoRepository produtoRepository, IMapper mapper) : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ProdutoResponseContract> Adicionar(ProdutoRequestContract entidade)
        {
            var produto = _mapper.Map<Produto>(entidade);
            produto.DataCadastro = DateTime.Now;

            var result = await _produtoRepository.Adicionar(produto);
            return _mapper.Map<ProdutoResponseContract>(result);
        }


        public async Task<ProdutoResponseContract> Atualizar(int id, ProdutoRequestContract entidade)
        {
            _ = await ObterPorId(id) ?? throw new NotFoundException("Usuário não encontrado para atualização.");
            var produto = _mapper.Map<Produto>(entidade);
            produto.Id = id;

            produto = await _produtoRepository.Atualizar(produto);
            return _mapper.Map<ProdutoResponseContract>(produto);

        }

        public async Task Inativar(int id)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            await _produtoRepository.Deletar(_mapper.Map<Produto>(produto));
        }

        public async Task<ProdutoResponseContract> ObterPorId(int id)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            return _mapper.Map<ProdutoResponseContract>(produto);
        }


        public async Task<IEnumerable<ProdutoResponseContract>> ObterTodos()
        {
            var produtos = await _produtoRepository.ObterTodos();
            return produtos.Select(_mapper.Map<ProdutoResponseContract>);
        }

        public async Task<IEnumerable<ProdutoResponseContract>> ObterProdutosComMaiorEstoque(int quantidade)
        {
            var produtos = await _produtoRepository.ObterTodos();
            return produtos.OrderByDescending(p => p.QuantidadeEstoque)
                           .Take(quantidade)
                           .Select(p => new ProdutoResponseContract
                           {
                               Id = p.Id,
                               Nome = p.Nome,
                               QuantidadeEstoque = p.QuantidadeEstoque,
                               ValorCusto = p.ValorCusto,
                               ValorVenda = p.ValorVenda,
                               Observacao = p.Observacao,
                               DataCadastro = p.DataCadastro,
                           });
        }
        public async Task<IEnumerable<ProdutoResponseContract>> ObterProdutosComEstoqueZeradoOuNegativo()
        {
            var produtos = await _produtoRepository.ObterTodos();
            return produtos.Where(p => p.QuantidadeEstoque <= 0)
                            .Select(p => new ProdutoResponseContract
                            {
                                Id = p.Id,
                                Nome = p.Nome,
                                QuantidadeEstoque = p.QuantidadeEstoque,
                                ValorCusto = p.ValorCusto,
                                ValorVenda = p.ValorVenda,
                                Observacao = p.Observacao,
                                DataCadastro = p.DataCadastro,
                            });
        }


        public async Task<int> ObterQuantidadeTotalDeProdutos()
        {
            var produtos = await _produtoRepository.ObterTodos();
            return produtos.Count();
        }

    }
}