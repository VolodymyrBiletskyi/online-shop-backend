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
        public UserModel User { get; set; } = null!;
        public string OrderNumber { get; set; } = null!;
        public OrderStatus Status { get; set; } = OrderStatus.created;

        public int Total_amount { get; set; }
        public int Subtotal_amount { get; set; }
        public int Disscount_amount { get; set; }
        public int Tax_amount { get; set; }
        public int Ship_amount { get; set; }
        public string Currency { get; set; } = "USD";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PaidAt { get; set; } 

        public enum OrderStatus
        {
            created = 0,
            paid = 1,
            shipped = 2,
            delivered = 3,
            cancelled = 4,
            refunded = 5
        }
    }
}