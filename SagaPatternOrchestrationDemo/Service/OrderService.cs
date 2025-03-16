using SagaPatternOrchestrationDemo.Model;

namespace SagaPatternOrchestrationDemo.Service
{
    /// <summary>
    /// Sipariş oluşturma : Yeni sipariş saklar
    /// Sipariş iptal : Eğer işlem başarısız olursa, siparişi iptal eder
    /// </summary>
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(OrderRequest order);
        Task CancelOrderAsync(Guid orderId);
    }
    public class OrderService : IOrderService
    {
        private readonly Dictionary<Guid, OrderRequest> _orders = new();
        public async Task CancelOrderAsync(Guid orderId)
        {
            if (_orders.ContainsKey(orderId))
            {
                _orders.Remove(orderId);
                Console.WriteLine($"Order {orderId} cancelled");
                await Task.CompletedTask;
            }
        }

        public async Task<Guid> CreateOrderAsync(OrderRequest order)
        {
            var orderId = order.OrderId;
            _orders[orderId] = order;
            Console.WriteLine($"Order {orderId} created");
            return await Task.FromResult(orderId);
        }
    }
}
