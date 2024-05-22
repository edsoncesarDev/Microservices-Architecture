namespace GeekShopping.ProductAPI.Domain.BaseEntity;

public abstract class BaseUser
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}
