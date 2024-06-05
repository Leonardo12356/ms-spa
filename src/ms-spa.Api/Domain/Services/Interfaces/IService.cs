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
        Task<IEnumerable<RS>> ObterTodos(I idUsuario);
        Task<RS> ObterPorId(I id, I idUsuario);

        Task<RS> Adicionar(RQ entidade, I idUsuario);

        Task<RS> Atualizar(I id, RQ entidade, I idUsuario);

        Task Inativar(I id, I idUsuario);

    }
}