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
        public NinjaServiceTest()
        {
            NinjaRepositoryMock = new Mock<INinjaRepository>();
            ServiceUnderTest = new NinjaService(NinjaRepositoryMock.Object);
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
                    .ReturnsAsync(expectedNinjas);

                //Act
                var result = await ServiceUnderTest.ReadAllClanAsync(clanName);

                //Assert
                Assert.Same(expectedNinjas, result);
            }

            [Fact]
            public async void ShouldReturnNotFoundWhenClanNotFoundExceptionIsThrown()
            {
                //Arrange
                var unexistingClanName = "Some clan name";
                NinjaRepositoryMock
                    .Setup(x => x.ReadAllClanAsync(unexistingClanName))
                    .ThrowsAsync(new ClanNotFoundException(unexistingClanName));

                //Act
                var result = await ServiceUnderTest.ReadAllClanAsync(unexistingClanName);

                //Assert
                Assert.IsType<ClanNotFoundException>(result);
            }
        }
    }
}
