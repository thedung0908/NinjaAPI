using NinjaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NinjaAPI
{
    public class NinjaApiException : Exception
    {
        public NinjaApiException()
        {

        }
        public NinjaApiException(string message) : base(message)
        {
                
        }
        public NinjaApiException(string message, Exception innerException) : base(message, innerException)
        {

        }
        public NinjaApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }

    public class ClanNotFoundException : NinjaApiException
    {
        public ClanNotFoundException(Clan clan) 
            : this(clan.Name)
        {

        }
        public ClanNotFoundException(string clanName) : base($"Clan {clanName} was not found.")
        {

        }
    }
    public class NinjaNotFoundException : NinjaApiException
    {
        public NinjaNotFoundException(Ninja ninja) : base($"Ninja {ninja.Name} ({ninja.Key}) of clan {ninja.Clan.Name} was not found.")
        {
        }
        public NinjaNotFoundException(string clanName, string ninjaKey) : base($"Ninja {ninjaKey} of clan {clanName} was not found.")
        {

        }
    }
}
