using GeekShopping.ProductAPI.Domain.BaseEntity;
using Shared.Exceptions;

namespace GeekShopping.ProductAPI.Domain.Entities;

public sealed class Product : Base
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? ImageURL { get; set; }

    public Product() { }

    public Product(string name, decimal price, string description, string category, string imageURL)
    {
        ImplementProduct(name, price, description, category, imageURL);
    }

    public void ImplementProduct(string name, decimal price, string description, string category, string imageURL)
    {
        BusinessException.When(string.IsNullOrWhiteSpace(name), "Invalid name");
        Name = name;

        BusinessException.When(price <= 0, "Invalid price");
        Price = price;

        BusinessException.When((description != null) && (string.IsNullOrWhiteSpace(description)), "Invalid description");
        Description = description;

        BusinessException.When((category != null) && (string.IsNullOrWhiteSpace(category)), "Invalid category");
        Category = category;

        BusinessException.When((imageURL != null) && (string.IsNullOrWhiteSpace(imageURL)), "Invalid imageURL");
        ImageURL = imageURL;

        CreationDate = DateTime.UtcNow;
    }
    
}
