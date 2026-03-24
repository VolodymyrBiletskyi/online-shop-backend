using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IOrderRepository
    {
        Task<IReadOnlyList<Order>> GetUserOrdersAsync(Guid userId);
        Task<Order?> GetByIdAsync(Guid orderId);
        Task<OrderItem?> GetItemByIdAsync(Guid itemId);
        Task CreateAsync(Order order);
        Task<Order?> DeleteAsync(Guid orderId);
        Task AddItemAsync(OrderItem item);
        Task<OrderItem?> RemoveItemAsync(Guid orderId,Guid itemId);
        Task<int> SaveChangesAsync();

        Task<OrderAddress> GetOrderAddress(Guid userId);
    }
}