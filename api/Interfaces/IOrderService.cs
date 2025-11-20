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
        Task<OrderDto> CreateAsync(CreateOrder createOrder);
        Task<OrderDto> GetByIdAsync(Guid orderId);
        Task<IEnumerable<OrderDto>> GetUserOrdersAsync(Guid userId);
        Task<OrderDto> CancelAsync(Guid userId,Guid orderId);
    }
}