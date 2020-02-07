using NinjaAPI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Threading.Tasks;
using NinjaAPI.Models;
using System.Collections.ObjectModel;
using NinjaAPI.Repositories;
using Moq;

namespace NinjaAPI.Tests.Services
{
    public class ClanServiceTest
    {
        protected ClanService ServiceUnderTest { get; }
        protected Mock<IClanRepository> ClanRepositoryMock { get; }
        public ClanServiceTest()
        {
            ClanRepositoryMock = new Mock<IClanRepository>();
            ServiceUnderTest = new ClanService(ClanRepositoryMock.Object);
        }

        public class ReadAllAsync : ClanServiceTest
        {
            [Fact]
            public async Task ShouldReturnAllClans()
            {
                // Arrange
                var expectedClans = new ReadOnlyCollection<Clan>(new List<Clan>
                {
                    new Clan { Name = "My Clan"},
                    new Clan { Name = "Your Clan"},
                    new Clan { Name = "His Clan"}
                });
                ClanRepositoryMock
                    .Setup(x => x.ReadAllAsync())
                    .ReturnsAsync(expectedClans);

                // Act
                var result = await ServiceUnderTest.ReadAllAsync();

                // Assert
                Assert.Same(expectedClans, result);

            }
        }

        public class ReadOneAsync : ClanServiceTest
        {
            [Fact]
            public async Task ShouldReturnTheExpectedClan()
            {
                //Arrange
                string clanName = "My Clan";
                var expectedClan = new Clan { Name = clanName };
                ClanRepositoryMock
                    .Setup(x => x.ReadOneAsync(clanName))
                    .ReturnsAsync(expectedClan);

                //Act
                var result = await ServiceUnderTest.ReadOneAsync(clanName);

                //Assert
                Assert.Same(expectedClan, result);
            }

            [Fact]
            public async Task ShouldReturnNullIfClanDoesNotExist()
            {
                //Arrange
                string clanName = "My Clan";
                ClanRepositoryMock
                    .Setup(x => x.ReadOneAsync(clanName))
                    .ReturnsAsync(default(Clan));

                //Act
                var result = await ServiceUnderTest.ReadOneAsync(clanName);

                //Assert
                Assert.Null(result);
            }
        }

        public class IsClanExistsAsync : ClanServiceTest
        {
            [Fact]
            public async Task ShouldReturnTrueIfClanExists()
            {
                //Arrange
                string clanName = "My Clan";
                var expectedClan = new Clan { Name = clanName }; 
                ClanRepositoryMock
                    .Setup(x => x.ReadOneAsync(clanName))
                    .ReturnsAsync(expectedClan);

                //Act
                var result = await ServiceUnderTest.IsClanExistsAsync(clanName);

                //Assert
                Assert.True(result);
            }

            [Fact]
            public async Task ShouldReturnFalseIfClanDoesNotExist()
            {
                //Arrange
                string clanName = "My Clan";
                ClanRepositoryMock
                    .Setup(x => x.ReadOneAsync(clanName))
                    .ReturnsAsync(default(Clan));

                //Act
                var result = await ServiceUnderTest.IsClanExistsAsync(clanName);

                //Assert
                Assert.False(result);
            }
        }

        public class CreateAsync : ClanServiceTest
        {
            [Fact]
            public async Task ShouldCreateAndReturnTheSpecifiedClan()
            {
                // Arrange, Act, Assert
                var exception = await Assert.ThrowsAsync<NotSupportedException>(() => ServiceUnderTest.CreateAsync(null));

            }
        }

        public class UpdateAsync : ClanServiceTest
        {
            [Fact]
            public async Task ShouldUpdateAndReturnTheSpecifiedClan()
            {
                // Arrange, Act, Assert
                var exception = await Assert.ThrowsAsync<NotSupportedException>(() => ServiceUnderTest.UpdateAsync(null));

            }
        }

        public class DeleteAsync : ClanServiceTest
        {
            [Fact]
            public async Task ShouldDeleteAndReturnTheSpecifiedClan()
            {
                // Arrange, Act, Assert
                var exception = await Assert.ThrowsAsync<NotSupportedException>(() => ServiceUnderTest.DeleteAsync(null));

            }
        }
    }
}
