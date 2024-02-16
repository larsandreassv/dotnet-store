
namespace Domain.Entities;

public class ProductType : IIdEntity, INameEntity, IDeletedDateEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTimeOffset? DeletedDate { get; }
}