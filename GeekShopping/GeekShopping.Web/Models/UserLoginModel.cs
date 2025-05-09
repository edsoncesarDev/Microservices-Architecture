﻿using System.ComponentModel.DataAnnotations;

namespace GeekShopping.Web.Models;

public class UserLoginModel
{
    [Required(ErrorMessage = "Invalid email")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Invalid password")]
    public string Password { get; set; } = null!;
}
