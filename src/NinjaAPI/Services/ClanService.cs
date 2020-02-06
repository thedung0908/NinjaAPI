using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NinjaAPI.Models;
using NinjaAPI.Repositories;

namespace NinjaAPI.Services
{
    public class ClanService : IClanService
    {
        private IClanRepository _clanRepository;
        public ClanService(IClanRepository clanRepository)
        {
            _clanRepository = clanRepository ?? throw new ArgumentNullException(nameof(clanRepository));
        }
        public Task<Clan> CreateAsync(Clan clan)
        {
            return _clanRepository.CreateAsync(clan);
        }

        public Task<Clan> DeleteAsync(string clanName)
        {
            return _clanRepository.DeleteAsync(clanName);
        }

        public async Task<bool> IsClanExistsAsync(string clanName)
        {
            return await _clanRepository.ReadOneAsync(clanName) != null;
        }

        public Task<IEnumerable<Clan>> ReadAllAsync()
        {
            return _clanRepository.ReadAllAsync();
        }

        public Task<Clan> ReadOneAsync(string clanName)
        {
            return _clanRepository.ReadOneAsync(clanName);
        }

        public Task<Clan> UpdateAsync(Clan clan)
        {
            return _clanRepository.UpdateAsync(clan);
        }
    }
}
