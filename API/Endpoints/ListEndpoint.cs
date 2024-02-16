using Microsoft.AspNetCore.Mvc;

public class ListEndpoint<T, TDto> : IEndpoint where T : class
{
    private readonly string _route;

    public ListEndpoint(string route)
    {
        _route = route;
    }

    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet($"{_route}/{typeof(T).Name.ToLower()}", async (
            [FromServices] IGenericRepository<T> repository,
            [FromServices] IQuerySpecification<T, TDto> specification,
            CancellationToken cancellationToken) =>
        {
            var entities = await repository
                .ListAsync(specification, cancellationToken);

            return Results.Ok(entities);
        });
    }
}
