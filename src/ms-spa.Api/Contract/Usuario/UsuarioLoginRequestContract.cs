namespace ms_spa.Api.Contract.Usuario
{
    public class UsuarioLoginRequestContract
    {
        public string Email { get; set; } = string.Empty;

        public string Senha { get; set; } = string.Empty;
    }
}