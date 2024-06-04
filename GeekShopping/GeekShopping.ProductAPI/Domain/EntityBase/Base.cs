namespace GeekShopping.ProductAPI.Domain.BaseEntity;

public abstract class Base
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
}
