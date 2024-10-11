using APITutorials.Extensions;
using APITutorials.Models;
using APITutorials.Repositories.Interface;
using APITutorials.Services.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITutorials.Services.Implementation
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfioloRepository _portfolioRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFMPService _fmpService;

        public PortfolioService(
            IPortfioloRepository portfolioRepository,
            IStockRepository stockRepository,
            UserManager<AppUser> userManager,
            IFMPService fmpService)
        {
            _portfolioRepository = portfolioRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
            _fmpService = fmpService;
        }

        public async Task<List<Stock>> GetUserPortfolioAsync(string userName)
        {
            var appUser = await _userManager.FindByNameAsync(userName);
            if (appUser == null)
            {
                throw new ArgumentException("User not found");
            }

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            return userPortfolio;
        }

        public async Task<Portfolio> AddPortfolioAsync(string userName, string symbol)
        {
            var appUser = await _userManager.FindByNameAsync(userName);
            if (appUser == null)
            {
                throw new ArgumentException("User not found");
            }

            var stock = await _stockRepository.GetBySymbolAsync(symbol);
            if (stock == null)
            {
                stock = await _fmpService.FindStockBySymbolAsync(symbol);
                if (stock == null)
                {
                    throw new ArgumentException("Stock does not exist");
                }
                else
                {
                    stock = await _stockRepository.CreateAsync(stock);
                }
            }

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            if (userPortfolio.Any(e => e.Symbol!.ToLower() == symbol.ToLower()))
            {
                throw new InvalidOperationException("Cannot add the same stock to portfolio!");
            }

            var portfolioModel = new Portfolio
            {
                StockId = stock!.Id,
                AppUserId = appUser.Id,
            };

            var createdPortfolio = await _portfolioRepository.CreateAsync(portfolioModel);
            if (createdPortfolio == null)
            {
                throw new Exception("Could not create portfolio");
            }

            return createdPortfolio;
        }

        public async Task<bool> DeletePortfolioAsync(string userName, string symbol)
        {
            var appUser = await _userManager.FindByNameAsync(userName);
            if (appUser == null)
            {
                throw new ArgumentException("User not found");
            }

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            var filterStock = userPortfolio.Where(s => s.Symbol!.ToLower() == symbol.ToLower()).ToList();

            if (filterStock.Count != 1)
            {
                throw new InvalidOperationException("Stock not in your Portfolio");
            }

            var deletedPortfolio = await _portfolioRepository.DeletePortfolio(appUser, symbol);
            if (deletedPortfolio == null)
            {
                throw new Exception("Could not delete portfolio");
            }

            return true;
        }
    }
}
