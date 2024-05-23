﻿namespace GeekShopping.Web.Models;

public sealed class UserModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string Token { get; set; } = null!;
    public DateTime Expiration { get; set; }
}
