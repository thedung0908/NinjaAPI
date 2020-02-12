using Microsoft.AspNetCore.Mvc;
using Moq;
using NinjaAPI.Controllers;
using NinjaAPI.Models;
using NinjaAPI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NinjaAPI.Tests.Controllers
{
    public class NinjaControllerTest
    {
        protected NinjaController ControllerUnderTest { get; }
        protected Mock<INinjaService> NinjaServiceMock { get; }

        public NinjaControllerTest()
        {
            NinjaServiceMock = new Mock<INinjaService>();
            ControllerUnderTest = new NinjaController(NinjaServiceMock.Object);
        }
        public class ReadAllAsync : NinjaControllerTest
        {
            [Fact]
            public async void ShouldReturnOKObjectResultWithAllNinja()
            {
                //Arrange
                var expectedNinjas = new Ninja[]
                {
                    new Ninja { Name = "Test Ninja 1"},
                    new Ninja { Name = "Test Ninja 2"},
                    new Ninja { Name = "Test Ninja 3"}
                };
                NinjaServiceMock
                    .Setup(x => x.ReadAllAsync())
                    .ReturnsAsync(expectedNinjas);

                //Act
                var result = await ControllerUnderTest.ReadAllAsync();

                //Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Same(expectedNinjas, okResult.Value);

            }
        }
        public class ReadAllInClanAsync : NinjaControllerTest
        {
            [Fact]
            public async void ShouldReturnOKObjectResultWithAllNinjaInClan()
            {
                //Arrange
                var clanName = "Some clan name";
                var expectedNinjas = new Ninja[]
                {
                    new Ninja { Name = "Test Ninja 1"},
                    new Ninja { Name = "Test Ninja 2"},
                    new Ninja { Name = "Test Ninja 3"}
                };
                NinjaServiceMock
                    .Setup(x => x.ReadAllClanAsync(clanName))
                    .ReturnsAsync(expectedNinjas);

                //Act
                var result = await ControllerUnderTest.ReadAllClanAsync(clanName);

                //Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Same(expectedNinjas, okResult.Value);
            }
            [Fact]
            public async void ShouldReturnNotFoundResultWhenClanNotFoundExceptionIsThrown()
            {
                //Arrange
                var unexistingClanName = "Some clan name";
                NinjaServiceMock
                    .Setup(x => x.ReadAllClanAsync(unexistingClanName))
                    .ThrowsAsync(new ClanNotFoundException(unexistingClanName));

                //Act
                var result = await ControllerUnderTest.ReadAllClanAsync(unexistingClanName);

                //Assert
                Assert.IsType<NotFoundResult>(result);
            }
        }
        public class ReadOneAsync : NinjaControllerTest
        {
            [Fact]
            public async void ShoudReturnOkObjectResultWithANinja()
            {
                //Arrange
                var clanName = "Some clan name";
                var ninjaKey = "Some ninja key";
                var expectedNinja = new Ninja { Name = "Test ninja 1 " };
                NinjaServiceMock
                    .Setup(x => x.ReadOneAsync(clanName, ninjaKey))
                    .ReturnsAsync(expectedNinja);

                //Act
                var result = await ControllerUnderTest.ReadOneAsync(clanName, ninjaKey);

                //Assert 
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Same(expectedNinja, okResult.Value);
            }
            [Fact]
            public async void ShouldReturnNotFoundResultWhenNinjaNotFoundExceptionIsThrown()
            {
                //Arrange
                var clanName = "Some clan name";
                var ninjaKey = "Some ninja key";
                NinjaServiceMock
                    .Setup(x => x.ReadOneAsync(clanName, ninjaKey))
                    .ThrowsAsync(new NinjaNotFoundException(clanName, ninjaKey));

                //Act
                var result = await ControllerUnderTest.ReadOneAsync(clanName, ninjaKey);

                //Assert
                Assert.IsType<NotFoundResult>(result);
            }
        }
        public class CreateAsync : NinjaControllerTest
        {
            [Fact]
            public async void ShouldReturnCreatedAtActionResultWithTheCreatedNinja()
            {
                //Arrange
                var expectedNinjaKey = "SomeNinjaKey";
                var expectedClanName = "My Clan";
                var expectedCreatedAtActionName = nameof(NinjaController.ReadOneAsync);
                var expectedNinja = new Ninja { Name = "Test Ninja 1", Clan = new Clan { Name = expectedClanName } };
                NinjaServiceMock
                    .Setup(x => x.CreateAsync(expectedNinja))
                    .ReturnsAsync(() =>
                    {
                        expectedNinja.Key = expectedNinjaKey;
                        return expectedNinja;
                    });

                //Act
                var result = await ControllerUnderTest.CreateAsync(expectedNinja);

                //Assert
                var createdResult = Assert.IsType<CreatedAtActionResult>(result);
                Assert.Same(expectedNinja, createdResult.Value);
                Assert.Equal(expectedCreatedAtActionName, createdResult.ActionName);
                Assert.Equal(expectedNinjaKey, createdResult.RouteValues.GetValueOrDefault("key"));
                Assert.Equal(expectedClanName, createdResult.RouteValues.GetValueOrDefault("clan"));
            }
            [Fact] 
            public async void ShouldReturnBadRequestResult()
            {
                //Arrange
                var ninja = new Ninja { Name = "Test Ninja 1" };
                ControllerUnderTest.ModelState.AddModelError("Key", "Some error");

                //Act 
                var result = await ControllerUnderTest.CreateAsync(ninja);

                //Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.IsType<SerializableError>(badRequestResult.Value);
            }
        }
        public class UpdateAsync : NinjaControllerTest
        {
            [Fact]
            public async void ShouldReturnOkObjectResultWithTheUpdatedNinja()
            {
                //Arrange 
                var expectedNinja = new Ninja { Name = "Test Ninja 1" };
                NinjaServiceMock
                    .Setup(x => x.UpdateAsync(expectedNinja))
                    .ReturnsAsync(expectedNinja);

                //Act 
                var result = await ControllerUnderTest.UpdateAsync(expectedNinja);

                //Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Same(expectedNinja, okResult.Value);
            }
            [Fact]
            public async void Should_return_NotFoundResult_when_NinjaNotFoundException_is_thrown()
            {
                //Arrange 
                var unexistingNinja = new Ninja { Name = "Test Ninja 1", Clan = new Clan { Name = "Some Clan" }, Key = "SomeNinjaKey" };
                NinjaServiceMock
                    .Setup(x => x.UpdateAsync(unexistingNinja))
                    .ThrowsAsync(new NinjaNotFoundException(unexistingNinja));

                //Act 
                var result = await ControllerUnderTest.UpdateAsync(unexistingNinja);

                //Assert
                Assert.IsType<NotFoundResult>(result);
            }

            [Fact]
            public async void Should_return_BadRequestResult()
            {
                //Arrange
                var ninja = new Ninja { Name = "Test Ninja 1" };
                ControllerUnderTest.ModelState.AddModelError("Key", "Some error");

                //Act 
                var result = await ControllerUnderTest.UpdateAsync(ninja);

                //Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.IsType<SerializableError>(badRequestResult.Value);
            }
        }
        public class DeleteAsync : NinjaControllerTest
        {
            [Fact]
            public async void ShouldReturnOkObjectResultWithTheDeletedNinja()
            {
                //Arrange 
                var clanName = "Some clan name";
                var ninjaKey = "SomeNinjaKey";
                var expectedNinja = new Ninja { Name = "Test Ninja 1" };
                NinjaServiceMock
                    .Setup(x => x.DeleteAsync(clanName, ninjaKey))
                    .ReturnsAsync(expectedNinja);

                //Act 
                var result = await ControllerUnderTest.DeleteAsync(clanName, ninjaKey);

                //Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Same(expectedNinja, okResult.Value);
            }
            [Fact]
            public async void Should_return_NotFoundResult_when_NinjaNotFoundException_is_thrown()
            {
                //Arrange 
                var clanName = "Some clan name";
                var ninjaKey = "SomeNinjaKey";
                var unexistingNinja = new Ninja { Name = "Test Ninja 1", Clan = new Clan { Name = clanName }, Key = ninjaKey };
                NinjaServiceMock
                    .Setup(x => x.DeleteAsync(clanName, ninjaKey))
                    .ThrowsAsync(new NinjaNotFoundException(unexistingNinja));

                //Act 
                var result = await ControllerUnderTest.DeleteAsync(clanName, ninjaKey);

                //Assert
                Assert.IsType<NotFoundResult>(result);
            }

            [Fact]
            public async void Should_return_BadRequestResult()
            {
                //Arrange
                var clanName = "Some clan name";
                var ninjaKey = "SomeNinjaKey";
                ControllerUnderTest.ModelState.AddModelError("Key", "Some error");

                //Act 
                var result = await ControllerUnderTest.DeleteAsync(clanName, ninjaKey);

                //Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.IsType<SerializableError>(badRequestResult.Value);
            }
        }
    }
}
