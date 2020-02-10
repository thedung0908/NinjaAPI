using Microsoft.Extensions.DependencyInjection;
using NinjaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Newtonsoft.Json;

namespace IntegrationTests
{
    public class ClanControllerTest : BaseHttpTest
    {
        public class ReadAllAsync : ClanControllerTest
        {
            private IEnumerable<Clan> Clans => new Clan[]
            {
                new Clan { Name = "My Clan" },
                new Clan { Name = "Your Clan" },
                new Clan { Name = "His Clan" }
            };

            protected override void ConfigureServices(IServiceCollection services)
            {
                services.AddSingleton(Clans);
            }

            [Fact]
            public async Task ShouldReturnTheDefaultClans()
            {
                //Arrange
                var expectedNumberofClans = Clans.Count();

                //Act
                var result = await Client.GetAsync("api/clan");

                //Assert 
                result.EnsureSuccessStatusCode();
                string jsonString = await result.Content.ReadAsStringAsync();
                var clans = JsonConvert.DeserializeObject<Clan[]>(jsonString);

                Assert.NotNull(clans);
                Assert.Equal(expectedNumberofClans, clans.Length);
                Assert.Collection(clans,
                    clan => Assert.Equal(Clans.ElementAt(0).Name, clans[0].Name),
                    clan => Assert.Equal(Clans.ElementAt(1).Name, clans[1].Name),
                    clan => Assert.Equal(Clans.ElementAt(2).Name, clans[2].Name)
                );
            }
        }
    }
}
