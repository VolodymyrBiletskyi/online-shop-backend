using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Refund
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid PaymentId { get; set; }
        public Payment Payment { get; set; } = null!;

        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public RefundStatus RefundStatus { get; set; }

        public string? ProviderRefunId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
    }
    
    public enum RefundStatus{Pending = 0,Succeeded = 1,Failed = 2}
}