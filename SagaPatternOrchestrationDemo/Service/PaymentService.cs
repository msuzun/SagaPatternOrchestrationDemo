using Microsoft.EntityFrameworkCore;
using SagaPatternOrchestrationDemo.Data;
using SagaPatternOrchestrationDemo.Model;

namespace SagaPatternOrchestrationDemo.Service
{
    /// <summary>
    /// Ödeme Alma : Sipariş için ödeme alınır.
    /// Ödeme İadesi : Eğer sipariş iptal edilirse, ödeme geri alınır.
    /// Güncelleme : Ödeme bilgileri veritabanında tutuluyor
    /// </summary>
    public interface IPaymentService
    {
        Task<bool> ProccessPaymentService(Guid orderId);
        Task RefundPaymentAsync(Guid orderId);
    }
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ProccessPaymentService(Guid orderId)
        {

            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            var payment = new Payment
            {
                PaymentId = Guid.NewGuid(),
                OrderId = orderId,
                Amount = order.Amount
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task RefundPaymentAsync(Guid orderId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.OrderId == orderId);
            if (payment != null)
            {
                payment.IsRefunded = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
