using api.Models;

namespace api.Modules.CartModule.Repository
{
    public interface ICartRepository
    {
        Task<Cart?> GetActiveCartByUserAsync(Guid userId);
        Task CreateAsync(Cart entity);
        Task<CartItem?> GetByIdAsync(Guid itemId);
        Task AddAsync(CartItem item);
        Task<CartItem?> RemoveItemAsync(Guid itemId);
        Task<Cart> ClearCartAsync(Guid cartId);
        Task<int> SaveChangesAsync();
    }
}
