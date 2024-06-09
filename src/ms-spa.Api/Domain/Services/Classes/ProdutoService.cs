using AutoMapper;
using ms_spa.Api.Contract.Produto;
using ms_spa.Api.Domain.Models;
using ms_spa.Api.Domain.Repository.Interfaces;
using ms_spa.Api.Domain.Services.Interfaces;
using ms_spa.Api.Exceptions;

namespace ms_spa.Api.Domain.Services.Classes
{
    public class ProdutoService(IProdutoRepository produtoRepository, IMapper mapper, IClienteRepository clienteRepository, IUsuarioRepository usuarioRepository) : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly IClienteRepository _clienteRepository = clienteRepository;
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ProdutoResponseContract> Adicionar(ProdutoRequestContract entidade, int idUsuario)
        {
            _ = await _clienteRepository.ObterPorId(idUsuario) ?? throw new NotFoundException("Cliente não encontrado para associar o produto.");

            _ = await _clienteRepository.ObterPorId(idUsuario) ?? throw new NotFoundException("Cliente não encontrado para associar o produto.");
            var produto = _mapper.Map<Produto>(entidade);
            produto.DataCadastro = DateTime.Now;

            var result = await _produtoRepository.Adicionar(produto);
            return _mapper.Map<ProdutoResponseContract>(result);
        }


        public async Task<ProdutoResponseContract> Atualizar(int id, ProdutoRequestContract entidade, int idUsuario)
        {
            await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);
            var produto = _mapper.Map<Produto>(entidade);
            produto.Id = id;

            produto = await _produtoRepository.Atualizar(produto);
            return _mapper.Map<ProdutoResponseContract>(produto);

        }

        public async Task Inativar(int id, int idUsuario)
        {
            var produto = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);
            await _produtoRepository.Deletar(_mapper.Map<Produto>(produto));
        }

        public async Task<ProdutoResponseContract> ObterPorId(int id, int idUsuario)
        {
            var produto = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);
            return _mapper.Map<ProdutoResponseContract>(produto);
        }


        public async Task<IEnumerable<ProdutoResponseContract>> ObterTodos(int idUsuario)
        {
            var produtos = await _produtoRepository.ObeterPeloIdUsuario(idUsuario);
            return produtos.Select(_mapper.Map<ProdutoResponseContract>);
        }

        private async Task<Produto> ObterPorIdVinculadoAoIdUsuario(int id, int usuarioId)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            if (produto is null || produto.ClienteId != usuarioId)
            {
                throw new NotFoundException($"Não foi encontrada nenhum usuário pelo id fornecido {id}");
            }

            return produto;
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
                               ClienteId = p.ClienteId
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
                                ClienteId = p.ClienteId
                            });
        }


        public async Task<int> ObterQuantidadeTotalDeProdutos()
        {
            var produtos = await _produtoRepository.ObterTodos();
            return produtos.Count();
        }

    }
}