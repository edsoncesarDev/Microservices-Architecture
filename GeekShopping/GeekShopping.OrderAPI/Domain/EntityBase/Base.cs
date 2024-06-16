namespace GeekShopping.OrderAPI.Domain.EntityBase;

public abstract class Base
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
}
