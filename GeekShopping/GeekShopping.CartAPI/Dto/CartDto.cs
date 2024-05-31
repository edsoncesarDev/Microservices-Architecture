using GeekShopping.CartAPI.Domain.Entities;

namespace GeekShopping.CartAPI.Dto;

public sealed record CartDto
{
    public CartHeaderDto? CartHeader { get; set; }
    public IEnumerable<CartDetailDto>? CartDetails { get; set; }
}
