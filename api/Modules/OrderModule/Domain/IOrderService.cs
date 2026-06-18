using api.Modules.OrderModule.DTOs.Requests;
using api.Modules.OrderModule.DTOs.Responses;

namespace api.Modules.OrderModule.Domain
{
    public interface IOrderService
    {
        Task<OrderDto> CreateAsync(Guid userId, CreateOrder createOrder);
        Task<OrderDto> GetByIdAsync(Guid orderId);
        Task<IReadOnlyList<OrderDto>> GetUserOrdersAsync(Guid userId);
        Task<OrderDto> CancelAsync(Guid userId, Guid orderId);
        Task<OrderAddressDto> GetOrderAddressAsync(Guid orderId);
    }
}
