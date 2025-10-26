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
        private readonly IInventoryService _inentoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inentoryService = inventoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<InventoryDto>>> GetAll()
        {
            var inventory = await _inentoryService.GetAllAsync();
            return Ok(inventory);
        }

        [HttpPost("increase")]
        public Task<InventoryDto> Increase(Guid variantId, [FromBody] ChangeInventory dto)
        {
            return _inentoryService.IncreaseOnHandAsync(variantId, dto);
        }

        [HttpPost("reserve")]
        public Task<InventoryDto> Reserve(Guid variantId, [FromBody] ChangeInventory dto)
        {
            return _inentoryService.ReserveAsync(variantId, dto);
        }

        [HttpPost("release")]
        public Task<InventoryDto> Release(Guid variantId, [FromBody] ChangeInventory dto)
        {
            return _inentoryService.ReleaseAsync(variantId, dto);
        }

        [HttpPost("commit")]
        public Task<InventoryDto> Commit(Guid variantId, [FromBody] ChangeInventory dto)
        {
            return _inentoryService.CommitAsync(variantId, dto);
        }
    }
}