using GeekShopping.OrderAPI.Domain.Entities;

namespace GeekShopping.OrderAPI.Repository.Interface;

public interface IOrderRepository
{
    Task<bool> AddOrder(OrderHeader header);
    Task UpdateOrderPaymentStatus(int orderHeaderId, bool status);
}
