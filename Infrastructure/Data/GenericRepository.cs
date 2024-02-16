using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly StoreContext _context;

    public GenericRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task<TOut> GetAsync<TOut>(IQuerySpecification<T, TOut> specification, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<T>().AsQueryable();
        var querySpesified = SpecificationEvaluator.GetQuery<T, TOut>(query, specification);
        return await querySpesified.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<TOut>> ListAsync<TOut>(IQuerySpecification<T, TOut> specification, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<T>().AsQueryable();
        var querySpesified = SpecificationEvaluator.GetQuery<T, TOut>(query, specification);
        return await querySpesified.ToListAsync(cancellationToken);
    }
}