using SagaPatternOrchestrationDemo.Data;
using SagaPatternOrchestrationDemo.Model;

namespace SagaPatternOrchestrationDemo.Service
{
    /// <summary>
    /// Kargo gönderimi : Sipariş başarıyla tamamlanırsa, kargo gönderilir.
    /// </summary>
    public interface IShippingService
    {
        Task<bool> ShipOrderAsync(Guid orderId);
    }
    public class ShippingService : IShippingService
    {
        private readonly ApplicationDbContext _context;

        public ShippingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ShipOrderAsync(Guid orderId)
        {
            var shipment = new Shipment
            {
                ShipmentId = Guid.NewGuid(),
                OrderId = orderId,
                IsShipped = true
            };

            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
