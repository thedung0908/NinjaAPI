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
    public class ClanControllerTest
    {
        protected ClanController ControllerUnderTest { get; }
        protected Mock<IClanService> ClanServiceMock { get; }

        public ClanControllerTest ()
        {
            ClanServiceMock = new Mock<IClanService>();
            ControllerUnderTest = new ClanController(ClanServiceMock.Object);
        }
        
        public class ReadAllAsync : ClanControllerTest
        {
            [Fact]
            public async void ShouldReturnOKObjectResultWithClan()
            {
                //Arrange
                var expectedClans = new Clan[]
                {
                    new Clan{ Name = "Test Clan 1"},
                    new Clan{ Name = "Test Clan 2"},
                    new Clan{ Name = "Test Clan 3"}
                };
                ClanServiceMock
                    .Setup(x => x.ReadAllAsync())
                    .ReturnsAsync(expectedClans);
                
                //Act
                var result = await ControllerUnderTest.ReadAllAsync();

                //Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Same(expectedClans, okResult.Value);
            }
        }
    }
}
