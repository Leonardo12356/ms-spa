namespace ms_spa.Api.Contract.Usuario
{
    public class UsuarioRequestContract : UsuarioLoginRequestContract
    {
        public string Perfil { get; set; } = string.Empty;
        public DateTime? DataInativacao { get; set; }
    }
}