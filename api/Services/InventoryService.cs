using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Inventory;
using api.Dto;
using api.Interfaces;
using api.Mappers;

namespace api.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepo;
        public InventoryService(IInventoryRepository inventoryRepo)
        {
            _inventoryRepo = inventoryRepo;
        }

        private static void EnsurePositive(int Quantity)
        {
            if (Quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(Quantity), "Quantity must be > 0");
        }

        public async Task<InventoryDto> CommitAsync(Guid variantId, ChangeInventory changeInventory)
        {
            EnsurePositive(changeInventory.Quantity);

            var inventory = await _inventoryRepo.GetByVariantAsync(variantId)
                ?? throw new ArgumentException("Inventory don't found for variant");

            if (inventory.QuantityReserved < changeInventory.Quantity)
                throw new InvalidOperationException($"Not enough reserved to commit. Reserved={inventory.QuantityReserved}, requested={changeInventory.Quantity}");

            inventory.QuantityReserved -= changeInventory.Quantity;
            inventory.QuantityOnHand -= changeInventory.Quantity;

            if (inventory.QuantityOnHand < 0)
                throw new InvalidOperationException("Quantity cannot be negative after commit");

            await _inventoryRepo.SaveChangesAsync();

            return InventoryMapper.ToDto(inventory);
                
        }


        public async Task<InventoryDto> IncreaseOnHandAsync(Guid variantId, ChangeInventory changeInventory)
        {
            EnsurePositive(changeInventory.Quantity);

            var inventory = await _inventoryRepo.GetByVariantAsync(variantId)
                ?? throw new ArgumentException("Inventory don't found for variant");

            checked
            {
                inventory.QuantityOnHand += changeInventory.Quantity;
            }


            await _inventoryRepo.SaveChangesAsync();
            return InventoryMapper.ToDto(inventory);
        }

        public async Task<InventoryDto> ReleaseAsync(Guid variantId, ChangeInventory changeInventory)
        {
            EnsurePositive(changeInventory.Quantity);

            var inventory = await _inventoryRepo.GetByVariantAsync(variantId)
                ?? throw new ArgumentException("Inventory don't found for variant");

            inventory.QuantityReserved = Math.Max(0, inventory.QuantityReserved - changeInventory.Quantity);

            await _inventoryRepo.SaveChangesAsync();
            return InventoryMapper.ToDto(inventory);
        }

        public async Task<InventoryDto> ReserveAsync(Guid variantId, ChangeInventory changeInventory)
        {
            EnsurePositive(changeInventory.Quantity);

            var inventory = await _inventoryRepo.GetByVariantAsync(variantId)
                ?? throw new ArgumentException("Inventory don't found for variant");

            var available = inventory.QuantityOnHand - inventory.QuantityReserved;
            if (available < changeInventory.Quantity)
                throw new InvalidOperationException($"Not enough stock to reserve. Avaiable={available}, requested={changeInventory.Quantity}");

            inventory.QuantityReserved += changeInventory.Quantity;

            await _inventoryRepo.SaveChangesAsync();
            return InventoryMapper.ToDto(inventory);
        }
    }
}