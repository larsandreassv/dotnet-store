using System.Linq.Expressions;

public class GenericQuerySpecification<TIn, TOut> : IQuerySpecification<TIn, TOut>
{
    public Expression<Func<TIn, bool>> Filter { get; set; }
    public Expression<Func<TIn, object>> OrderBy { get; set; }
    public Expression<Func<TIn, TOut>> Select { get; set; }
}