
namespace Domain.Entities;

public class ProductBrand : IIdEntity, INameEntity, IDeletedDateEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTimeOffset? DeletedDate { get; }
}
