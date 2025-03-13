using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioService
    {
        private ApplicationDBcontext _DBcontext;

        public PortfolioRepository(ApplicationDBcontext dBcontext)
        {
            _DBcontext = dBcontext;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _DBcontext.portfolios.Where(p => p.appuserID == user.Id)
            .Select(S => new Stock
            {
                ID = S.stockid,
                Symbol = S.stock.Symbol,
                Companyname = S.stock.Companyname,
                Purchase = S.stock.Purchase,
                LastDiv = S.stock.LastDiv,
                Industry = S.stock.Industry,
                MarketCap = S.stock.MarketCap
            }).ToListAsync();
        }
    }
}