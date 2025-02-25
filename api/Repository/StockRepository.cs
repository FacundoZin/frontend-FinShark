using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Interfaces;
using api.mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : Istock
    {
        private readonly ApplicationDBcontext _context;
        public StockRepository(ApplicationDBcontext context)
        {
            _context = context;
        }

        public async Task<Stock> Createasync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> Deleteasync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return null;
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllasync()
        {
            return await _context.Stocks.Include(c => c.Comments).ToListAsync();

        }

        public async Task<Stock?> Getbyidasync(int id)
        {
            var stock = await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.ID == id);

            if (stock == null)
            {
                return null;
            }

            return stock;
        }

        public async Task<bool> StockExist(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.ID == id);

        }

        public async Task<Stock?> Updateasync(int id, UpdateStockDto updatedStock)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return null;
            }

            stock.Symbol = updatedStock.Symbol;
            stock.Companyname = updatedStock.Companyname;
            stock.Purchase = updatedStock.Purchase;
            stock.LastDiv = updatedStock.LastDiv;
            stock.Industry = updatedStock.Industry;
            stock.MarketCap = updatedStock.MarketCap;

            await _context.SaveChangesAsync();

            return stock;
        }
    }
}