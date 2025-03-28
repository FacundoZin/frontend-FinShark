using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Common;
using api.DTOs.Stock;
using api.Helpers;
using api.Models;

namespace api.Services.StockService
{
    public interface IStockService
    {
        Task<List<StockDto?>> GetAllStocksAsync(StockQueryObject queryObject);
        Task<Result<StockDto?>> GetStockByIdAsync(int id);
    }
}