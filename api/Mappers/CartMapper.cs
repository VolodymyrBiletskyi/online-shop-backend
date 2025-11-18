using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Migrations;
using api.Models;

namespace api.Mappers
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