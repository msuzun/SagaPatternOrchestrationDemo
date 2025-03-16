using System.ComponentModel.DataAnnotations;

namespace SagaPatternOrchestrationDemo.Model
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Amount { get; set; }
        public bool IsCanceled { get; set; } = false;
    }
}
