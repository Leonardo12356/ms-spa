using Microsoft.EntityFrameworkCore;
using ms_spa.Api.Data;
using ms_spa.Api.Domain.Models;
using ms_spa.Api.Domain.Repository.Interfaces;
using ms_spa.Api.Exceptions;

namespace ms_spa.Api.Domain.Repository.Classes
{
    public class UsuarioRepository(AppDbContext context) : IUsuarioRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Usuario> Adicionar(Usuario entidade)
        {
            await _context.Usuarios.AddAsync(entidade);
            await _context.SaveChangesAsync();

            return entidade;
        }

        public async Task<Usuario> Atualizar(Usuario entidade)
        {
            Usuario? entidadeBanco = await _context.Usuarios
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

        public async Task Deletar(Usuario entidade)
        {

            _context.Entry(entidade).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario?> Obter(string email)
        {
            return await _context.Usuarios.AsNoTracking()
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Usuario>> ObterTodos()
        {
            return await _context.Usuarios.AsNoTracking()
                                        .OrderBy(u => u.Id)
                                        .ToListAsync();
        }
        public async Task<Usuario?> ObterPorId(int id)
        {
            return await _context.Usuarios.AsNoTracking()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        }
    }
}