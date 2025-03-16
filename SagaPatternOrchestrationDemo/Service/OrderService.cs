using SagaPatternOrchestrationDemo.Data;
using SagaPatternOrchestrationDemo.Model;

namespace SagaPatternOrchestrationDemo.Service
{
    /// <summary>
    /// Sipariş oluşturma : Yeni sipariş saklar
    /// Sipariş iptal : Eğer işlem başarısız olursa, siparişi iptal eder
    /// Güncelleme: In-Memory dictionary yerine veritabanı kullanılıyor.
    /// Güncelleme: Sipariş ekleme ve iptal etme işlemleri EF CORE ile yönetildi
    /// </summary>
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(OrderRequest order);
        Task CancelOrderAsync(Guid orderId);
    }
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CancelOrderAsync(Guid orderId)
        {

            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.IsCanceled = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Guid> CreateOrderAsync(OrderRequest order)
        {
            var newOrder = new Order
            {
                OrderId = order.OrderId,
                ProductId = order.ProductId,
                Amount = order.Amount
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
            return newOrder.OrderId;
        }
    }
}
