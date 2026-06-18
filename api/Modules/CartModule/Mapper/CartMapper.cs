using api.Models;
using api.Modules.CartModule.DTOs.Responses;

namespace api.Modules.CartModule.Mapper
{
    public static class CartMapper
    {
        public static CartDto ToDto(this Cart cart)
        {
            return new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CreatedAt = cart.CreatedAt,
                UpdatedAt = cart.UpdatedAt,
                Items = cart.Items.Select(i => i.ToDto()).ToList()
            };
        }
    }
}
