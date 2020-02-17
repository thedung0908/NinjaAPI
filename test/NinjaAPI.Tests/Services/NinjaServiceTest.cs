using Moq;
using NinjaAPI.Models;
using NinjaAPI.Repositories;
using NinjaAPI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NinjaAPI.Tests.Services
{
    public class NinjaServiceTest
    {
        protected NinjaService ServiceUnderTest { get; }
        protected Mock<INinjaRepository> NinjaRepositoryMock { get; }
        protected Mock<IClanService> ClanServiceMock { get; }
        public NinjaServiceTest()
        {
            NinjaRepositoryMock = new Mock<INinjaRepository>();
            ClanServiceMock = new Mock<IClanService>();
            ServiceUnderTest = new NinjaService(NinjaRepositoryMock.Object, ClanServiceMock.Object);
        }

        public class ReadAllAsync : NinjaServiceTest
        {
            [Fact]
            public async void ShouldReturnAllNinjas()
            {
                //Arrange
                var expectedNinjas = new Ninja[]
                {
                    new Ninja { Name = "Test Ninja 1" },
                    new Ninja { Name = "Test Ninja 2" },
                    new Ninja { Name = "Test Ninja 3" }
                };
                NinjaRepositoryMock
                    .Setup(x => x.ReadAllAsync())
                    .ReturnsAsync(expectedNinjas);

                //Act
                var result = await ServiceUnderTest.ReadAllAsync();

                //Assert
                Assert.Same(expectedNinjas, result);
            }
        }
        public class ReadAllClanAsync : NinjaServiceTest
        {
            [Fact]
            public async void ShouldReturnAllNinjasInClan()
            {
                //Arrange
                var clanName = "Some clan name";
                var expectedNinjas = new Ninja[]
                {
                    new Ninja { Name = "Test Ninja 1" },
                    new Ninja { Name = "Test Ninja 2" },
                    new Ninja { Name = "Test Ninja 3" }
                };
                NinjaRepositoryMock
                    .Setup(x => x.ReadAllClanAsync(clanName))
                    .ReturnsAsync(expectedNinjas)
                    .Verifiable();
                ClanServiceMock
                    .Setup(x => x.IsClanExistsAsync(clanName))
                    .ReturnsAsync(true)
                    .Verifiable();

                //Act
                var result = await ServiceUnderTest.ReadAllClanAsync(clanName);

                //Assert
                Assert.Same(expectedNinjas, result);
                NinjaRepositoryMock
                    .Verify(x => x.ReadAllClanAsync(clanName), Times.Once);
                ClanServiceMock
                    .Verify(x => x.IsClanExistsAsync(clanName), Times.Once);
            }

            [Fact]
            public async void ShouldThrowClanNotFoundExceptionWhenClanDoesNotExist()
            {
                //Arrange
                var unexistingClanName = "Some clan name";
                NinjaRepositoryMock
                    .Setup(x => x.ReadAllClanAsync(unexistingClanName))
                    .Verifiable();
                ClanServiceMock
                    .Setup(x => x.IsClanExistsAsync(unexistingClanName))
                    .ReturnsAsync(false)
                    .Verifiable();

                //Act & Assert 
                await Assert.ThrowsAsync<ClanNotFoundException>(() => ServiceUnderTest.ReadAllClanAsync(unexistingClanName));
                NinjaRepositoryMock.Verify(x => x.ReadAllClanAsync(unexistingClanName), Times.Never);
                ClanServiceMock.Verify(x => x.IsClanExistsAsync(unexistingClanName), Times.Once);
            }
        }

        public class ReadOnceAsync : NinjaServiceTest
        {
            [Fact]
            public async void ShouldReturnExpectedNinjaInClan()
            {
                //Arrange
                var clanName = "Some clan name";
                var ninjaKey = "SomeNinjaKey";
                var expectedNinja = new Ninja { Name = "Test Ninja 1", Clan = new Clan { Name = clanName }, Key = ninjaKey };
                NinjaRepositoryMock
                    .Setup(x => x.ReadOneAsync(clanName, ninjaKey))
                    .ReturnsAsync(expectedNinja)
                    .Verifiable();
                ClanServiceMock
                    .Setup(x => x.IsClanExistsAsync(clanName))
                    .ReturnsAsync(true)
                    .Verifiable();

                //Act
                var result = await ServiceUnderTest.ReadOneAsync(clanName, ninjaKey);

                //Assert
                Assert.Same(expectedNinja, result);
                NinjaRepositoryMock.Verify(x => x.ReadOneAsync(clanName, ninjaKey), Times.Once);
                ClanServiceMock.Verify(x => x.IsClanExistsAsync(clanName), Times.Once);
            }
            [Fact]
            public async void ShouldThrowClanNotFoundExceptionWhenClanDoesNotExist()
            {
                //Arrange
                var unexistingClanName = "Some clan name";
                var ninjaKey = "SomeNinjaKey";
                NinjaRepositoryMock
                    .Setup(x => x.ReadOneAsync(unexistingClanName, ninjaKey))
                    .Verifiable();
                ClanServiceMock
                    .Setup(x => x.IsClanExistsAsync(unexistingClanName))
                    .ReturnsAsync(false)
                    .Verifiable();

                //Act & Assert 
                await Assert.ThrowsAsync<ClanNotFoundException>(() => ServiceUnderTest.ReadOneAsync(unexistingClanName, ninjaKey));
                NinjaRepositoryMock.Verify(x => x.ReadOneAsync(unexistingClanName, ninjaKey), Times.Never);
                ClanServiceMock.Verify(x => x.IsClanExistsAsync(unexistingClanName), Times.Once);
            }
            [Fact]
            public async void ShouldThrowNinjaNotFoundExceptionWhenNinjaDoesNotExist()
            {
                //Arrange
                var clanName = "Some clan name";
                var ninjaKey = "SomeNinjaKey";
                NinjaRepositoryMock
                    .Setup(x => x.ReadOneAsync(clanName, ninjaKey))
                    .ThrowsAsync(new NinjaNotFoundException(clanName, ninjaKey))
                    .Verifiable();
                ClanServiceMock
                    .Setup(x => x.IsClanExistsAsync(clanName))
                    .ReturnsAsync(true)
                    .Verifiable();

                //Act & Assert 
                await Assert.ThrowsAsync<NinjaNotFoundException>(() => ServiceUnderTest.ReadOneAsync(clanName, ninjaKey));
                NinjaRepositoryMock.Verify(x => x.ReadOneAsync(clanName, ninjaKey), Times.Once);
                ClanServiceMock.Verify(x => x.IsClanExistsAsync(clanName), Times.Once);
            }
        }
        public class CreateAsync : NinjaServiceTest
        {
            [Fact]
            public async void ShouldCreateAndReturnTheCreatedNinja()
            {
                //Arrange
                var clanName = "Some clan name";
                var expectedNinja = new Ninja { Name = "Test Ninja 1", Clan = new Clan { Name = clanName } };
                ClanServiceMock
                    .Setup(x => x.IsClanExistsAsync(expectedNinja.Clan.Name))
                    .ReturnsAsync(true)
                    .Verifiable();
                NinjaRepositoryMock
                    .Setup(x => x.CreateAsync(expectedNinja))
                    .ReturnsAsync(expectedNinja)
                    .Verifiable();

                //Act
                var result = await ServiceUnderTest.CreateAsync(expectedNinja);

                //Assert
                Assert.Same(expectedNinja, result);
                ClanServiceMock.Verify(x => x.IsClanExistsAsync(clanName), Times.Once);
                NinjaRepositoryMock.Verify(x => x.CreateAsync(expectedNinja), Times.Once);
            }
            [Fact]
            public async void ShouldThrowClanNotFoundExceptionWhenClanDoesNotExist()
            {
                //Arrange
                var clanName = "Some clan name";
                var expectedNinja = new Ninja { Name = "Test Ninja 1", Clan = new Clan { Name = clanName } };
                ClanServiceMock
                    .Setup(x => x.IsClanExistsAsync(expectedNinja.Clan.Name))
                    .ReturnsAsync(false)
                    .Verifiable();
                NinjaRepositoryMock
                    .Setup(x => x.CreateAsync(expectedNinja))
                    .Verifiable();

                //Act & Assert
                await Assert.ThrowsAsync<ClanNotFoundException>(() => ServiceUnderTest.CreateAsync(expectedNinja));

                ClanServiceMock.Verify(x => x.IsClanExistsAsync(clanName), Times.Once);
                NinjaRepositoryMock.Verify(x => x.CreateAsync(expectedNinja), Times.Never);
            }
        }
        public class UpdateAsync : NinjaServiceTest
        {
            [Fact]
            public async void ShouldUpdateAndReturnTheUpdatedNinja()
            {
                //Arrange
                var clanName = "Some clan name";
                var ninjaKey = "SomeNinjaKey";
                var expectedNinja = new Ninja { Clan = new Clan { Name = clanName }, Key = ninjaKey };
                ClanServiceMock
                    .Setup(x => x.IsClanExistsAsync(clanName))
                    .ReturnsAsync(true);
                NinjaRepositoryMock
                    .Setup(x => x.ReadOneAsync(clanName, ninjaKey))
                    .ReturnsAsync(expectedNinja)
                    .Verifiable();
                NinjaRepositoryMock
                    .Setup(x => x.UpdateAsync(expectedNinja))
                    .ReturnsAsync(expectedNinja)
                    .Verifiable();

                //Act
                var result = await ServiceUnderTest.UpdateAsync(expectedNinja);

                //Assert
                Assert.Same(expectedNinja, result);
                NinjaRepositoryMock.Verify(x => x.ReadOneAsync(clanName, ninjaKey), Times.Once);
                NinjaRepositoryMock.Verify(x => x.UpdateAsync(expectedNinja), Times.Once);
            }
            [Fact]
            public async void ShouldThrowNinjaNotFoundExceptionWhenNinjaDoesNotExist()
            {
                //Arrange
                var clanName = "Some clan name";
                var ninjaKey = "SomeNinjaKey";
                var expectedNinja = new Ninja { Clan = new Clan { Name = clanName }, Key = ninjaKey };
                NinjaRepositoryMock
                    .Setup(x => x.ReadOneAsync(clanName, ninjaKey))
                    .ThrowsAsync(new NinjaNotFoundException(clanName, ninjaKey))
                    .Verifiable();
                NinjaRepositoryMock
                    .Setup(x => x.DeleteAsync(clanName, ninjaKey))
                    .Verifiable();

                //Act & Assert
                await Assert.ThrowsAsync<NinjaNotFoundException>(() => ServiceUnderTest.UpdateAsync(expectedNinja));
                NinjaRepositoryMock.Verify(x => x.ReadOneAsync(clanName, ninjaKey), Times.Once);
                NinjaRepositoryMock.Verify(x => x.UpdateAsync(expectedNinja), Times.Never);
            }
        }
        public class DeleteAsync : NinjaServiceTest
        {
            [Fact]
            public async void ShouldDeleteAndReturnTheDeletedNinja()
            {
                //Arrange
                var clanName = "Some clan name";
                var ninjaKey = "SomeNinjaKey";
                var expectedNinja = new Ninja();
                ClanServiceMock
                    .Setup(x => x.IsClanExistsAsync(clanName))
                    .ReturnsAsync(true);
                NinjaRepositoryMock
                    .Setup(x => x.ReadOneAsync(clanName, ninjaKey))
                    .ReturnsAsync(expectedNinja)
                    .Verifiable();
                NinjaRepositoryMock
                    .Setup(x => x.DeleteAsync(clanName, ninjaKey))
                    .ReturnsAsync(expectedNinja)
                    .Verifiable();

                //Act
                var result = await ServiceUnderTest.DeleteAsync(clanName, ninjaKey);

                //Assert
                Assert.Same(expectedNinja, result);
                NinjaRepositoryMock.Verify(x => x.ReadOneAsync(clanName, ninjaKey), Times.Once);
                NinjaRepositoryMock.Verify(x => x.DeleteAsync(clanName, ninjaKey), Times.Once);
            }
            [Fact]
            public async void ShouldThrowNinjaNotFoundExceptionWhenNinjaDoesNotExist()
            {
                //Arrange
                var clanName = "Some clan name";
                var ninjaKey = "SomeNinjaKey";
                NinjaRepositoryMock
                    .Setup(x => x.ReadOneAsync(clanName, ninjaKey))
                    .ThrowsAsync(new NinjaNotFoundException(clanName, ninjaKey))
                    .Verifiable();
                NinjaRepositoryMock
                    .Setup(x => x.DeleteAsync(clanName, ninjaKey))
                    .Verifiable();

                //Act & Assert
                await Assert.ThrowsAsync<NinjaNotFoundException>(() => ServiceUnderTest.DeleteAsync(clanName, ninjaKey));
                NinjaRepositoryMock.Verify(x => x.ReadOneAsync(clanName, ninjaKey), Times.Once);
                NinjaRepositoryMock.Verify(x => x.DeleteAsync(clanName, ninjaKey), Times.Never);
            }

        }
    }
}
