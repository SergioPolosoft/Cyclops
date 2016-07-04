using System;
using ApplicationServices;
using FluentAssertions;
using InstrumentCommunication.Application.Commands;
using LabConfiguration.Application.Commands.Handlers;
using LabConfiguration.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LabConfiguration.Application.Tests
{
    [TestClass]
    public class GetFTPConfigurationHandlerTests
    {
        private Mock<IConfigurationRepository> repository;
        private GetFTPConfigurationHandler handler;

        [TestInitialize]
        public void TestInitialize()
        {
            repository = new Mock<IConfigurationRepository>();
            handler = new GetFTPConfigurationHandler(repository.Object);
        }

        [TestMethod]
        public void WhenHandlerIsCalledTheRepositoryIsQueried()
        {
            handler.Handle(new GetFTPConfiguration());

            repository.Verify(x=>x.GetFTPConfiguration());
        }

        [TestMethod]
        public void TheResponseContainsTheFTPConfiguration()
        {
            var ftpConfiguration = new FTPConfiguration {DataBasket = "DataBasket", Password = "Password", User = "User"};

            repository.Setup(x => x.GetFTPConfiguration()).Returns(ftpConfiguration);

            var result = handler.Handle(new GetFTPConfiguration());

            result.Status.Should().Be(CommandResult.Success);
            result.FTPConfiguration.User.Should().Be(ftpConfiguration.User);
            result.FTPConfiguration.DataBasket.Should().Be(ftpConfiguration.DataBasket);
            result.FTPConfiguration.Password.Should().Be(ftpConfiguration.Password);
        }

        [TestMethod]
        public void WhenRepositoryFailsAnErrorIsReturned()
        {
            Assert.Inconclusive();
        }
    }
}
