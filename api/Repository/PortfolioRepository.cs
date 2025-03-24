using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Common;
using api.Data;
using api.DTOs.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioService
    {
        private ApplicationDBcontext _DBcontext;
        private readonly UserManager<AppUser> _usermanager;
        private readonly IstockService _stockrepo;
        private readonly IFMPService _FMPservice;

        public PortfolioRepository(ApplicationDBcontext dBcontext, UserManager<AppUser> userManager, IstockService stockservice,
        IFMPService fMPService)
        {
            _DBcontext = dBcontext;
            _usermanager = userManager;
            _stockrepo = stockservice;
            _FMPservice = fMPService;
        }

        public async Task<Result<List<Stock>>> AddStockToPortfolio(AppUser User, string symbol)
        {
            var stock = await _stockrepo.GetbySymbolAsync(symbol);

            if (stock == null)
            {
                var search_in_fmp = await _FMPservice.FindBySymbolAsync(symbol);

                if (search_in_fmp.Exit == false)
                {
                    return Result<List<Stock>>.Error(search_in_fmp.Errormessage, search_in_fmp.Errorcode);
                }
                else
                {
                    await _stockrepo.Createasync(search_in_fmp.Data);
                    stock = await _stockrepo.GetbySymbolAsync(symbol);
                }
            }


            if (stock == null) return Result<List<Stock>>.Error("Stock not found", 400);

            Portfolio added_item = new Portfolio
            {
                StockID = stock.ID,
                AppUserID = User.Id
            };

            await _DBcontext.portfolios.AddAsync(added_item);
            await _DBcontext.SaveChangesAsync();

            var portfolioresult = await GetUserPortfolio(User);

            if (portfolioresult.Exit == false) return Result<List<Stock>>.Error(portfolioresult.Errormessage, portfolioresult.Errorcode);

            var portfolio = portfolioresult.Data;

            return Result<List<Stock>>.Exito(portfolio);
        }

        public async Task<Result<List<Stock>>> GetUserPortfolio(AppUser User)
        {
            var portfolio = await _DBcontext.portfolios.Where(p => p.AppUserID == User!.Id)
            .Select(S => new Stock
            {
                ID = S.StockID,
                Symbol = S.Stock.Symbol,
                Companyname = S.Stock.Companyname,
                Purchase = S.Stock.Purchase,
                LastDiv = S.Stock.LastDiv,
                Industry = S.Stock.Industry,
                MarketCap = S.Stock.MarketCap
            }).ToListAsync();

            if (portfolio == null) return Result<List<Stock>>.Error("Portfolio not found", 400);

            return Result<List<Stock>>.Exito(portfolio);
        }

        public async Task<Result<List<Stock>>> DeleteStock(AppUser user, string symbol)
        {
            var stock = await _stockrepo.GetbySymbolAsync(symbol);

            if (stock == null) return Result<List<Stock>>.Error("Stock not found", 400);

            Portfolio delete_item = new Portfolio
            {
                StockID = stock.ID,
                AppUserID = user.Id
            };

            _DBcontext.portfolios.Remove(delete_item);
            await _DBcontext.SaveChangesAsync();

            return Result<List<Stock>>.Exito(null);
        }

        public async Task<bool> ContainStock(string symbol, AppUser User)
        {
            var portfolioresult = await GetUserPortfolio(User);

            if (portfolioresult.Exit == false) return false;

            var portfolio = portfolioresult.Data;

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