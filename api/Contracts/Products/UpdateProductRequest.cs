using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Contracts.Products
{
    public class UpdateProductRequest
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string? Slug { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public int SortOrder { get; set; } = 0;
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; } = true;
    }
}