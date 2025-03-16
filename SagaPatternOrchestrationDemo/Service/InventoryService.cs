using SagaPatternOrchestrationDemo.Data;
using SagaPatternOrchestrationDemo.Model;

namespace SagaPatternOrchestrationDemo.Service
{
    /// <summary>
    /// Stok Kontrolü : Ürün stok durumu kontrol edilir.
    /// </summary>
    public interface IInventoryService
    {
        Task<bool> CheckInventoryAsync(Guid productId);
        Task<bool> CreateInventoryAsync(Guid productId, int stockQuantity);
        Task<bool> DecreaseStockAsync(Guid productId, int quantity);
        Task<bool> IncreaseStockAsync(Guid productId, int quantity);
    }
    public class InventoryService : IInventoryService
    {
        private readonly ApplicationDbContext _context;

        public InventoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Stokta yeterli miktar olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<bool> CheckInventoryAsync(Guid productId)
        {
            var inventory = await _context.Inventories.FindAsync(productId);
            return inventory != null && inventory.StockQuantity > 0;
        }

        /// <summary>
        /// Yeni bir ürün ekler veya stok günceller.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="stockQuantity"></param>
        /// <returns></returns>
        public async Task<bool> CreateInventoryAsync(Guid productId, int stockQuantity)
        {
            var inventory = await _context.Inventories.FindAsync(productId);
            if (inventory == null)
            {
                _context.Inventories.Add(new Inventory
                {
                    ProductId = productId,
                    StockQuantity = stockQuantity
                });
            }
            else
            {
                inventory.StockQuantity += stockQuantity; // Eğer ürün varsa stoğu artır
            }

            await _context.SaveChangesAsync();
            return true;
        }
        /// <summary>
        ///  Stok miktarını azaltır (Sipariş verildiğinde kullanılır).
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> DecreaseStockAsync(Guid productId, int quantity)
        {
            var inventory = await _context.Inventories.FindAsync(productId);
            if (inventory == null || inventory.StockQuantity < quantity)
                return false;

            inventory.StockQuantity -= quantity;
            await _context.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// Stok miktarını artırır (İptal durumunda kullanılabilir).
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> IncreaseStockAsync(Guid productId, int quantity)
        {
            var inventory = await _context.Inventories.FindAsync(productId);
            if (inventory == null)
                return false;

            inventory.StockQuantity += quantity;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
