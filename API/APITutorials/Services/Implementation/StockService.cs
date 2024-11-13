using APITutorials.DTOs.Stock;
using APITutorials.Helper;
using APITutorials.Mappers;
using APITutorials.Models;
using APITutorials.Repositories.Interface;
using APITutorials.Services.Interface;

namespace APITutorials.Services.Implementation
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<Stock?> CreateAsync(CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDto();
            return await _stockRepository.CreateAsync(stockModel);
        }

        public async Task<Stock?> DeleteAsync(Guid id)
        {
            return await _stockRepository.DeleteAsync(id);
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _stockRepository.GetAllAsync();
        }

        public async Task<Stock?> GetByIdAsync(Guid id)
        {
            return await _stockRepository.GetByIdAsync(id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _stockRepository.GetBySymbolAsync(symbol);
        }

        public async Task<List<Stock>> SearchAsync(QueryObject query)
        {
            return await _stockRepository.SearchAsync(query);
        }

        public async Task<bool> StockExistsAsync(Guid id)
        {
            return await _stockRepository.StockExits(id);
        }

        public async Task<Stock?> UpdateAsync(Guid id, UpdateStockRequestDto stockDto)
        {
            return await _stockRepository.UpdateAsync(id, stockDto);
        }
    }
}
