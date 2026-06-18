namespace api.Modules.OrderModule.DTOs.Responses
{
    public class OrderItemDto
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductNameSnapshot { get; set; } = null!;
        public string SkuSnapshot { get; set; } = null!;
        public string? AttributesSnapshot { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalLine { get; set; }
    }
}
