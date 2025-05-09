﻿using GeekShopping.CartAPI.Domain.Entities;

namespace GeekShopping.CartAPI.Dto;

public sealed record CartDetailDto
{
    public int Id { get; set; }
    public int CartHeaderId { get; set; }
    public CartHeaderDto? CartHeader { get; set; }
    public int ProductId { get; set; }
    public ProductDto? Product { get; set; }
    public int Count { get; set; }
}
