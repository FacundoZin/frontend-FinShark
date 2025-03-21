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
        private readonly IstockService _stockrepo;
        private readonly IaccountService _AccountService;


        public PortfolioController(IPortfolioService portfoliorepo, IaccountService accountservice,
        IstockService stockrepo)
        {
            _PortfolioRepo = portfoliorepo;
            _stockrepo = stockrepo;
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
                return Ok(portfolioresult.Date);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToPortfolio(string symbol)
        {
            var username = User.getUserName();
            var user = await _AccountService.FindByname(username);

            if (await _PortfolioRepo.ContainStock(symbol, user) == false) return BadRequest("Cannot add same stock to portfolio");

            var result = await _PortfolioRepo.AddToPortfolio(user, symbol);

            if (!result.Exit)
            {
                return StatusCode(result.Errorcode, result.Errormessage);
            }
            else
            {
                return Ok(result);
            }
        }

    }
}