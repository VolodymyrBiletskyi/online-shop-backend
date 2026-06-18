namespace api.Modules.CartModule.DTOs.Requests
{
    public class AddItem
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
