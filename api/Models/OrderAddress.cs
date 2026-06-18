namespace api.Models;
public class OrderAddress
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public AddressType Type { get; set; } = AddressType.Shipping;

    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string BuildingNumber { get; set; } = null!;
    public string PostalCode { get; set; } = null!;

}
