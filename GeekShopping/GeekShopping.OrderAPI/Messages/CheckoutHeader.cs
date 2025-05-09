﻿using GeekShopping.MessageBus.Models;
using GeekShopping.OrderAPI.Dto;

namespace GeekShopping.OrderAPI.Messages;

public sealed class CheckoutHeader : BaseMessage
{
    public int UserId { get; set; }
    public string? CouponCode { get; set; }
    public decimal PurchaseAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? CardNumber { get; set; }
    public string? CVV { get; set; }
    public string? ExpiryMonthYear { get; set; }

    public int CartTotalItens { get; set; }
    public List<CartDetailDto> CartDetails { get; set; } = new List<CartDetailDto>();

    public CheckoutHeader() { }

}
