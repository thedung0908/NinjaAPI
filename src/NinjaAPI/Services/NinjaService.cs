using NinjaAPI.Models;
using NinjaAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaAPI.Services
{
    public class NinjaService : INinjaService
    {
        private INinjaRepository _ninjaRepository;
        public NinjaService(INinjaRepository ninjaRepository)
        {
            _ninjaRepository = ninjaRepository ?? throw new ArgumentNullException(nameof(ninjaRepository));
        }
        public Task<Ninja> CreateAsync(Ninja ninja)
        {
            throw new NotImplementedException();
        }

        public Task<Ninja> DeleteAsync(string clanName, string ninjaKey)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Ninja>> ReadAllAsync()
        {
            var allNinjas = _ninjaRepository.ReadAllAsync();
            return allNinjas;
        }

        public Task<IEnumerable<Ninja>> ReadAllClanAsync(string clanName)
        {
            try
            {
                var allNinjasInClan = _ninjaRepository.ReadAllClanAsync(clanName);
                return allNinjasInClan;
            }
            catch (ClanNotFoundException)
            {
                return null;
            }
        }

        public Task<Ninja> ReadOneAsync(string clanName, string ninjaKey)
        {
            throw new NotImplementedException();
        }

        public Task<Ninja> UpdateAsync(Ninja ninja)
        {
            throw new NotImplementedException();
        }
    }
}
