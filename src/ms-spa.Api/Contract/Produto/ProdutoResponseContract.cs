namespace ms_spa.Api.Contract.Produto
{
    public class ProdutoResponseContract : ProdutoRequestContract
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
    }
}