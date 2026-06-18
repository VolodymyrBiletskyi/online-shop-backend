using System.ComponentModel;

namespace api.Modules.ProductModule.DTOs.Requests
{
    public class UpdateProductRequest
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = null!;
        [DefaultValue("")]
        public string? Slug { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public int SortOrder { get; set; } = 0;
        public decimal BasePrice { get; set; }
        public bool IsActive { get; set; } = true;
        public int Available { get; set; }
    }
}
