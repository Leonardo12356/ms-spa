using System.ComponentModel.DataAnnotations;

namespace ms_spa.Api.Domain.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo de Nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo de Email é obrigatório.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public string Cpf { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Observacao { get; set; } = string.Empty;
        public DateTime? DataInativacao { get; set; }

        public Usuario? Usuario { get; set; }

        public IEnumerable<Produto> Produtos { get; set; } = new List<Produto>();
    }
}