using SagaPatternOrchestrationDemo.Model;
using SagaPatternOrchestrationDemo.Service;

namespace SagaPatternOrchestrationDemo.App
{
    /// <summary>
    /// Sipariş işlemlerini yöneten sınıf
    /// Başarısız olursa geri alım işlemlerini yönetir.
    /// </summary>
    public class OrderSagaOrchestrator
    {
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private readonly IInventoryService _inventoryService;
        private readonly IShippingService _shippingService;

        public OrderSagaOrchestrator(IOrderService orderService, IPaymentService paymentService, IInventoryService inventoryService, IShippingService shippingService)
        {
            _orderService = orderService;
            _paymentService = paymentService;
            _inventoryService = inventoryService;
            _shippingService = shippingService;
        }

        public async Task<bool> ProcessOrderAsync(OrderRequest order)
        {
            try
            {
                var orderId = await _orderService.CreateOrderAsync(order);
                if (orderId == Guid.Empty)
                {
                    throw new Exception("Order creation failed");
                }
                var paymentResult = await _paymentService.ProccessPaymentService(orderId);
                if (!paymentResult)
                {
                    throw new Exception("Payment failed");
                }
                var stockAvailable = await _inventoryService.CheckInventoryAsync(orderId);
                if (!stockAvailable)
                {
                    throw new Exception("Out of stock");
                }
                var shippingResult = await _shippingService.ShipOrderAsync(orderId);
                if (!shippingResult)
                {
                    throw new Exception("Shipping failed");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                await Roolback(order);
                return false;
            }
        }
        private async Task Roolback(OrderRequest order)
        {
            await _paymentService.RefundPaymentAsync(order.OrderId);
            await _orderService.CancelOrderAsync(order.OrderId);
        }
    }
}
