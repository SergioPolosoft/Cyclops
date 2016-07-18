using System;
using Application.Payloads;
using ApplicationServices;
using FluentAssertions;
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
        private const int ApplicationTestCode = 12050;
        private Mock<IApplicationRepository> applicationRepository;
        private GetApplicationCommandHandler handler;

        [TestInitialize]
        public void TestInitialize()
        {
            applicationRepository = new Mock<IApplicationRepository>();

            handler = new GetApplicationCommandHandler(applicationRepository.Object);
        }

        [TestMethod]
        public void GetApplicationLoadsTheApplicationFromTheRepository()
        {
            handler.Handle(new GetApplicationCommand(ApplicationTestCode));

            applicationRepository.Verify(x => x.GetByCode(ApplicationTestCode));
        }

        [TestMethod]
        public void IfTheApplicationNotExistsAnErrorIsReturned()
        {
            applicationRepository.Setup(x => x.GetByCode(ApplicationTestCode)).Returns(() => null);

            var response = handler.Handle(new GetApplicationCommand(ApplicationTestCode));

            response.Status.Should().Be(CommandResult.Error);
            response.Message.Should().Be(string.Format(LabConfigurationMessages.ApplicationNotFound,ApplicationTestCode));
        }

        [TestMethod]
        public void IfTheRepositoryFailsAnErrorIsReturned()
        {
            applicationRepository.Setup(x => x.GetByCode(ApplicationTestCode)).Throws(new Exception());

            var response = handler.Handle(new GetApplicationCommand(ApplicationTestCode));

            response.Status.Should().Be(CommandResult.Error);
            response.Message.Should().Be(string.Format(LabConfigurationMessages.ErrorReadingApplication, ApplicationTestCode));
        }

        [TestMethod]
        public void TheApplicationPayloadIsReturnedAsPartOfTheResponse()
        {
            applicationRepository.Setup(x => x.GetByCode(ApplicationTestCode))
                .Returns(new Domain.ApplicationTest(ApplicationTestCode));
            
            var response = handler.Handle(new GetApplicationCommand(ApplicationTestCode));

            response.Status.Should().Be(CommandResult.Success);
            response.Application.Should().BeOfType<ApplicationPayload>();
            response.Application.TestCode.Should().Be(ApplicationTestCode);
        }
    }
}
