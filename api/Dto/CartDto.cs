using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace api.Dto
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }

        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }

        public List<CartItemDto> Items { get; set; } = new();

        public decimal Total => Items.Sum(i => i.Subtotal);
    }
}