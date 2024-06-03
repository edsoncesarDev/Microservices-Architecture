namespace GeekShopping.CartAPI.Domain.Entities;

public sealed class Cart
{
    public CartHeader? CartHeader { get; set; }
    public List<CartDetail>? CartDetails { get; set; }
}
