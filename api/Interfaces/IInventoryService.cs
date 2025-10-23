using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Inventory;
using api.Dto;
using api.Models;

namespace api.Interfaces
{
    public interface IInventoryService
    {
        Task<IReadOnlyList<InventoryDto>> GetAllAsync();
        Task<InventoryDto> IncreaseOnHandAsync(Guid variantId, ChangeInventory changeInventory);
        Task<InventoryDto> ReserveAsync(Guid variantId, ChangeInventory changeInventory);
        Task<InventoryDto> ReleaseAsync(Guid variantId, ChangeInventory changeInventory);
        Task<InventoryDto> CommitAsync(Guid variantId, ChangeInventory changeInventory);
    }
}