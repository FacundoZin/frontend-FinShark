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

        public HoldingService(IaccountService accountservice, IHoldingRepository holdingRepository)
        {
            _AccountService = accountservice;
            _HoldingRepository = holdingRepository;
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