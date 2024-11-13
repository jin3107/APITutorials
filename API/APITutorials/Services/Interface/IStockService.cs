using APITutorials.DTOs.Stock;
using APITutorials.Helper;
using APITutorials.Models;

namespace APITutorials.Services.Interface
{
    public interface IStockService
    {
        Task<Stock?> CreateAsync(CreateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(Guid id);
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(Guid id);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<List<Stock>> SearchAsync(QueryObject query);
        Task<bool> StockExistsAsync(Guid id);
        Task<Stock?> UpdateAsync(Guid id, UpdateStockRequestDto stockDto);
    }
}
