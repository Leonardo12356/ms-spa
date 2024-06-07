namespace ms_spa.Api.Contract.Usuario
{
    public class UsuarioResponseContract : UsuarioRequestContract
    {
        public int Id { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}