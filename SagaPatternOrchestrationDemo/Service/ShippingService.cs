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
        public async Task<bool> ShipOrderAsync(Guid orderId)
        {
            Console.WriteLine($"Order {orderId} shipped");
            return await Task.FromResult(true);
        }
    }
}
