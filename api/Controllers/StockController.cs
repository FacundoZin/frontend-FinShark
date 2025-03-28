using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Helpers;
using api.Interfaces;
using api.mappers;
using api.Models;
using api.Services.StockService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{

    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly IStockService _StockService;
        public StockController(IStockService stockService)
        {
            _StockService = stockService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] StockQueryObject query)
        {

            var stocks = await _StockService.GetAllStocksAsync(query);

            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            var Result = await _StockService.GetStockByIdAsync(id);

            if (Result.Exit == true)
            {
                return Ok(Result.Data);
            }
            else
            {
                return StatusCode(Result.Errorcode, Result.Errormessage);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto createStockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = createStockDto.ToStockfromCreateDto();
            await Stockrepository.Createasync(stock);
            return CreatedAtAction(nameof(GetByID), new { id = stock.ID }, stock.toStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto updatedStock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = await Stockrepository.Updateasync(id, updatedStock);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.toStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var stock = await Stockrepository.Deleteasync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
