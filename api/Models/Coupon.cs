using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Coupon
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Code { get; set; } = null!;
        public CouponType Type { get; set; } = CouponType.Percent;
        public decimal Value { get; set; }
        public DateTime? StartsAt { get; set; }
        public DateTime? EndsAt { get; set; }
        public int UsageLimit { get; set; } = 0;
        public int UsedCount { get; set; } = 0;
        public bool IsActive { get; set; } = true;

        public ICollection<OrderDiscount> OrderDiscount { get; set; } = new List<OrderDiscount>();

    }
    
    public enum CouponType
    {
         Percent = 0,
        Fixed = 1
    }

}