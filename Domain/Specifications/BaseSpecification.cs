using System.Linq.Expressions;

public record BaseSpecification<TIn, TOut> : IQuerySpecification<TIn, TOut>
{
    public Expression<Func<TIn, bool>> Filter { get; }
    public Expression<Func<TIn, object>> OrderBy { get => _orderBy; }
    public Expression<Func<TIn, TOut>> Select { get; }

    private readonly Expression<Func<TIn, bool>> _filter = null;
    private readonly Expression<Func<TIn, object>> _orderBy = null;
    private readonly Expression<Func<TIn, TOut>> _select = null;

    public BaseSpecification() {
        if (typeof(TIn).IsAssignableFrom(typeof(IDeletedDateEntity))) {
            _filter = (Expression<Func<TIn, bool>>)(x => !(x as IDeletedDateEntity).DeletedDate.HasValue);
        }
        if (typeof(TIn).IsAssignableFrom(typeof(INameEntity))) {
            _orderBy = (Expression<Func<TIn, object>>)(x => (x as INameEntity).Name);
        }
    }
}
