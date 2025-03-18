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
        private readonly UserManager<AppUser> _usermanager;
        private readonly IPortfolioService _PortfolioRepo;
        private readonly IstockService _stockrepo;


        public PortfolioController(UserManager<AppUser> userManager, IPortfolioService portfoliorepo,
        IstockService stockrepo)
        {
            _usermanager = userManager;
            _PortfolioRepo = portfoliorepo;
            _stockrepo = stockrepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.getUserName();
            var appUser = await _usermanager.FindByNameAsync(username);

            var userportfolio = _PortfolioRepo.GetUserPortfolio(appUser!);

            return Ok(userportfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToPortfolio(string symbol)
        {
            var username = User.getUserName();
            var appUser = await _usermanager.FindByNameAsync(username);
            var stock = _stockrepo.GetbySymbolAsync(symbol);

            if (stock == null) return BadRequest("stock not found");

            var Userportfolio = await _PortfolioRepo.GetUserPortfolio(appUser!);

            if (Userportfolio.Any(p => p.Symbol.ToLower() == symbol.ToLower())) return BadRequest("cannot add same stock to portfolio");

            var portfoliomodel = new Portfolio
            {
                stockid = stock.Id,
                appuserID = appUser.Id
            };

            await _PortfolioRepo.CreateAsync(portfoliomodel);

            if (portfoliomodel == null)
            {
                return StatusCode(500, "could not add");
            }
            else
            {
                return Created();
            }



        }

    }
}