using SagaPatternOrchestrationDemo.Data;

namespace SagaPatternOrchestrationDemo.Service
{
    /// <summary>
    /// Stok Kontrolü : Ürün stok durumu kontrol edilir.
    /// </summary>
    public interface IInventoryService
    {
        Task<bool> CheckInventoryAsync(Guid productId);
    }
    public class InventoryService : IInventoryService
    {
        private readonly ApplicationDbContext _context;

        public InventoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckInventoryAsync(Guid productId)
        {
            var inventory = await _context.Inventories.FindAsync(productId);
            return inventory != null && inventory.StockQuantity > 0;
        }
    }
}
