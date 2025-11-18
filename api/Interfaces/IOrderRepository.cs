using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetActiveOrderAsync(Guid userId);
        Task<IEnumerable<Order>> GetUserOrdersAsync(Guid userId);
        Task<Order?> GetByIdAsync(Guid orderId);
        Task<OrderItem?> GetItemByIdAsync(Guid itemId);
        Task CreateAsync(Order order);
        Task DeleteAsync(Guid orderId);

        Task AddItemAsync(Guid orderId,OrderItem item);
        Task<OrderItem?> RemoveItemAsync(Guid orderId,Guid itemId);

        Task<int> SaveChangesAsync();
    }
}