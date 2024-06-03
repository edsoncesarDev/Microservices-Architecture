
namespace GeekShopping.Web.Models;

public sealed class CartDetailModel
{
    public int Id { get; set; }
    public int CartHeaderId { get; set; }
    public CartHeaderModel? CartHeader { get; set; }
    public int ProductId { get; set; }
    public ProductModel? Product { get; set; }
    public int Count { get; set; }

    public CartDetailModel() { }
    
    public CartDetailModel(int productId, ProductModel product, int count)
    {
        ProductId = productId;
        Product = product;
        Count = count;
    }
}
