using APITutorials.Data;
using APITutorials.DTOs.Stock;
using APITutorials.Helper;
using APITutorials.Models;
using APITutorials.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace APITutorials.Repositories.Implementation
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock?> CreateAsync(Stock stockModel)
        {
            stockModel.Id = Guid.NewGuid();
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();

            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(Guid id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return null;
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks
                .Include(c => c.Comments).ThenInclude(a => a.AppUser).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(Guid id)
        {
            return await _context.Stocks.Include(c => c.Comments)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }

        public async Task<List<Stock>> SearchAsync(QueryObject query)
        {
            var stocks = _context.Stocks
                .Include(c => c.Comments).ThenInclude(a => a.AppUser).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName!.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol!.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                switch (query.SortBy.ToLower())
                {
                    case "symbol":
                        stocks = query.IsDecsending
                            ? stocks.OrderByDescending(s => s.Symbol)
                            : stocks.OrderBy(s => s.Symbol);
                        break;
                    case "companyname":
                        stocks = query.IsDecsending
                            ? stocks.OrderByDescending(s => s.CompanyName)
                            : stocks.OrderBy(s => s.CompanyName);
                        break;
                    case "marketcap":
                        stocks = query.IsDecsending
                            ? stocks.OrderByDescending(s => s.MarketCap)
                            : stocks.OrderBy(s => s.MarketCap);
                        break;
                    case "lastdiv":
                        stocks = query.IsDecsending
                            ? stocks.OrderByDescending(s => s.LastDiv)
                            : stocks.OrderBy(s => s.LastDiv);
                        break;
                    default:
                        break;
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            var result = await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
            
            return result;
        }

        public async Task<bool> StockExits(Guid id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(Guid id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingStock == null)
            {
                return null;
            }

            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();
            return existingStock;
        }
    }
}
