using System.ComponentModel.DataAnnotations;

namespace GeekShopping.Web.Models;

public sealed class UserRegisterModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Invalid name")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Invalid email")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Invalid password")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Invalid role")]
    public string Role { get; set; } = null!;
}
