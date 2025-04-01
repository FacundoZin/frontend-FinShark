using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Common;
using api.DTOs.Stock;
using api.Interfaces;
using api.mappers;
using api.Models;

namespace api.Services.HoldingService
{
    public class HoldingService : IHoldingService
    {
        private readonly IaccountService _AccountService;
        private readonly IHoldingRepository _HoldingRepository;
        private readonly IStockRepository _StockRepo;
        private readonly IFMPService _FMPservice;

        public HoldingService(IaccountService accountservice, IHoldingRepository holdingRepository,
        IStockRepository stockrepo, IFMPService fMPService)
        {
            _AccountService = accountservice;
            _HoldingRepository = holdingRepository;
            _StockRepo = stockrepo;
            _FMPservice = fMPService;
        }

        public async Task<Result<AddedstockToHolding>> AddStock(string username, string symbol)
        {
            var appUser = await _AccountService.FindByname(username);
            if (appUser == null) return Result<AddedstockToHolding>.Error("user not found", 404);

            var stock = await _StockRepo.GetbySymbolAsync(symbol);

            if (stock == null)
            {
                var search = await _FMPservice.FindBySymbolAsync(symbol);

                if (search.Data == null) return Result<AddedstockToHolding>.Error("this stock not exist", 400);

                stock = search.Data;
            }

            bool result = await _HoldingRepository.AddStockToHolding(appUser, stock);

            if (result == false) return Result<AddedstockToHolding>.Error("sorry, something went wrong", 500);

            var stockadded = stock.ToAddedStockHoldingfromStock();

            return Result<AddedstockToHolding>.Exito(stockadded)
        }

        public async Task<Result<List<StockDto>?>> GetHoldingUser(string username)
        {
            try
            {
                var appUser = await _AccountService.FindByname(username);

                if (appUser == null) return Result<List<StockDto>?>.Error("User not found", 404);

                var userstocks = await _HoldingRepository.GetHoldingByUser(appUser);

                if (userstocks == null) return Result<List<StockDto>?>.Exito(null);

                var Holding = userstocks.Select(s => s.toStockDto()).ToList();

                return Result<List<StockDto>?>.Exito(Holding);
            }
            catch (Exception ex)
            {
                return Result<List<StockDto>?>.Error("we are sorry, something went wrong", 500);
            }
        }
    }
}