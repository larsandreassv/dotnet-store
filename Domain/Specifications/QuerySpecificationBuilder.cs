using System.Linq.Expressions;

public class QuerySpecificationBuilder<TIn, TOut> {
    private Expression<Func<TIn, bool>> _filter = null;
    private Expression<Func<TIn, object>> _orderBy = null;
    private Expression<Func<TIn, TOut>> _select = null;

    public static QuerySpecificationBuilder<TIn, TOut> Create() {
        return new QuerySpecificationBuilder<TIn, TOut>();
    }

    public static QuerySpecificationBuilder<TIn, TOut> CreateFromSpecification(IQuerySpecification<TIn, TOut> specification) {
        return new QuerySpecificationBuilder<TIn, TOut>().SetSpecification(specification);
    }

    public QuerySpecificationBuilder<TIn, TOut> AddFilter(Expression<Func<TIn, bool>> filter) {
        if (_filter is not null) {
            var parameter = Expression.Parameter(typeof(TIn));
            var left = _filter.Body;
            var right = filter.Body;
            var combined = Expression.Lambda<Func<TIn, bool>>(Expression.AndAlso(left, right), parameter);
            _filter = combined;
        } else {
            _filter = filter;
        }
        return this;
    }

    public QuerySpecificationBuilder<TIn, TOut> SetFilter(Expression<Func<TIn, bool>> filter) {
        _filter = filter;
        return this;
    }

    public QuerySpecificationBuilder<TIn, TOut> SetOrderBy(Expression<Func<TIn, object>> orderBy) {
        _orderBy = orderBy;
        return this;
    }

    public QuerySpecificationBuilder<TIn, TOut> AddOrderBy(Expression<Func<TIn, object>> orderBy) {
        throw new NotImplementedException();
    }

    public QuerySpecificationBuilder<TIn, TOut> SetSelect(Expression<Func<TIn, TOut>> select) {
        _select = select;
        return this;
    }

    public QuerySpecificationBuilder<TIn, TOut> AddSpecification(IQuerySpecification<TIn, TOut> specification) {
        if (specification.Filter is not null) {
            this.AddFilter(specification.Filter);
        }
        if (specification.OrderBy is not null) {
            this.AddOrderBy(specification.OrderBy);
        }
        return this;
    }

    public QuerySpecificationBuilder<TIn, TOut> SetSpecification(IQuerySpecification<TIn, TOut> specification) {
        this.SetSelect(specification.Select);
        this.SetFilter(specification.Filter);
        this.SetOrderBy(specification.OrderBy);
        return this;
    }

    public IQuerySpecification<TIn, TOut> Build() {
        return new GenericQuerySpecification<TIn, TOut> {
            Filter = _filter,
            OrderBy = _orderBy,
            Select = _select
        };
    }
}