using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NinjaAPI.Models;

namespace NinjaAPI.Controllers
{
    [Route("api/[controller]")]
    public class ClanController : Controller
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Clan>), 200)]
        public Task<IActionResult> ReadAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}