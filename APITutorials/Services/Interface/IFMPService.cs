using APITutorials.Models;

namespace APITutorials.Services.Interface
{
    public interface IFMPService
    {
        Task<Stock> FindStockBySymbolAsync(string symbol);
    }
}
