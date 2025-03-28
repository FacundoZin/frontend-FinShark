using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services.StockService
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _StockRepo;

        public StockService(IStockRepository stockRepository)
        {
            _StockRepo = stockRepository;
        }

        public async Task<List<Stock?>> GetAllStocksAsync(StockQueryObject queryObject)
        {
            var Stocks = _StockRepo.GetAllStocks();

            if (!string.IsNullOrWhiteSpace(queryObject.CompanyName))
            {
                Stocks = Stocks.Where(s => s.Companyname.Contains(queryObject.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                Stocks = Stocks.Where(s => s.Symbol.Contains(queryObject.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if (queryObject.SortBy.Equals("symbol", StringComparison.OrdinalIgnoreCase))
                {
                    if (queryObject.IsDecsending == true)
                    {
                        Stocks = Stocks.OrderByDescending(s => s.Symbol);
                    }
                    else
                    {
                        Stocks = Stocks.OrderBy(s => s.Symbol);
                    }
                }
            }

            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;

            return await Stocks.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
        }
    }
}