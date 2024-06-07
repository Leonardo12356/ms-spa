using ms_spa.Api.Contract.Usuario;

namespace ms_spa.Api.Domain.Services.Interfaces
{
    public interface IUsuarioService : IService<UsuarioRequestContract, UsuarioResponseContract, int>
    {
        Task<UsuarioLoginResponseContract> Autenticar(UsuarioLoginRequestContract usuarioLoginRequest);

        Task<UsuarioResponseContract> ObterEmail(string email);
    }
}