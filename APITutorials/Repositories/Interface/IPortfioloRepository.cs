using APITutorials.Models;

namespace APITutorials.Repositories.Interface
{
    public interface IPortfioloRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
    }
}
