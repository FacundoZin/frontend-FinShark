using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _PortfolioRepo;
        private readonly IaccountService _AccountService;

        public PortfolioController(IPortfolioService portfoliorepo, IaccountService accountservice)
        {
            _PortfolioRepo = portfoliorepo;
            _AccountService = accountservice;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.getUserName();
            var user = await _AccountService.FindByname(username);

            var portfolioresult = await _PortfolioRepo.GetUserPortfolio(user);

            if (portfolioresult.Exit == false)
            {
                return StatusCode(portfolioresult.Errorcode, portfolioresult.Errormessage);
            }
            else
            {
                return Ok(portfolioresult.Data);
            }
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