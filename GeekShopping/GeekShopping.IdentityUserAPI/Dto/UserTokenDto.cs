namespace GeekShopping.IdentityUserAPI.Dto;

public sealed record UserTokenDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string Token { get; set; }
    public DateTime Expiration { get; set; }

    public UserTokenDto(int id, string name, string email, string role, string token, DateTime expiration)
    {
        Id = id;
        Name = name;
        Email = email;
        Role = role;
        Token = token;
        Expiration = expiration;
    }
}
