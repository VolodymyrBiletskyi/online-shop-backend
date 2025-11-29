using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Order;
using api.Dto;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("{userId:guid}")]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromRoute] Guid userId, [FromBody] CreateOrder newOrder)
        {
            var order = await _orderService.CreateAsync(userId,newOrder);
            return Ok(order);
        }

        [HttpGet("{orderId:guid}")]
        public async Task<ActionResult<OrderDto>> GetById([FromRoute] Guid orderId)
        {
            var order = await _orderService.GetByIdAsync(orderId);
            return Ok(order);
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetUserOrders([FromRoute] Guid userId)
        {
            var orders = await _orderService.GetUserOrdersAsync(userId);
            return Ok(orders);
        }

        [HttpPost("{userId:guid}/cancel/{orderId:guid}")]
        public async Task<ActionResult<OrderDto>> Cancel([FromRoute] Guid userId,[FromRoute] Guid orderId)
        {
            var order = await _orderService.CancelAsync(userId,orderId);
            return Ok(order);
        }

        [HttpGet("{orderId:guid}/order-address")]
        public async Task<ActionResult<OrderAddressDto>> GetOrderAddress([FromRoute] Guid orderId)
        {
            var address = await _orderService.GetOrderAddressAsync(orderId);
            return Ok(address);
        }

    }
}