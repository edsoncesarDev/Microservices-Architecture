using GeekShopping.Email.Domain.EntityBase;

namespace GeekShopping.Email.Domain.Entities;

public sealed class Message : Base
{
    public string EmailAddress { get; set; } = null!;
    public string Log { get; set; } = null!;

}
