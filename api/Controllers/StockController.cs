using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.mappers;
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
        public async Task<IActionResult> CreateStock([FromBody] CreateStockDto Stock)
        {
            var stockmodel = Stock.ToCreateStockDto();
            _context.Stocks.Add(stockmodel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetByID), new { id = stockmodel.ID }, stockmodel.toStockDto());
        }
    }
}
