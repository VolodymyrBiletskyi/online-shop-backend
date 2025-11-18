using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetActiveCartByUserAsync(Guid userId);
        Task CreateAsync(Cart entity);
        // Task<CartItem?> GetItemAsync(Guid itemId, Guid productId, Guid? variantId); 
        Task<CartItem?> GetByIdAsync(Guid itemId);
        Task AddAsync(CartItem item);
        Task<CartItem> RemoveItemAsync(Guid itemId);
        Task<Cart> ClearCartAsync(Guid cartId);
        Task<int> SaveChangesAsync();

    }
}