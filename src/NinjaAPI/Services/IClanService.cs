using NinjaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaAPI.Services
{
    public interface IClanService
    {
        Task<IEnumerable<Clan>> ReadAllAsync();
        Task<Clan> ReadOneAsync(string clanName);
        Task<bool> IsClanExistsAsync(string clanName);
        Task<Clan> CreateAsync(Clan clan);
        Task<Clan> UpdateAsync(Clan clan);
        Task<Clan> DeleteAsync(string clanName);
    }
}
