using Microsoft.EntityFrameworkCore;
using ms_spa.Api.Data.Mapping;
using ms_spa.Api.Domain.Models;

namespace ms_spa.Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Usuarios = Set<Usuario>();
            Produtos = Set<Produto>();
            Clientes = Set<Cliente>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new ClienteMap());
        }
    }
}
