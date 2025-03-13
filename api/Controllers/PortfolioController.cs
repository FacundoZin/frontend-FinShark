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


        public PortfolioController(UserManager<AppUser> userManager, IPortfolioService portfoliorepo)
        {
            _usermanager = userManager;
            _PortfolioRepo = portfoliorepo;
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

    }
}