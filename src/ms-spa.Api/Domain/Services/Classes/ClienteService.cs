using AutoMapper;
using ms_spa.Api.Contract.Cliente;
using ms_spa.Api.Domain.Models;
using ms_spa.Api.Domain.Repository.Interfaces;
using ms_spa.Api.Domain.Services.Interfaces;
using ms_spa.Api.Exceptions;

namespace ms_spa.Api.Domain.Services.Classes
{
    public class ClienteService(IClienteRepository clienteRepository, IMapper mapper) : IClienteService
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ClienteResponseContract> Adicionar(ClienteRequestContract entidade)
        {
            var cliente = _mapper.Map<Cliente>(entidade);

            cliente = await _clienteRepository.Adicionar(cliente);
            return _mapper.Map<ClienteResponseContract>(cliente);
        }

        public async Task<ClienteResponseContract> Atualizar(int id, ClienteRequestContract entidade)
        {
            _ = await ObterPorId(id) ?? throw new NotFoundException("Usuário não encontrado para atualização.");
            var cliente = _mapper.Map<Cliente>(entidade);
            cliente.Id = id;

            cliente = await _clienteRepository.Atualizar(cliente);
            return _mapper.Map<ClienteResponseContract>(cliente);

        }

        public async Task Inativar(int id)
        {
            var cliente = await _clienteRepository.ObterPorId(id);
            await _clienteRepository.Deletar(_mapper.Map<Cliente>(cliente));
        }

        public async Task<IEnumerable<ClienteResponseContract>> ObterTodos()
        {
            var clientes = await _clienteRepository.ObterTodos();
            return clientes.Select(_mapper.Map<ClienteResponseContract>);
        }

        public async Task<ClienteResponseContract> ObterPorId(int id)
        {
            Cliente? cliente = await _clienteRepository.ObterPorId(id);
            return _mapper.Map<ClienteResponseContract>(cliente);
        }

        public async Task<int> ObterQuantidadeTotalDeClientes()
        {
            var clientes = await _clienteRepository.ObterTodos();
            return clientes.Count();
        }



    }
}