using api.Modules.CartModule.DTOs.Requests;
using api.Modules.CartModule.DTOs.Responses;

namespace api.Modules.CartModule.Domain
{
    public interface ICartService
    {
        Task<CartDto> GetOrCreateByUserAsync(Guid userId);
        Task<CartDto> UpdateItemQuantityAsync(Guid userId, Guid itemId, int quantity);
        Task<CartDto> AddItemAsync(Guid userId, AddItem request);
        Task<CartDto> RemoveItemAsync(Guid userId, Guid itemId);
        Task<CartDto> ClearCartAsync(Guid userId);
    }
}
