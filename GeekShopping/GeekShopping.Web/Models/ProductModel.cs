namespace GeekShopping.Web.Models;

public sealed class ProductModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? ImageURL { get; set; }
}
