using Microsoft.AspNetCore.Mvc;

public class SingleEndpoint<T, TDto> : IEndpoint where T : class, IIdEntity
{
    private readonly string _route;

    public SingleEndpoint(string route)
    {
        _route = route;
    }

    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet($"{_route}/{typeof(T).Name.ToLower()}/{{id}}", async (
            [FromServices] IGenericRepository<T> repository,
            [FromServices] IQuerySpecification<T, TDto> specification,
            int id,
            CancellationToken cancellationToken) =>
        {
            specification = QuerySpecificationBuilder<T, TDto>
                .CreateFromSpecification(specification)
                .AddFilter(p => p.Id == id)
                .Build();
            
            TDto dto;

            try {
                dto = await repository
                    .GetAsync(specification, cancellationToken);
            } catch(OperationCanceledException ex) {
                return Results.StatusCode(499);
            } catch(Exception ex) {
            }

            if (dto is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(dto);
        });
    }
}
