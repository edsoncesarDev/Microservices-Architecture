namespace GeekShopping.CartAPI.Dto;

public sealed class CartDto
{
    public CartHeaderDto? CartHeader { get; set; }
    public List<CartDetailDto>? CartDetails { get; set; }
}
