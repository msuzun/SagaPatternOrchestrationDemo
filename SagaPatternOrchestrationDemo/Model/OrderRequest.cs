namespace SagaPatternOrchestrationDemo.Model
{
    public class OrderRequest
    {
        public Guid OrderId { get; set; } = Guid.NewGuid();
        public Guid ProductId { get; set; }
        public decimal Amount { get; set; }
    }
}
