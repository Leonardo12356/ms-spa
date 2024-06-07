namespace ms_spa.Api.Contract.Produto
{
    public class ProdutoRequestContract
    {
        public string Nome { get; set; } = string.Empty;
        public int QuantidadeEstoque { get; set; }
        public double ValorCusto { get; set; }
        public double ValorVenda { get; set; }
        public string Observacao { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public int ClienteId { get; set; }
    }
}