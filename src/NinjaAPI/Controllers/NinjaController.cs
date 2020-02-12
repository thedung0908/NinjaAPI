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
        public async Task<IActionResult> ReadAllAsync()
        {
            var allNinjas = await _ninjaService.ReadAllAsync();
            return Ok(allNinjas);
        }

        // GET: api/Ninja/5
        [HttpGet("{clan}")]
        [ProducesResponseType(typeof(IEnumerable<Ninja>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReadAllClanAsync(string clanName)
        {
            try
            {
                var allNinjasInClan = await _ninjaService.ReadAllClanAsync(clanName);
                return Ok(allNinjasInClan);
            }
            catch (ClanNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/Ninja/5/2
        [HttpGet("{clan}/{key}")]
        [ProducesResponseType(typeof(Ninja), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReadOneAsync(string clanName, string key)
        {
            try
            {
                var ninja = await _ninjaService.ReadOneAsync(clanName, key);
                return Ok(ninja);
            }
            catch (NinjaNotFoundException)
            {
                return NotFound();
            }
        }

        // Create: api/Ninja
        [HttpPost]
        [ProducesResponseType(typeof(Ninja), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody]Ninja ninja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdNinja = await _ninjaService.CreateAsync(ninja);
            return CreatedAtAction(nameof(ReadOneAsync), new { clan = createdNinja.Clan.Name, key = createdNinja.Key }, createdNinja);

        }
        
        // Update: api/Ninja
        [HttpPut]
        [ProducesResponseType(typeof(Ninja), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody]Ninja ninja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updatedNinja = await _ninjaService.UpdateAsync(ninja);
                return Ok(updatedNinja);
            }
            catch (NinjaNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE: api/Ninja/5/2
        [HttpDelete("{clan}/{key}")]
        [ProducesResponseType(typeof(Ninja), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(string clanName, string key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var deletedNinja = await _ninjaService.DeleteAsync(clanName, key);
                return Ok(deletedNinja);
            }
            catch (NinjaNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
