using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Migrations;

namespace api.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid OrderAddressId { get; set; }
        public OrderAddress OrderAddress { get; set; } = null!;
        public string OrderNumber { get; set; } = null!;
        public OrderStatus Status { get; set; } = OrderStatus.Created;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        public ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<OrderDiscount> OrderDiscount { get; set; } = new List<OrderDiscount>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public decimal TotalAmount { get; set; }
        public decimal SubtotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ShipAmount { get; set; }
        public string Currency { get; set; } = "USD";
        

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PaidAt { get; set; }
        public DateTime? ShippedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public DateTime? CancelledAt { get; set; }

    }
    public enum OrderStatus{Created = 0,Paid = 1,Shipped = 2,Delivered = 3,Cancelled = 4,Refunded = 5}
}