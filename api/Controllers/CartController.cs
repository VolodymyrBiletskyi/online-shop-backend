using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Cart;
using api.Dto;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId:guid}")]
        public async Task<ActionResult<CartDto>> GetOrCreateCart([FromRoute] Guid userId)
        {
            var cart = await _cartService.GetOrCreateByUserAsync(userId);
            return Ok(cart);
        }

        [HttpPost("{userId:guid}/items")]
        public async Task<ActionResult<CartDto>> AddCartItem([FromRoute] Guid userId, [FromBody] AddItem request)
        {
            var item = await _cartService.AddItemAsync(userId,request);
            return Ok(item);
        }

        [HttpDelete("{userId:guid}/items/{itemId:guid}")]
        public async Task<ActionResult<CartDto>> RemoveCartItem([FromRoute] Guid userId, [FromRoute] Guid itemId)
        {
            var item = await _cartService.RemoveItemAsync(userId,itemId);
            return Ok(item);
        }
        [HttpDelete("{userId:guid}")]
        public async Task<ActionResult<CartDto>> ClearCart([FromRoute] Guid userId)
        {
            var cart = await  _cartService.ClearCartAsync(userId);
            return Ok(cart);
        }
        [HttpPatch("{userId:guid}/items/{itemId:guid}")]
        public async Task<ActionResult<CartDto>> UpdateItemQuantity([FromRoute] Guid userId,[FromRoute] Guid itemId, [FromBody] int quantity)
        {
            var item = await _cartService.UpdateItemQuantityAsync(userId,itemId,quantity);
            return Ok(item);
        }
    }
}