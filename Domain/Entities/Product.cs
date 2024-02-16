
namespace Domain.Entities;

public class Product : IIdEntity, INameEntity, IDeletedDateEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string PictureUrl { get; set; } = null!;
    public int ProductTypeId { get; set; }
    public ProductType ProductType { get; set; } = null!;
    public int ProductBrandId { get; set; }
    public ProductBrand ProductBrand { get; set; } = null!;
    public DateTimeOffset? DeletedDate { get; }
}
