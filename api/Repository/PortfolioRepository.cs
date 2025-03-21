using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Common;
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
        private readonly UserManager<AppUser> _usermanager;
        private readonly IstockService _stockrepo;

        public PortfolioRepository(ApplicationDBcontext dBcontext, UserManager<AppUser> userManager, IstockService stockservice)
        {
            _DBcontext = dBcontext;
            _usermanager = userManager;
            _stockrepo = stockservice;
        }

        public async Task<Result<List<Stock>>> AddToPortfolio(AppUser User, string symbol)
        {
            var stock = await _stockrepo.GetbySymbolAsync(symbol);

            if (stock == null) return Result<List<Stock>>.Error("Stock not found", 400);

            Portfolio added_item = new Portfolio
            {
                stockid = stock.ID,
                appuserID = User.Id
            };

            await _DBcontext.portfolios.AddAsync(added_item);
            await _DBcontext.SaveChangesAsync();

            var portfolioresult = await GetUserPortfolio(User);

            if (portfolioresult.Exit == false) return Result<List<Stock>>.Error(portfolioresult.Errormessage, portfolioresult.Errorcode);

            var portfolio = portfolioresult.Date;

            return Result<List<Stock>>.Exito(portfolio);
        }

        public async Task<Result<List<Stock>>> GetUserPortfolio(AppUser User)
        {
            var portfolio = await _DBcontext.portfolios.Where(p => p.appuserID == User!.Id)
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

            if (portfolio == null) return Result<List<Stock>>.Error("Portfolio not found", 400);

            return Result<List<Stock>>.Exito(portfolio);
        }

        public async Task<bool> ContainStock(string symbol, AppUser User)
        {
            var portfolioresult = await GetUserPortfolio(User);

            if (portfolioresult.Exit == false) return false;

            var portfolio = portfolioresult.Date;

            if (portfolio.Any(p => p.Symbol.ToLower() == symbol.ToLower()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}