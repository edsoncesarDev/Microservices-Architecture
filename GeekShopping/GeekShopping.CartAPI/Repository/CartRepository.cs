using AutoMapper;
using GeekShopping.CartAPI.Domain.Entities;
using GeekShopping.CartAPI.Dto;
using GeekShopping.CartAPI.Infrastructure.Context;
using GeekShopping.CartAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> ApplyCoupon(int userId, string couponCode)
    {
        var cartHeader = await _context.CartHeaders
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(x => x.UserId == userId);

        if (cartHeader != null) 
        {
            cartHeader.CouponCode = couponCode;
            _context.CartHeaders.Update(cartHeader);
            return await _context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> RemoveCoupon(int userId)
    {
        var cartHeader = await _context.CartHeaders
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(x => x.UserId == userId);

        if (cartHeader != null)
        {
            cartHeader.CouponCode = string.Empty;
            _context.CartHeaders.Update(cartHeader);
            return await _context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> ClearCart(int userId)
    {
        CartHeader? cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId);

        if(cartHeader is not null)
        {
            _context.CartDetails.RemoveRange(_context.CartDetails.Where(x => x.CartHeaderId == cartHeader.Id));

            _context.CartHeaders.Remove(cartHeader);

            return await _context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<CartDto> FindCartByUserId(int userId)
    {
        Cart cart = new();

        cart.CartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == userId);

        cart.CartDetails = await _context.CartDetails.AsNoTracking()
                                                .Include(x => x.Product)
                                                .Where(x => x.CartHeaderId == cart.CartHeader!.Id)
                                                .ToListAsync();

        return _mapper.Map<CartDto>(cart);
    }

    public async Task<bool> RemoveFromCart(int cartDetailId)
    {
        CartDetail? cart = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(x => x.Id == cartDetailId);

        int total = _context.CartDetails.Where(x => x.CartHeaderId == cart!.CartHeaderId).Count();

        _context.CartDetails.Remove(cart!);

        if (total == 1)
        {
            CartHeader? cartHeaderToRemove = await _context.CartHeaders.FirstOrDefaultAsync(x => x.Id == cart!.CartHeaderId); 

            _context.CartHeaders.Remove(cartHeaderToRemove!);

        }
        
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<CartDto> SaveOrUpdateCart(CartDto model)
    {
        var cart = _mapper.Map<Cart>(model);

        var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == cart.CartDetails!.FirstOrDefault()!.ProductId);

        if(product is null)
        {
            await _context.Products.AddAsync(cart.CartDetails!.FirstOrDefault()!.Product!);
            await _context.SaveChangesAsync();
        }

        var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == cart.CartHeader!.UserId);

        if(cartHeader is null)
        {
            await _context.CartHeaders.AddAsync(cart.CartHeader!);
            await _context.SaveChangesAsync();

            cart.CartDetails!.FirstOrDefault()!.CartHeaderId = cart.CartHeader!.Id;
            cart.CartDetails!.FirstOrDefault()!.Product = null!;

            await _context.CartDetails.AddAsync(cart.CartDetails!.FirstOrDefault()!);
            await _context.SaveChangesAsync();
        }
        else
        {
            var cartDetail = await _context.CartDetails.AsNoTracking()
                                                       .FirstOrDefaultAsync(p => p.ProductId == cart.CartDetails!.FirstOrDefault()!.ProductId &&
                                                       p.CartHeaderId == cartHeader.Id);

            if(cartDetail is null)
            {
                cart.CartDetails!.FirstOrDefault()!.CartHeaderId = cartHeader!.Id;
                cart.CartDetails!.FirstOrDefault()!.Product = null!;
                await _context.CartDetails.AddAsync(cart.CartDetails!.FirstOrDefault()!);
                await _context.SaveChangesAsync();
            }
            else
            {
                cart.CartDetails!.FirstOrDefault()!.Product = null!;
                cart.CartDetails!.FirstOrDefault()!.Count += cartDetail.Count;
                cart.CartDetails!.FirstOrDefault()!.Id = cartDetail.Id;
                cart.CartDetails!.FirstOrDefault()!.CartHeaderId = cartDetail.CartHeaderId;
                _context.CartDetails.Update(cart.CartDetails!.FirstOrDefault()!);
                await _context.SaveChangesAsync();
            }
        }

        return _mapper.Map<CartDto>(cart);
    }
}
