using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Cart;
using api.Dto;
using api.Interfaces;
using api.Mappers;
using api.Models;

namespace api.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepo;
        private readonly IProductRepository _productRepo;
        public CartService(ICartRepository cartRepo,IProductRepository productRepo)
        {
            _cartRepo = cartRepo;
            _productRepo = productRepo;
        }

        private async Task<Cart> GetOrCreateCartAsync(Guid userId)
        {
            var cart = await _cartRepo.GetActiveCartByUserAsync(userId);

            if(cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Items = new List<CartItem>()
                };  
                await _cartRepo.CreateAsync(cart);
                await _cartRepo.SaveChangesAsync();

                
            }
            return cart;
        }

        public async Task<CartDto> AddItemAsync(Guid userId, AddItem request)
        {
            var cart = await GetOrCreateCartAsync(userId);

            var existingItem = cart.Items.FirstOrDefault(i =>
                i.ProductId == request.ProductId &&
                i.VariantId == request.VariantId);

            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
            }
            else
            {
                var price = await _productRepo.GetPriceSnapshotAsync(
                    request.ProductId,
                    request.VariantId
                );

                var newItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = request.ProductId,
                    VariantId = request.VariantId,
                    Quantity = request.Quantity,
                    UnitPriceSnapshot = price
                };

                await _cartRepo.AddAsync(newItem);
            }
            
            await _cartRepo.SaveChangesAsync();

            var updatedCart = await _cartRepo.GetActiveCartByUserAsync(userId);

            return updatedCart!.ToDto();
        }

        public async Task<CartDto> ClearCartAsync(Guid userId)
        {
            var cart = await _cartRepo.GetActiveCartByUserAsync(userId)
                ?? throw new InvalidOperationException("Cart does not exist");

            var clear = await _cartRepo.ClearCartAsync(cart.Id);
            await _cartRepo.SaveChangesAsync();
            return clear.ToDto();
        }

        public async Task<CartDto> GetOrCreateByUserAsync(Guid userId)
        {
            var cart = await GetOrCreateCartAsync(userId);
            return cart.ToDto();
        }

        public async Task<CartDto> RemoveItemAsync(Guid userId, Guid itemId )
        {
            var cart = await GetOrCreateCartAsync(userId);

            var item = cart.Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                throw new InvalidOperationException("item not found in cart");

            await _cartRepo.RemoveItemAsync(itemId);

            await _cartRepo.SaveChangesAsync();
            
            return cart.ToDto();
        }

        public async Task<CartDto> UpdateItemQuantityAsync(Guid userId, Guid itemId, int quantity)
        {
            var cart = await GetOrCreateCartAsync(userId);

            var existingItem = cart.Items.FirstOrDefault(i => i.Id == itemId)
                ??throw new InvalidOperationException("item not found");

            if (quantity <= 0)
            {
                await _cartRepo.RemoveItemAsync(itemId);
            }
            else
            {
                existingItem.Quantity = quantity;
            }

            cart.UpdatedAt = DateTime.UtcNow;

            await _cartRepo.SaveChangesAsync();

            return cart.ToDto();
        }
    }
}