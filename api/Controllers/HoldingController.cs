using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using api.Repository;
using api.Services.HoldingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [Route("api/holding")]
    public class HoldingController : ControllerBase
    {
        private readonly IHoldingRepository _PortfolioRepo;
        private readonly IaccountService _AccountService;
        private readonly IHoldingService _HoldingService;

        public HoldingController(IHoldingRepository portfoliorepo, IaccountService accountservice, IHoldingService holdingService)
        {
            _PortfolioRepo = portfoliorepo;
            _AccountService = accountservice;
            _HoldingService = holdingService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserHolding()
        {
            var username = User.getUserName();
            var result = await _HoldingService.GetHoldingUser(username);

            if (result.Exit == false) return StatusCode(result.Errorcode, result.Errormessage);

            return Ok(result.Data);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> AddStock(string symbol)
        {
            var username = User.getUserName();
            var user = await _AccountService.FindByname(username);

            if (await _PortfolioRepo.ContainStock(symbol, user) == true) return BadRequest("Cannot add same stock to portfolio");

            var result = await _PortfolioRepo.AddStockToPortfolio(user, symbol);

            if (!result.Exit)
            {
                return StatusCode(result.Errorcode, result.Errormessage);
            }
            else
            {
                return Ok(result.Data);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteStock(string symbol)
        {
            var username = User.getUserName();
            var appuser = await _AccountService.FindByname(username);

            if (await _PortfolioRepo.ContainStock(symbol, appuser) == false) return BadRequest("the stock is not on the portfolio");

            var result = await _PortfolioRepo.DeleteStock(appuser, symbol);

            if (result.Exit == false)
            {
                return StatusCode(result.Errorcode, result.Errormessage);
            }

            return Ok();
        }

    }
}