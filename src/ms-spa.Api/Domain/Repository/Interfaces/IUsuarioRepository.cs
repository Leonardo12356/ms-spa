using ms_spa.Api.Domain.Models;

namespace ms_spa.Api.Domain.Repository.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario, int>
    {
        Task<Usuario?> Obter(string email);
    }
}