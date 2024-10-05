using APITutorials.Extensions;
using APITutorials.Models;
using APITutorials.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace APITutorials.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfoliosController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfioloRepository _portfioloRepository;

        public PortfoliosController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfioloRepository portfioloRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfioloRepository = portfioloRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var userName = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(userName);
            var userPortfolio = await _portfioloRepository.GetUserPortfolio(appUser!);

            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var userName = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(userName);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);

            if (stock == null) return BadRequest("Stock not found!");
            var userPortfolio = await _portfioloRepository.GetUserPortfolio(appUser!);
            if (userPortfolio.Any(e => e.Symbol!.ToLower() == symbol.ToLower())) return BadRequest("Can not add same stock to portfolio!");

            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser!.Id,
            };

            await _portfioloRepository.CreateAsync(portfolioModel);
            if (portfolioModel == null) return StatusCode(500, "Could not create!");
            else return Created();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePrtfolio(string symbol)
        {
            var userName = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(userName);
            var userPortfolio = await _portfioloRepository.GetUserPortfolio(appUser!);

            var filterStock = userPortfolio.Where(s => s.Symbol!.ToLower() == symbol.ToLower()).ToList();
            if (filterStock.Count() == 1)
            {
                await _portfioloRepository.DeletePortfolio(appUser!, symbol);
            }
            else
            {
                return BadRequest("Stock not in your Portfolio");
            }

            return Ok();
        }
    }
}
