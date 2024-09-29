using APITutorials.DTOs.Stock;
using APITutorials.Models;

namespace APITutorials.Repositories.Interface
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(Guid id);
        Task<Stock?> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(Guid id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(Guid id);
        Task<bool> StockExits(Guid id);
    }
}
