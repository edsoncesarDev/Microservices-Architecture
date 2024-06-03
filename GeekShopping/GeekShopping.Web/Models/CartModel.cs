namespace GeekShopping.Web.Models;

public sealed class CartModel
{
    public CartHeaderModel? CartHeader { get; set; }
    public List<CartDetailModel>? CartDetails { get; set; } = new List<CartDetailModel>();

    public CartModel() { }

    public CartModel(CartHeaderModel? cartHeader, CartDetailModel cartDetails)
    {
        AddCartHeader(cartHeader!);
        AddCartDetails(cartDetails);
    }

    public void AddCartHeader(CartHeaderModel cartHeader)
    {
        CartHeader = cartHeader;
    }

    public void AddCartDetails(CartDetailModel cartDetail)
    {
        var details = CartDetails!.Any(x => x.Id == cartDetail.Id);

        if (!details)
        {
            CartDetails!.Add(cartDetail);
        }
    }
}
