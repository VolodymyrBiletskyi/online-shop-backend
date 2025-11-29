using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Order;
using api.Dto;

namespace api.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateAsync(Guid userId,CreateOrder createOrder);
        Task<OrderDto> GetByIdAsync(Guid orderId);
        Task<IReadOnlyList<OrderDto>> GetUserOrdersAsync(Guid userId);
        Task<OrderDto> CancelAsync(Guid userId,Guid orderId);
        Task<OrderAddressDto> GetOrderAddressAsync(Guid orderId);
    }
}