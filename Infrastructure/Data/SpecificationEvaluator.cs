public class SpecificationEvaluator
{
    public static IQueryable<TOut> GetQuery<TIn, TOut>(IQueryable<TIn> inputQuery, IQuerySpecification<TIn, TOut> spec) 
    {
        var query = inputQuery;

        // modify the IQueryable using the specification's criteria expression
        if (spec.Filter != null)
        {
            query = query.Where(spec.Filter);
        }

        // Apply any ordering expressions if they exist
        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        // Apply any select expressions if they exist
        if (spec.Select != null)
        {
            return query.Select(spec.Select);
        }

        throw new ("No select expression found");
    }
}