using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Shipment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public string Carrier { get; set; } = null!;
        public string? TrackingNumber { get; set; } = null!;
        public ShipmentStatus Status { get; set; } = ShipmentStatus.Pending;
        public DateTime? ShippedAt { get; set; }
        public DateTime? DeliveredAt { get; set; } 
        
        public enum ShipmentStatus
        {
            Pending = 0,
            Shipped = 1,
            Delivered = 2,
            Returned = 3
        }
    }


}