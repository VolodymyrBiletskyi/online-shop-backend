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

        public Task AddItemAsync(OrderItem item)
        {
            return _dbContext.OrderItems.AddAsync(item).AsTask();
        }

        public Task CreateAsync(Order order)
        {
            return _dbContext.Orders.AddAsync(order).AsTask();
        }

        public async Task<Order?> DeleteAsync(Guid orderId)
        {
            var order = await GetByIdAsync(orderId);
            if(order == null) return null;

            _dbContext.Orders.Remove(order);
            await SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetByIdAsync(Guid orderId)
        {
            return await _dbContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(x => x.Id == orderId);
        }

        public async Task<OrderItem?> GetItemByIdAsync(Guid itemId)
        {
            return await _dbContext.OrderItems.FirstOrDefaultAsync(x => x.Id == itemId);
        }

        public async Task<IReadOnlyList<Order>> GetUserOrdersAsync(Guid userId)
        {
            return await _dbContext.Orders
                .Where(x => x.UserId == userId)
                .ToListAsync();
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