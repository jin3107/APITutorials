using APITutorials.Extensions;
using APITutorials.Models;
using APITutorials.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace APITutorials.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfoliosController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;

        public PortfoliosController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var userName = User.GetUserName();
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("User not authenticated");
            }

            try
            {
                var userPortfolio = await _portfolioService.GetUserPortfolioAsync(userName);
                return Ok(userPortfolio);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio([FromQuery] string symbol)
        {
            var userName = User.GetUserName();
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("User not authenticated");
            }

            try
            {
                var createdPortfolio = await _portfolioService.AddPortfolioAsync(userName, symbol);
                return Created();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio([FromQuery] string symbol)
        {
            var userName = User.GetUserName();
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("User not authenticated");
            }

            try
            {
                var result = await _portfolioService.DeletePortfolioAsync(userName, symbol);
                if (result)
                {
                    return Ok("Portfolio deleted successfully.");
                }
                else
                {
                    return BadRequest("Could not delete portfolio.");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
