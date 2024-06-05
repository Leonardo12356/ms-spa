namespace ms_spa.Api.Domain.Repository.Interfaces
{
    public interface IRepository<T, I> where T : class
    {
        Task<IEnumerable<T>> ObterTodos();

        Task<T?> ObterPorId(I id);

        Task<T> Adicionar(T entidade);

        Task<T> Atualizar(T entidade);

        Task Deletar(T entidade);
    }
}