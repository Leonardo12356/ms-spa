namespace ms_spa.Api.Contract.Usuario
{
    public class UsuarioLoginResponseContract
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;
    }
}