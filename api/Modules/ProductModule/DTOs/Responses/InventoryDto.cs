namespace api.Modules.ProductModule.DTOs.Responses
{
    public class InventoryDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int QuantityOnHand { get; set; }
        public int QuantityReserved { get; set; }
        public int Available => QuantityOnHand - QuantityReserved;
    }
}
