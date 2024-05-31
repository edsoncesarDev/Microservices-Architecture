namespace GeekShopping.CartAPI.Domain.EntityBase;

public abstract class Base
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}
