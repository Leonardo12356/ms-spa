namespace ms_spa.Api.Domain.Services.Interfaces
{
    public interface IService<RQ, RS, I> where RQ : class
    {
        /// <summary>
        /// Interface generica para criacao do tipo Crud
        /// </summary>
        /// <typeparam name="RQ">Contrato de Request</typeparam>
        /// <typeparam name="RS">Contrato de Response</typeparam>
        /// <typeparam name="I">Tipo do Id</typeparam>
        Task<IEnumerable<RS>> ObterTodos();
        Task<RS> ObterPorId(I id);

        Task<RS> Adicionar(RQ entidade);

        Task<RS> Atualizar(I id, RQ entidade);

        Task Inativar(I id);

    }
}