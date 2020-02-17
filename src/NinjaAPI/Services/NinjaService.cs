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
        private IClanService _clanService;
        public NinjaService(INinjaRepository ninjaRepository, IClanService clanService)
        {
            _ninjaRepository = ninjaRepository ?? throw new ArgumentNullException(nameof(ninjaRepository));
            _clanService = clanService ?? throw new ArgumentNullException(nameof(clanService));
        }
        public async Task<Ninja> CreateAsync(Ninja ninja)
        {
            if (!await _clanService.IsClanExistsAsync(ninja.Clan.Name))
            {
                throw new ClanNotFoundException(ninja.Clan.Name);
            }
            return await _ninjaRepository.CreateAsync(ninja);
        }

        public async Task<Ninja> DeleteAsync(string clanName, string ninjaKey)
        {
            await EnforceNinjaExistenceAsync(clanName, ninjaKey);
            return await _ninjaRepository.DeleteAsync(clanName, ninjaKey);
        }

        public Task<IEnumerable<Ninja>> ReadAllAsync()
        {
            var allNinjas = _ninjaRepository.ReadAllAsync();
            return allNinjas;
        }

        public async Task<IEnumerable<Ninja>> ReadAllClanAsync(string clanName)
        {
            bool isClanExist = await _clanService.IsClanExistsAsync(clanName);
            if (!isClanExist)
            {
                throw new ClanNotFoundException(clanName);
            }
            return await _ninjaRepository.ReadAllClanAsync(clanName);
        }

        public async Task<Ninja> ReadOneAsync(string clanName, string ninjaKey)
        {
            bool isClanExist = await _clanService.IsClanExistsAsync(clanName);
            if (!isClanExist)
            {
                throw new ClanNotFoundException(clanName);
            }
            var ninja = await EnforceNinjaExistenceAsync(clanName, ninjaKey);
            return ninja;
        }

        public async Task<Ninja> UpdateAsync(Ninja ninja)
        {
            await EnforceNinjaExistenceAsync(ninja.Clan.Name, ninja.Key);
            return await _ninjaRepository.UpdateAsync(ninja);
        }

        private async Task<Ninja> EnforceNinjaExistenceAsync(string clanName, string ninjaKey)
        {
            var remoteNinja = await _ninjaRepository.ReadOneAsync(clanName, ninjaKey);
            if (remoteNinja == null)
            {
                throw new NinjaNotFoundException(clanName, ninjaKey);
            }
            return remoteNinja;
        }
    }
}
