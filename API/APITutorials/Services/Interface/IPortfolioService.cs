using APITutorials.Models;

namespace APITutorials.Services.Interface
{
    public interface IPortfolioService
    {
        Task<List<Stock>> GetUserPortfolioAsync(string userName);
        Task<Portfolio> AddPortfolioAsync(string userName, string symbol);
        Task<bool> DeletePortfolioAsync(string userName, string symbol);
    }
}
