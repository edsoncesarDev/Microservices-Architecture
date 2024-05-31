using AutoMapper;
using GeekShopping.CartAPI.Dto;
using GeekShopping.CartAPI.Infrastructure.Context;
using GeekShopping.CartAPI.Repository.Interface;

namespace GeekShopping.CartAPI.Repository;

public sealed class CartRepository : ICartRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CartRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<bool> ApplyCoupon(int userId, string couponCode)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ClearCart(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<CartDto> FindCartByUserId(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveCoupon(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveFromCart(int cartDetailId)
    {
        throw new NotImplementedException();
    }

    public Task<CartDto> SaveOrUpdateCart(CartDto cart)
    {
        throw new NotImplementedException();
    }
}
