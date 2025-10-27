using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string Name { get; set; } = null!;
        public string? Slug { get; set; } =string.Empty;
        public string? Description { get; set; }
        public int SortOrder { get; set; }
        public string? ThumbnailUrl { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; } 
        public DateTime CreatedAt { get; set; } 
    }
}