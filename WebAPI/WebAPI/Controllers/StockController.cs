using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;
using WebAPI.Helpers;
using WebAPI.Interface;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStock _stock;
        private readonly IMapper _mapper;

        public StockController(IStock stock, IMapper mapper)
        {
            _stock = stock;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetStocks([FromQuery] QueryObject query)
        {
            var stocks = _mapper.Map<List<StockDto>>(await _stock.GetStocksAsync(query));

            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!stocks.Any()) return NotFound();

            return Ok(stocks);
        }

        [HttpGet("{stockId}")]
        public async Task<IActionResult> GetById(int stockId)
        {
            if (!await _stock.StockExistAsync(stockId)) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stock = _mapper.Map<StockDto>(await _stock.GetStockAsync(stockId));

            return Ok(stock);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateStock([FromBody] StockDto stockCreate)
        {
            if (stockCreate == null) return BadRequest(ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stockMap = _mapper.Map<Stock>(stockCreate);

            if (!await _stock.CreateStockAsync(stockMap))
            {
                ModelState.AddModelError("", "Something went wrong in creating the stock ");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created!");

        }

        [HttpPatch("{stockId:int}/UpdatePartial")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> PatchStock(int stockId, [FromBody] JsonPatchDocument<StockDto> stockUpdate)
        {

            if (!await _stock.StockExistAsync(stockId))
            {
                return NotFound();
            }

            var stock = await _stock.GetStockAsync(stockId);
            if (stock == null)
            {
                return NotFound();
            }

            var stockDto = _mapper.Map<StockDto>(stock);
            stockUpdate.ApplyTo(stockDto, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            _mapper.Map(stockDto, stock);
            if (!await _stock.UpdateStockAsync(stock))
            {
                ModelState.AddModelError("", "Something went wrong while updating the stock");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfulyy Updated");
        }

        [HttpDelete("{stockId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteStock(int stockId)
        {
            if (!await _stock.StockExistAsync(stockId)) return NotFound();
            if (!ModelState.IsValid) return BadRequest();

            var stockDelete = await _stock.GetStockAsync(stockId);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _stock.DeleteStockAsync(stockDelete))
            {
                ModelState.AddModelError("", "Something went wrong in deleting the stock");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Deleted");
        }

        [HttpGet("comments/{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]

        public async Task<IActionResult> GetCommentsByStock(int id)
        {
            var comments = _mapper.Map<List<CommentDto>>(await _stock.GetCommentsByStockAsync(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!await _stock.StockExistAsync(id)) return NotFound();

            return Ok(comments);
        }
    }

}
