public interface IGenericRepository<T> where T : class
{
    Task<TOut> GetAsync<TOut>(IQuerySpecification<T, TOut> specification, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<TOut>> ListAsync<TOut>(IQuerySpecification<T, TOut> specification, CancellationToken cancellationToken = default);
}