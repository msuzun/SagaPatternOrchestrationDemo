using System.ComponentModel.DataAnnotations;

namespace SagaPatternOrchestrationDemo.Model
{
    public class Shipment
    {
        [Key]
        public Guid ShipmentId { get; set; }
        public Guid OrderId { get; set; }
        public bool IsShipped { get; set; } = false;
    }
}
