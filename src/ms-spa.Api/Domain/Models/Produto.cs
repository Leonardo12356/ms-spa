using System.ComponentModel.DataAnnotations;

namespace ms_spa.Api.Domain.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo de Nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo de Quantidade Estoque é obrigatório.")]
        public int QuantidadeEstoque { get; set; }

        [Required(ErrorMessage = "O campo de Valor Custo é obrigatório.")]
        public double ValorCusto { get; set; }

        [Required(ErrorMessage = "O campo de Valor Venda é obrigatório.")]
        public double ValorVenda { get; set; }
        public string Observacao { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }

    }
}