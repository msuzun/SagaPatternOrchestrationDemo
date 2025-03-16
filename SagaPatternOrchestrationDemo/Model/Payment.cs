using System.ComponentModel.DataAnnotations;

namespace SagaPatternOrchestrationDemo.Model
{
    public class Payment
    {
        [Key]
        public Guid PaymentId { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public bool IsRefunded { get; set; } = false;
    }
}
