
using Domain.Entities;

public class ProductsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        EndpointBuilder.Create()
            .AddDeleteEndpoint<Product>()
            .AddCreateEndpoint<Product, ProductCreateDto>()
            .AddUpdateEndpoint<Product, ProductUpdateDto>()
            .AddListEndpoint<Product, ProductListDto>()
            .AddSingleEndpoint<Product, ProductDto>()
            .Map(endpoints);
    }
}

public class ProductCreateDto
{
}

public class ProductUpdateDto : IIdEntity
{
    public int Id { get; }
}

public class ProductListDto
{
}