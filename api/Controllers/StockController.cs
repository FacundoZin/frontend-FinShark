using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{

    [Route("api/stock")]
    [ApiController]
    public class StockController : Controller
    {

        private readonly ApplicationDBcontext _context;


        public StockController(ApplicationDBcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _context.Stocks.ToListAsync();
            var result = stocks.Select(s => s.toStockDto());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(stock);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto NewStock)
        {
            var stock = NewStock.ToCreateStockDto();
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByID), new { id = stock.ID }, stock.toStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto)
        {


            Stock stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            stock.Symbol = updateStockDto.Symbol;
            stock.Companyname = updateStockDto.Companyname;
            stock.Purchase = updateStockDto.Purchase;
            stock.LastDiv = updateStockDto.LastDiv;
            stock.Industry = updateStockDto.Industry;
            stock.MarketCap = updateStockDto.MarketCap;

            await _context.SaveChangesAsync();

            return Ok(stock);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return NotFound();
        }
    }
}
