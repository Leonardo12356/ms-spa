namespace ms_spa.Api.Contract.Usuario
{
    public class UsuarioResponseContract
    {
        public int Id { get; set; }

        public DateTime DataCadastro { get; set; }
        public string Perfil { get; set; } = string.Empty;
    }
}