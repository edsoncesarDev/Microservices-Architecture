namespace GeekShopping.IdentityUserAPI.Dto;

public sealed record UserTokenDto
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }

    public UserTokenDto(string token, DateTime expiration)
    {
        Token = token;
        Expiration = expiration;
    }
}
