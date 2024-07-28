namespace GeekShopping.Email.Dto;

public sealed record UpdatePaymentResultDto
{
    public int OrderId { get; set; }
    public bool Status { get; set; }
    public string Email { get; set; } = null!;

}
