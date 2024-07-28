using GeekShopping.Email.Domain.Entities;
using GeekShopping.Email.Dto;
using GeekShopping.Email.Infrastructure.Context;
using GeekShopping.Email.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Email.Repository;

public sealed class EmailRepository : IEmailRepository
{
    private readonly DbContextOptions<AppDbContext> _context;

    public EmailRepository(DbContextOptions<AppDbContext> context)
    {
        _context = context;
    }

    public async Task LogEmail(UpdatePaymentResultDto message)
    {
        var email = new Message()
        {
            EmailAddress = message.Email,
            Log = $"Order - {message.OrderId} has been created successfully!"
        };

        await using var _db = new AppDbContext(_context);
        _db.Emails.Add(email);
        await _db.SaveChangesAsync();
    }
}
