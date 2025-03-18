using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
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

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {


            await _DBcontext.portfolios.AddAsync(portfolio);
            await _DBcontext.SaveChangesAsync();

            return portfolio;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser appUser)
        {

            return await _DBcontext.portfolios.Where(p => p.appuserID == appUser.Id)
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