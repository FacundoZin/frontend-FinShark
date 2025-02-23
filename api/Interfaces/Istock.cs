using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stock;
using api.Models;

namespace api.Interfaces
{

    public interface Istock
    {
        Task<List<Stock>> GetAllasync();
        Task<Stock?> Getbyidasync(int id);
        Task<Stock> Createasync(Stock stock);
        Task<Stock?> Updateasync(int id, UpdateStockDto updatedstock);
        Task<Stock?> Deleteasync(int id);
    }
}