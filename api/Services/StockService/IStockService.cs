using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;

namespace api.Services.StockService
{
    public interface IStockService
    {
        Task<List<Stock?>> GetAllStocksAsync(StockQueryObject queryObject);
    }
}