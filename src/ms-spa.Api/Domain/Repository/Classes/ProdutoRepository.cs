using Microsoft.EntityFrameworkCore;
using ms_spa.Api.Data;
using ms_spa.Api.Domain.Models;
using ms_spa.Api.Domain.Repository.Interfaces;
using ms_spa.Api.Exceptions;

namespace ms_spa.Api.Domain.Repository.Classes
{
    public class ProdutoRepository(AppDbContext context) : IProdutoRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Produto> Adicionar(Produto entidade)
        {
            await _context.Produtos.AddAsync(entidade);
            await _context.SaveChangesAsync();

            return entidade;
        }

        public async Task<Produto> Atualizar(Produto entidade)
        {
            Produto? entidadeBanco = await _context.Produtos
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
                throw new NotFoundException("O Produto não foi localizado");
            }
        }

        public async Task Deletar(Produto entidade)
        {
            _context.Remove(entidade);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await _context.Produtos.AsNoTracking()
                                        .OrderBy(u => u.Id)
                                        .ToListAsync();
        }
        public async Task<Produto?> ObterPorId(int id)
        {
            return await _context.Produtos.AsNoTracking()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Produto>> ObeterPeloIdUsuario(int IdUsuario)
        {
            return await _context.Produtos.AsNoTracking()
            .Where(c => c.ClienteId == IdUsuario)
            .OrderBy(p => p.Id)
            .ToListAsync();
        }
    }
}