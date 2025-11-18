using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Cart;
using api.Dto;

namespace api.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetOrCreateByUserAsync(Guid userId);
        Task<CartDto> UpdateItemQuantityAsync(Guid itemId,Guid userId, int quantity);
        Task<CartDto> AddItemAsync(Guid userId, AddItem request);
        Task<CartDto> RemoveItemAsync(Guid itemId,Guid userId);
        Task<CartDto> ClearCartAsync(Guid cartId);
    }
}