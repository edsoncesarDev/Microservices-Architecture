using GeekShopping.CartAPI.Domain.EntityBase;

namespace GeekShopping.CartAPI.Domain.Entities;

public sealed class CartDetail : Base
{
    public int CartHeaderId { get; set; }
    public CartHeader? CartHeader { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int Count { get; set; }
}
