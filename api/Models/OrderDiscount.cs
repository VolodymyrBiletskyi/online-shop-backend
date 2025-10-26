using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class OrderDiscount
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public Guid? CouponId { get; set; }
        public Coupon? Coupon { get; set; }

        public string? Description { get; set; }
        public decimal Amount { get; set; } 

        public decimal AppliedAmount { get; set; }
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    }
}