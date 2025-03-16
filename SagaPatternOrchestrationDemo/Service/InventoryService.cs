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
        public async Task<bool> CheckInventoryAsync(Guid productId)
        {
            Console.WriteLine($"Inventory for product {productId} checked");
            return await Task.FromResult(true);
        }
    }
}
