﻿namespace SagaPatternOrchestrationDemo.Model
{
    public class OrderRequest
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Amount { get; set; }
        public int stockQuantity { get; set; }
        
    }
}
