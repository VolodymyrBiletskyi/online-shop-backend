using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Inventory;
using api.Dto;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/inventory")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<InventoryDto>>> GetAll()
        {
            var inventory = await _inventoryService.GetAllAsync();
            return Ok(inventory);
        }

        [HttpPost("increase")]
        public Task<InventoryDto> Increase(Guid productId, [FromBody] ChangeInventory dto)
        {
            return _inventoryService.IncreaseOnHandAsync(productId, dto);
        }

        [HttpPost("reserve")]
        public Task<InventoryDto> Reserve(Guid productId, [FromBody] ChangeInventory dto)
        {
            return _inventoryService.ReserveAsync(productId, dto);
        }

        [HttpPost("release")]
        public Task<InventoryDto> Release(Guid productId, [FromBody] ChangeInventory dto)
        {
            return _inventoryService.ReleaseAsync(productId, dto);
        }

        [HttpPost("commit")]
        public Task<InventoryDto> Commit(Guid productId, [FromBody] ChangeInventory dto)
        {
            return _inventoryService.CommitAsync(productId, dto);
        }
    }
}