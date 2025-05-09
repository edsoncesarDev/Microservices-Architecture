﻿
namespace GeekShopping.Web.Models;

public sealed class CartHeaderModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? CouponCode { get; set; }
    public decimal PurchaseAmount { get; set; }

    public decimal DiscountAmount { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateTime { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? CardNumber { get; set; }
    public string? CVV { get; set; }
    public string? ExpiryMonthYear { get; set; }

    public CartHeaderModel() { }
    
    public CartHeaderModel(int userId, string? couponCode)
    {
        UserId = userId;
        CouponCode = couponCode;
    }
}
