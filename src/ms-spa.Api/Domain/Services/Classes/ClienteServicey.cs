using AutoMapper;
using ms_spa.Api.Contract.Cliente;
using ms_spa.Api.Domain.Models;
using ms_spa.Api.Domain.Repository.Interfaces;
using ms_spa.Api.Domain.Services.Interfaces;
using ms_spa.Api.Exceptions;

namespace ms_spa.Api.Domain.Services.Classes
{
    public class ClienteService(IClienteRepository ClienteRepository, IMapper mapper) : IClienteService
    {
        private readonly IClienteRepository _clienteRepository = ClienteRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ClienteResponseContract> Adicionar(ClienteRequestContract entidade, int idUsuario)
        {
            var Cliente = _mapper.Map<Cliente>(entidade);
            Cliente = await _clienteRepository.Adicionar(Cliente);
            return _mapper.Map<ClienteResponseContract>(Cliente);
        }

        public async Task<ClienteResponseContract> Atualizar(int id, ClienteRequestContract entidade, int idUsuario)
        {
            _ = await _clienteRepository.ObterPorId(id) ?? throw new NotFoundException("Cliente não encotrada para atualizar.");
            var Cliente = _mapper.Map<Cliente>(entidade);
            Cliente.Id = id;

            Cliente = await _clienteRepository.Atualizar(Cliente);
            return _mapper.Map<ClienteResponseContract>(Cliente);

        }

        public async Task Inativar(int id, int idUsuario)
        {
            var Cliente = await _clienteRepository.ObterPorId(id) ?? throw new NotFoundException("Cliente não encotrada para deletar.");
            await _clienteRepository.Deletar(_mapper.Map<Cliente>(Cliente));
        }

        public async Task<ClienteResponseContract> ObterPorId(int id, int idUsuario)
        {
            var Cliente = await _clienteRepository.ObterPorId(id);
            return _mapper.Map<ClienteResponseContract>(Cliente);
        }

        public async Task<IEnumerable<ClienteResponseContract>> ObterTodos(int idUsuario)
        {
            var despesas = await _clienteRepository.ObterTodos();
            return despesas.Select(_mapper.Map<ClienteResponseContract>);
        }

    }
}