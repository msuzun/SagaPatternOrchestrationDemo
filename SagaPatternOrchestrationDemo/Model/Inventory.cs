using System.ComponentModel.DataAnnotations;

namespace SagaPatternOrchestrationDemo.Model
{
    public class Inventory
    {
        [Key]
        public Guid ProductId { get; set; }
        public int StockQuantity { get; set; }
    }
}
