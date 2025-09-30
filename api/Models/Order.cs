using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public string OrderNumber { get; set; } = null!;
        public OrderStatus Status { get; set; } = OrderStatus.Created;

        public int Total_amount { get; set; }
        public int SubtotalAmount { get; set; }
        public int DisscountAmount { get; set; }
        public int TaxAmount { get; set; }
        public int Ship_amount { get; set; }
        public string Currency { get; set; } = "USD";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PaidAt { get; set; }

    }
    public enum OrderStatus
    {
        Created = 0,
        Paid = 1,
        Shipped = 2,
        Delivered = 3,
        Cancelled = 4,
        Refunded = 5
    }
}