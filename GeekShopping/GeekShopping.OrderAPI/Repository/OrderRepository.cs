using GeekShopping.OrderAPI.Domain.Entities;
using GeekShopping.OrderAPI.Infrastructure.Context;
using GeekShopping.OrderAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderAPI.Repository;

public sealed class OrderRepository : IOrderRepository
{
    private readonly DbContextOptions<AppDbContext> _context;

    public OrderRepository(DbContextOptions<AppDbContext> context)
    {
        _context = context;
    }

    public async Task<bool> AddOrder(OrderHeader header)
    {
        if(header is null)
        {
            return false;
        }
        await using var _db = new AppDbContext(_context);
        await _db.OrderHeaders.AddAsync(header);
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task UpdateOrderPaymentStatus(int orderHeaderId, bool status)
    {
        await using var _db = new AppDbContext(_context);
        var header = await _db.OrderHeaders.FirstOrDefaultAsync(x => x.Id == orderHeaderId);
        if (header is not null) 
        {
            header.PaymentStatus = status;
        }

        _db.OrderHeaders.Update(header!);
        await _db.SaveChangesAsync();
    }
}
