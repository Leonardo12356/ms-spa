namespace ms_spa.Api.Contract.Cliente
{
    public class ClienteResponseContract : ClienteRequestContract
    {

        public int Id { get; set; }
        public DateTime? DataInativacao { get; set; }
    }
}