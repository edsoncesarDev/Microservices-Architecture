namespace GeekShopping.IdentityUserAPI.Dto;

public sealed record UserLoginDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
