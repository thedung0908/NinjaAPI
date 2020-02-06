using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NinjaAPI.Models;
using NinjaAPI.Services;

namespace NinjaAPI.Controllers
{
    [Route("api/[controller]")]
    public class ClanController : Controller
    {
        private readonly IClanService _clanService;
        
        public ClanController(IClanService clanService)
        {
            _clanService = clanService ?? throw new ArgumentNullException(nameof(clanService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Clan>), 200)]
        public async Task<IActionResult> ReadAllAsync()
        {
            var allClans = await _clanService.ReadAllAsync();
            return Ok(allClans);
        }
    }
}