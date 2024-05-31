using GeekShopping.ProductAPI.Domain.BaseEntity;
using GeekShopping.ProductAPI.Infrastructure.Context;
using GeekShopping.ProductAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository;

public sealed class GenericRepository<T> : IGenericRepository<T> where T : Base
{
    private readonly AppDbContext _context;
    private DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<List<T>> GetAll()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T> FindById(int id)
    {
        var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id) ?? null;
        return result!;
    }

    public async Task<T> Create(T item)
    {
        var result = await _dbSet.AddAsync(item);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<T> Update(T item)
    {
        var result = await FindById(item.Id);

        if(result is not null)
        {
            _context.Entry(result).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
            return result;
        }

        return null!;
    }

    public async Task<bool> Delete(int id)
    {
        var result = await FindById(id);

        if(result is not null)
        {
            _dbSet.Remove(result);
            return await _context.SaveChangesAsync() > 0;
        }

        return false;
    }

}
