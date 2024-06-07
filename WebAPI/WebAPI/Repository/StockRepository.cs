using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Helpers;
using WebAPI.Interface;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class StockRepository : IStock
    {
        private readonly AppDbContext _context;

        public StockRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateStockAsync(Stock stock)
        {
            await _context.AddAsync(stock);
            return await SaveAsync();
        }

        public async Task<bool> DeleteStockAsync(Stock stock)
        {
            _context.Remove(stock);
            return await SaveAsync();
        }

        public async Task<List<Comment>> GetCommentsByStockAsync(int id)
        {
            return await _context.Comment.Where(s => s.StockId == id).ToListAsync();
        }

        public async Task<List<Stock>> GetStocksAsync(QueryObject query)
        {
            var stocks = _context.Stock.Include(s => s.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
                if (query.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<Stock> GetStockAsync(int stockId)
        {
            return await _context.Stock.Where(s => s.Id == stockId).Include(s => s.Comments).FirstOrDefaultAsync();
        }

        public async Task<bool> StockExistAsync(int id)
        {
            return await _context.Stock.AnyAsync(s => s.Id == id);
        }

        public async Task<bool> UpdateStockAsync(Stock stock)
        {
            _context.Update(stock);
            return await SaveAsync();
        }

        public async Task<bool> PatchStockAsync(Stock stock)
        {
            _context.Update(stock);
            return await SaveAsync();
        }
    }
}
