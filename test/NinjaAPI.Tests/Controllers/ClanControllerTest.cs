using Microsoft.AspNetCore.Mvc;
using NinjaAPI.Controllers;
using NinjaAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NinjaAPI.Tests.Controllers
{
    class ClanControllerTest
    {
        protected ClanController ControllerUnderTest { get; }
        public ClanControllerTest ()
        {
            ControllerUnderTest = new ClanController();
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

                //Act
                var result = await ControllerUnderTest.ReadAllAsync();

                //Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Same(expectedClans, okResult.Value);
            }
        }
    }
}
