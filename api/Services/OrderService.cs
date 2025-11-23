using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Order;
using api.Dto;
using api.Interfaces;
using api.Mappers;
using api.Models;

namespace api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly ICartRepository _cartRepo;

        public OrderService(IOrderRepository orderRepo,ICartRepository cartRepo)
        {
            _orderRepo = orderRepo;
            _cartRepo = cartRepo;
        }

        public async Task<OrderDto> CancelAsync(Guid userId, Guid orderId)
        {
            var order = await _orderRepo.GetByIdAsync(orderId)
                ?? throw new InvalidOperationException("Order does not exist");

            if (order.UserId != userId)
                throw new UnauthorizedAccessException("You cannot cancel this order");
            if (order.Status == OrderStatus.Delivered)
                throw new InvalidOperationException("Delivere orders cannot be cancelled ");

            if (order.Status == OrderStatus.Cancelled)
                throw new InvalidOperationException("Order is already cancelled");

            order.Status = OrderStatus.Cancelled;
            order.CancelledAt = DateTime.UtcNow;

            await _orderRepo.SaveChangesAsync();

            return order.ToOrderDto();

        }

        public async Task<OrderDto> CreateAsync(Guid userId,CreateOrder createOrder)
        {
            var cart = await _cartRepo.GetActiveCartByUserAsync(userId)
                ?? throw new InvalidOperationException("Cart is empty or does not exist");

            decimal subtotal = cart.Items.Sum(i => i.UnitPriceSnapshot * i.Quantity);
            decimal discount = 0;
            decimal tax = 0;
            decimal ship = 0;
            decimal total = subtotal - discount + tax + ship;

            var ordernumber = $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}-{Random.Shared.Next(1000,9999)}";

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                OrderAddressId = createOrder.OrderAddressId,
                OrderNumber = ordernumber,

                Status = OrderStatus.Created,
                PaymentStatus = PaymentStatus.Pending,

                SubtotalAmount = subtotal,
                DiscountAmount = discount,
                TaxAmount = tax,
                ShipAmount = ship,
                TotalAmount = total,
                Currency = "USD",

                CreatedAt = DateTime.UtcNow,
                OrderItems = new List<OrderItem>()
            };
            
            foreach(var cartitem in cart.Items)
            {
                order.OrderItems.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = cartitem.ProductId,
                    VariantId = cartitem.VariantId,
                    ProductNameSnapshot = cartitem.ProductNameSnapshot,
                    SkuSnapshot = cartitem.SkuSnapshot,
                    AttributesSnapshot = cartitem.AttributesSnapshot,
                    Quantity = cartitem.Quantity,
                    UnitPrice = cartitem.UnitPriceSnapshot
                });
            }

            await _orderRepo.CreateAsync(order);
            await _cartRepo.ClearCartAsync(cart.Id);
            await _orderRepo.SaveChangesAsync();

            return order.ToOrderDto();
        }

        public async Task<OrderDto> GetByIdAsync(Guid orderId)
        {
            var order = await _orderRepo.GetByIdAsync(orderId)
                ?? throw new InvalidOperationException("Order does not exist");

            return order.ToOrderDto();
        }

        public async Task<IReadOnlyList<OrderDto>> GetUserOrdersAsync(Guid userId)
        {
            var orders = await _orderRepo.GetUserOrdersAsync(userId);
            return orders.Select(OrderMapper.ToOrderDto).ToList();
        }
    }
}