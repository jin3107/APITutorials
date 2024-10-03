using APITutorials.Extensions;
using APITutorials.Models;
using APITutorials.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    }
}
