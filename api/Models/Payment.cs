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
        public string? ProviderId { get; set; }

        public PaymantMethod Method { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public string? TrasnsactionId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? SucceededAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public string? FailureReason { get; set; }

        public ICollection<Refund> Refund { get; set; } = new List<Refund>();


    }
    public enum PaymentStatus { Pending = 0, Succeeded = 1, Failed = 3, Refunded = 4 }
    public enum PaymantMethod {Carr = 0,CashOnDelivery = 1}




}