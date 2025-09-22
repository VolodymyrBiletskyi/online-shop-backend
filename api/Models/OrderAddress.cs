using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Migrations
{
    public class OrderAddress
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public OrderAddresType Type { get; set; } = OrderAddresType.Shipping;

        public string Country { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string BuildingNumber { get; set; } = null!;
        public string PostalCode { get; set; } = null!;

    }

    public enum OrderAddresType
    {
        Shipping = 0,
        Billing = 1
    }
}