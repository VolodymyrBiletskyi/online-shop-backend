using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Order;
using api.Dto;
using api.Interfaces;

namespace api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;

        public OrderService(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public Task<OrderDto> CancelAsync(Guid userId, Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto> CreateAsync(CreateOrder createOrder)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto> GetByIdAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderDto>> GetUserOrdersAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}