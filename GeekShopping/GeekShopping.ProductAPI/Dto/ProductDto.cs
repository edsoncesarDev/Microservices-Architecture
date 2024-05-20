namespace GeekShopping.ProductAPI.Dto;

public sealed record ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string ImageURL { get; set; } = null!;

}
