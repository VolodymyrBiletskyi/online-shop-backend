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
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _dbContext;
        public CartRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task AddAsync(CartItem item)
        {
            return _dbContext.CartItems.AddAsync(item).AsTask();
        }

        public async Task<Cart> ClearCartAsync(Guid cartId)
        {
            var cart = await _dbContext.Cart
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart is null)
                throw new InvalidOperationException("Cart not found");

            _dbContext.CartItems.RemoveRange(cart.Items);
            cart.Items.Clear();

            return cart;
        }

        public Task CreateAsync(Cart entity)
        {
            return _dbContext.Cart.AddAsync(entity).AsTask();
        }

        public Task<Cart?> GetActiveCartByUserAsync(Guid userId)
        {
            return _dbContext.Cart
                .Include(i => i.Items)
                    .ThenInclude(i => i.Product)
                .Include(i => i.Items)
                    .ThenInclude(i => i.ProductVariant)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<CartItem?> GetByIdAsync(Guid itemId)
        {
           return await _dbContext.CartItems.FirstOrDefaultAsync(x => x.Id == itemId);
        }

        public async Task<CartItem?> RemoveItemAsync(Guid itemId)
        {
            var item = await GetByIdAsync(itemId);

            if (item is null) return null; 
               
            _dbContext.CartItems.Remove(item);
            return item;
            
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}