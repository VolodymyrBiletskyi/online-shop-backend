using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Payment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public string Provider { get; set; } = null!;

        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public decimal Amount { get; set; }
        public string? TrasnsactionId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public enum PaymentStatus
        {
            Pending = 0,
            Succeeded = 1,
            Failed = 3,
            Refunded = 4
        }
    }


}