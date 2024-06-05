using System.ComponentModel.DataAnnotations;

namespace ms_spa.Api.Domain.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo de E-mail é obrigatório.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo de Senha é obrigatório.")]
        public string Senha { get; set; } = string.Empty;

        public string Perfil { get; set; } = string.Empty;

        [Required]
        public DateTime DataCadastro { get; set; }

        public DateTime? DataInativacao { get; set; }
    }
}
