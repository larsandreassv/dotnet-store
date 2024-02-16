using Infrastructure.Data;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{

    private readonly IGenericRepository<Product> _productRepository;

    public ProductsController(IGenericRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Product>>> GetProductsAsync(CancellationToken cancellationToken)
    {
        var specification = new QuerySpecificationBuilder<Product, Product>()
            .AddOrderBy(p => p.Name)
            .SetSelect(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
            })
            .Build();

        var products = await _productRepository
            .ListAsync<Product>(specification, cancellationToken);

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductAsync(int id, CancellationToken cancellationToken)
    {
        var specification = new QuerySpecificationBuilder<Product, Product>()
            .AddFilter(p => p.Id == id)
            .SetSelect(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
            })
            .Build();

        var product = await _productRepository
            .GetAsync<Product>(specification, cancellationToken);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }
}