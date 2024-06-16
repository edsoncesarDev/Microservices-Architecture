using GeekShopping.OrderAPI.Domain.EntityBase;

namespace GeekShopping.OrderAPI.Domain.Entities;

public sealed class OrderDetail : Base
{
    public int OrderHeaderId { get; set; }
    public OrderHeader? OrderHeader { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public string? ProductName { get; set; }
    public decimal Price { get; set; }

    public OrderDetail() { }
    
}
