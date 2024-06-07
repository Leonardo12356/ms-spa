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

        public async Task<ProdutoResponseContract> Adicionar(ProdutoRequestContract entidade, int idUsuario)
        {
            var produto = _mapper.Map<Produto>(entidade);
            produto = await _produtoRepository.Adicionar(produto);
            return _mapper.Map<ProdutoResponseContract>(produto);
        }

        public async Task<ProdutoResponseContract> Atualizar(int id, ProdutoRequestContract entidade, int idUsuario)
        {
            _ = await _produtoRepository.ObterPorId(id) ?? throw new NotFoundException("Produto não encotrada para atualizar.");
            var produto = _mapper.Map<Produto>(entidade);
            produto.Id = id;

            produto = await _produtoRepository.Atualizar(produto);
            return _mapper.Map<ProdutoResponseContract>(produto);

        }

        public async Task Inativar(int id, int idUsuario)
        {
            var produto = await _produtoRepository.ObterPorId(id) ?? throw new NotFoundException("Produto não encotrada para deletar.");
            await _produtoRepository.Deletar(_mapper.Map<Produto>(produto));
        }

        public async Task<ProdutoResponseContract> ObterPorId(int id, int idUsuario)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            return _mapper.Map<ProdutoResponseContract>(produto);
        }

        public async Task<IEnumerable<ProdutoResponseContract>> ObterTodos(int idUsuario)
        {
            var despesas = await _produtoRepository.ObterTodos();
            return despesas.Select(_mapper.Map<ProdutoResponseContract>);
        }

    }
}