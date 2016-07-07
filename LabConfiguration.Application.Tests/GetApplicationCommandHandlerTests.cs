using System;
using LabConfiguration.Application.Commands;
using LabConfiguration.Application.Commands.Handlers;
using LabConfiguration.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LabConfiguration.Application.Tests
{
    [TestClass]
    public class GetApplicationCommandHandlerTests
    {
        [TestMethod]
        public void GetApplicationLoadsTheApplicationFromTheRepository()
        {
            Mock<IApplicationRepository> applicationRepository = new Mock<IApplicationRepository>();
            
            var handler = new GetApplicationCommandHandler(applicationRepository.Object);

            handler.Handle(new GetApplicationCommand(12050));

            applicationRepository.Verify(x => x.GetByCode(12050));
        }

        [TestMethod]
        public void IfTheApplicationNotExistsAnErrorIsReturned()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void IfTheRepositoryFailsAnErrorIsReturned()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TheApplicationPayloadIsReturnedAsPartOfTheResponse()
        {
            Assert.Inconclusive();
        }
    }
}
