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
        Task<InventoryDto> IncreaseOnHandAsync(Guid productId, ChangeInventory changeInventory);
        Task<InventoryDto> ReserveAsync(Guid productId, ChangeInventory changeInventory);
        Task<InventoryDto> ReleaseAsync(Guid productId, ChangeInventory changeInventory);
        Task<InventoryDto> CommitAsync(Guid productId, ChangeInventory changeInventory);
    }
}