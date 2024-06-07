using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.Interface
{
    public interface IStock
    {
        Task<List<Stock>> GetStocksAsync(QueryObject query);
        Task<Stock> GetStockAsync(int stockId);
        Task<bool> StockExistAsync(int stockId);
        Task<bool> CreateStockAsync(Stock stock);
        Task<bool> UpdateStockAsync(Stock stock);
        Task<bool> DeleteStockAsync(Stock stock);
        Task<List<Comment>> GetCommentsByStockAsync(int stockId);

        Task<bool> SaveAsync();

    }
}
