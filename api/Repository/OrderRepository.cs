using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddItemAsync(Guid orderId, OrderItem item)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Order order)
        {
            return _dbContext.Orders.AddAsync(order).AsTask();
        }

        public Task DeleteAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task<Order?> GetActiveOrderAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Order?> GetByIdAsync(Guid orderId)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
        }

        public async Task<OrderItem?> GetItemByIdAsync(Guid itemId)
        {
            return await _dbContext.OrderItems.FirstOrDefaultAsync(x => x.Id == itemId);
        }

        public Task<IEnumerable<Order>> GetUserOrdersAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderItem?> RemoveItemAsync(Guid orderId, Guid itemId)
        {
            var item = await GetItemByIdAsync(itemId);
            if(item is null) return null;

            _dbContext.OrderItems.Remove(item);
            return item;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}