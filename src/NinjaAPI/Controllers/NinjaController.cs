using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NinjaAPI.Models;
using NinjaAPI.Services;

namespace NinjaAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class NinjaController : Controller
    {
        private readonly INinjaService _ninjaService;
        public NinjaController(INinjaService ninjaService)
        {
            _ninjaService = ninjaService ?? throw new ArgumentNullException(nameof(ninjaService));
        }

        // GET: api/Ninja
        [HttpGet]
        public Task<IActionResult> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        // GET: api/Ninja/5
        [HttpGet("{clan}")]
        public Task<IActionResult> ReadAllClanAsync(string clanName)
        {
            throw new NotImplementedException();
        }

        // GET: api/Ninja/5/2
        [HttpGet("{clan}/{key}")]
        public Task<IActionResult> ReadOneAsync(string clanName, string key)
        {
            throw new NotImplementedException();
        }

        // Create: api/Ninja
        [HttpPost]
        public Task<IActionResult> CreateAsync([FromBody]Ninja ninja)
        {
            throw new NotImplementedException();
        }
        
        // Update: api/Ninja
        [HttpPut]
        public Task<IActionResult> UpdateAsync([FromBody]Ninja ninja)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Ninja/5/2
        [HttpDelete("{clan}/{key}")]
        public void DeleteAsync(string clanName, string key)
        {
            throw new NotImplementedException();
        }
    }
}
