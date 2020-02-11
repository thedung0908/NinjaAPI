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
        [ProducesResponseType(typeof(IEnumerable<Ninja>), StatusCodes.Status200OK)]
        public Task<IActionResult> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        // GET: api/Ninja/5
        [HttpGet("{clan}")]
        [ProducesResponseType(typeof(IEnumerable<Ninja>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> ReadAllClanAsync(string clanName)
        {
            throw new NotImplementedException();
        }

        // GET: api/Ninja/5/2
        [HttpGet("{clan}/{key}")]
        [ProducesResponseType(typeof(Ninja), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> ReadOneAsync(string clanName, string key)
        {
            throw new NotImplementedException();
        }

        // Create: api/Ninja
        [HttpPost]
        [ProducesResponseType(typeof(Ninja), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> CreateAsync([FromBody]Ninja ninja)
        {
            throw new NotImplementedException();
        }
        
        // Update: api/Ninja
        [HttpPut]
        [ProducesResponseType(typeof(Ninja), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> UpdateAsync([FromBody]Ninja ninja)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Ninja/5/2
        [HttpDelete("{clan}/{key}")]
        [ProducesResponseType(typeof(Ninja), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> DeleteAsync(string clanName, string key)
        {
            throw new NotImplementedException();
        }
    }
}
