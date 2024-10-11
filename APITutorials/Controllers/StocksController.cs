using APITutorials.DTOs.Stock;
using APITutorials.Helper;
using APITutorials.Mappers;
using APITutorials.Models;
using APITutorials.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APITutorials.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var stocks = await _stockService.GetAllAsync();
                var stockDto = stocks.Select(s => s.ToStockDto()).ToList();

                return Ok(stockDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockService.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdStock = await _stockService.CreateAsync(stockDto);

            return CreatedAtAction(nameof(GetById), new { id = createdStock!.Id }, createdStock.ToStockDto());
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedStock = await _stockService.UpdateAsync(id, updateDto);
            if (updatedStock == null)
            {
                return NotFound();
            }

            return Ok(updatedStock.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deletedStock = await _stockService.DeleteAsync(id);
            if (deletedStock == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("search")]
        public async Task<ActionResult<List<Stock>>> Search([FromBody] QueryObject query)
        {
            var stocks = await _stockService.SearchAsync(query);

            if (stocks == null || stocks.Count == 0)
            {
                return NotFound("No stocks found with the specified criteria.");
            }

            return Ok(stocks);
        }
    }
}
