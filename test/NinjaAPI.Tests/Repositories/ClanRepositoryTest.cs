using NinjaAPI.Models;
using NinjaAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NinjaAPI.Tests.Repositories
{
    public class ClanRepositoryTest
    {
        protected ClanRepository RepositoryUnderTest { get; }
        protected Clan[] Clans { get; }

        public ClanRepositoryTest()
        {
            Clans = new Clan[]
            {
                new Clan { Name = "My Clan" },
                new Clan { Name = "Your Clan" },
                new Clan { Name = "His Clan" }
            };
            RepositoryUnderTest = new ClanRepository(Clans);
        }
        
        public class ReadAllAsync : ClanRepositoryTest
        {
            [Fact]
            public async Task ShouldReturnAllClans()
            {
                //Arrange
                //Act
                var result = await RepositoryUnderTest.ReadAllAsync();

                //Assert
                Assert.Collection(result,
                    clan => Assert.Same(Clans[0], clan),
                    clan => Assert.Same(Clans[1], clan),
                    clan => Assert.Same(Clans[2], clan)
                 );
            }
        }
        public class ReadOneAsync : ClanRepositoryTest
        {
            [Fact]
            public async Task ShouldReturnTheExpectedClan()
            {
                //Arrange
                var expectedClan = Clans[1];
                var expectedClanName = expectedClan.Name;
                

                //Act
                var result = await RepositoryUnderTest.ReadOneAsync(expectedClanName);

                //Assert
                Assert.Same(expectedClan, result);
            }

            [Fact]
            public async Task ShouldReturnNullIfTheClanDoesNotExist()
            {
                //Arrange
                string clanName = "Not Exist Clan";

                //Act
                var result = await RepositoryUnderTest.ReadOneAsync(clanName);

                //Assert
                Assert.Null(result);
            }
        }

        public class CreateAsync : ClanRepositoryTest
        {
            [Fact]
            public async Task ShouldCreateAndReturnTheSpecifiedClan()
            {
                var exception = await Assert.ThrowsAsync<NotSupportedException>(() => RepositoryUnderTest.CreateAsync(new Clan()));
            }
        }
        public class UpdateAsync : ClanRepositoryTest
        {
            [Fact]
            public async Task ShouldUpdateAndReturnTheSpecifiedClan()
            {
                var exception = await Assert.ThrowsAsync<NotSupportedException>(() => RepositoryUnderTest.UpdateAsync(new Clan()));
            }
        }
        public class DeleteAsync : ClanRepositoryTest
        {
            [Fact]
            public async Task ShouldDeleteAndReturnTheSpecifiedClan()
            {
                var exception = await Assert.ThrowsAsync<NotSupportedException>(() => RepositoryUnderTest.DeleteAsync("My Clan"));
            }
        }
    }
}
