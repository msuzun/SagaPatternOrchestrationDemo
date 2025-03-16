namespace SagaPatternOrchestrationDemo.Service
{
    /// <summary>
    /// Ödeme Alma : Sipariş için ödeme alınır.
    /// Ödeme İadesi : Eğer sipariş iptal edilirse, ödeme geri alınır.
    /// </summary>
    public interface IPaymentService
    {
        Task<bool> ProccessPaymentService(Guid orderId);
        Task RefundPaymentAsync(Guid orderId);
    }
    public class PaymentService : IPaymentService
    {
        private readonly Dictionary<Guid, bool> _payments = new();
        public async Task<bool> ProccessPaymentService(Guid orderId)
        {
            _payments[orderId] = true;
            Console.WriteLine($"Payment for order {orderId} processed");
            return await Task.FromResult(true);
        }

        public async Task RefundPaymentAsync(Guid orderId)
        {
            if (_payments.ContainsKey(orderId))
            {
                _payments.Remove(orderId);
                Console.WriteLine($"Payment for order {orderId} refunded");
            }
            await Task.CompletedTask;
        }
    }
}
