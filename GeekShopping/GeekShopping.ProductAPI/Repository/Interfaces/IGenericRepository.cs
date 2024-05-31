using GeekShopping.ProductAPI.Domain.BaseEntity;

namespace GeekShopping.ProductAPI.Repository.Interfaces;

public interface IGenericRepository<T> where T : Base
{
    Task<List<T>> GetAll();
    Task<T> FindById(int id);
    Task<T> Create(T item);
    Task<T> Update(T item);
    Task<bool> Delete(int id);
}
