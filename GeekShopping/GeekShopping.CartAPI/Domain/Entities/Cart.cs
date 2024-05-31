namespace GeekShopping.CartAPI.Domain.Entities;

public sealed class Cart
{
    public CartHeader? CartHeader { get; set; }
    public IEnumerable<CartDetail>? CartDetails { get; set; }
}
