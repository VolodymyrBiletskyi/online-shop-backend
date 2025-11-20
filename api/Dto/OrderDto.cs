using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Migrations;
using api.Models;

namespace api.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid OrderAddressId { get; set; }
        public string OrderNumber { get; set; } = null!;
        public OrderStatus OrderStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal SubtotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ShipAmount { get; set; }
        public string Currency { get; set; } = "USD";

        public DateTime CreatedAt { get; set; } 
        public DateTime? PaidAt { get; set; }
        public DateTime? ShippedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public DateTime? CancelledAt { get; set; }
    }
}