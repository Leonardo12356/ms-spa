using Microsoft.EntityFrameworkCore;
using ms_spa.Api.Data;
using ms_spa.Api.Domain.Models;
using ms_spa.Api.Domain.Repository.Interfaces;
using ms_spa.Api.Exceptions;

namespace ms_spa.Api.Domain.Repository.Classes
{
    public class ClienteRepository(AppDbContext context) : IClienteRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Cliente> Adicionar(Cliente entidade)
        {
            await _context.Clientes.AddAsync(entidade);
            await _context.SaveChangesAsync();

            return entidade;
        }

        public async Task<Cliente> Atualizar(Cliente entidade)
        {
            Cliente? entidadeBanco = await _context.Clientes
            .Where(u => u.Id == entidade.Id)
            .FirstOrDefaultAsync();

            if (entidadeBanco != null)
            {
                _context.Entry(entidadeBanco).CurrentValues.SetValues(entidade);
                _context.Update(entidadeBanco);

                await _context.SaveChangesAsync();
                return entidadeBanco;
            }
            else
            {
                throw new NotFoundException("O Usuário não foi localizado");
            }
        }

        public async Task Deletar(Cliente entidade)
        {

            entidade.DataInativacao = DateTime.Now;
            await Atualizar(entidade);
        }

        public async Task<IEnumerable<Cliente>> ObterTodos()
        {
            return await _context.Clientes.AsNoTracking()
                                        .OrderBy(u => u.Id)
                                        .ToListAsync();
        }
        public async Task<Cliente?> ObterPorId(int id)
        {
            return await _context.Clientes.AsNoTracking()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Cliente>> ObeterPeloIdUsuario(int IdUsuario)
        {
            return await _context.Clientes.AsNoTracking()
            .Where(c => c.UsuarioId == IdUsuario)
            .OrderBy(c => c.Id)
            .ToListAsync();
        }
    }
}