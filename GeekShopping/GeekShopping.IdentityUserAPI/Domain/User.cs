using GeekShopping.IdentityUserAPI.Helpers;
using GeekShopping.ProductAPI.Domain.BaseEntity;
using Shared.Exceptions;

namespace GeekShopping.IdentityUserAPI.Domain;

public sealed class User : BaseUser
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!; 
    public string Role { get; set; } = null!;

    public User() { }

    public User(string name, string email, string password, string role)
    {
        ImplementUser(name, email, password, role);
    }

    public void ImplementUser(string name, string email, string password, string role)
    {
        BusinessException.When(string.IsNullOrWhiteSpace(name), "Invalid name");
        Name = name;

        BusinessException.When(EmailValidation.IsEmail(email), "Invalid email");
        Email = email;

        BusinessException.When(string.IsNullOrWhiteSpace(password) || password.Length < 8, "Invalid password");
        Password = password;

        BusinessException.When(string.IsNullOrWhiteSpace(role), "Invalid role");
        Role = role;

        CreationDate = DateTime.SpecifyKind(Convert.ToDateTime(CreationDate), DateTimeKind.Utc);
    }
}
