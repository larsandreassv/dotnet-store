using System.Linq.Expressions;

public interface IQuerySpecification<TIn, TOut>
{
    public Expression<Func<TIn, bool>> Filter { get; }
    public Expression<Func<TIn, object>> OrderBy { get; }
    public Expression<Func<TIn, TOut>> Select { get; }
}