using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Common;
using api.DTOs.Stock;

namespace api.Services.HoldingService
{
    public interface IHoldingService
    {
        Task<Result<List<StockDto>?>> GetHoldingUser(string username);

    }
}