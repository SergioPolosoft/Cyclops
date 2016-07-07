using System;
using ApplicationServices;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCConfiguration.Application.Commands;
using QCConfiguration.Application.Commands.Handlers;
using QCConfiguration.Domain;
using QCConfiguration.Domain.Repositories;

namespace QCConfiguration.Application.Tests
{
    [TestClass]
    public class GetQualityControlHandlerTests
    {
        private Mock<IQualityControlRepository> repository;
        private GetQualityControlHandler handler;
        private const int TestCode = 17280;

        [TestInitialize]
        public void TestInitialize()
        {
            repository = new Mock<IQualityControlRepository>();
            handler = new GetQualityControlHandler(repository.Object);
        }

        [TestMethod]
        public void IfTheQualityControlNotExistsAnErrorIsReturned()
        {
            repository.Setup(x => x.GetQualityControlForTestCode(TestCode)).Returns(() => null);

            var response = handler.Handle(new GetQualityControl(TestCode));

            response.Status.Should().Be(CommandResult.Error);
            response.Message.Should().Be(string.Format(QCConfigurationMessages.QCNotExists, TestCode));
        }

        [TestMethod]
        public void IfTheRepositoryFailsAnErrorIsReturned()
        {
            repository.Setup(x => x.GetQualityControlForTestCode(TestCode)).Throws(new Exception());

            var response = handler.Handle(new GetQualityControl(TestCode));

            response.Status.Should().Be(CommandResult.Error);
            response.Message.Should().Be(string.Format(QCConfigurationMessages.ErrorReadingQCRepository, TestCode));
        }

        [TestMethod]
        public void TheQualityControlPayloadIsReturnedAsPartOfTheResponse()
        {
            var qualityControl = new QualityControl();
            qualityControl.Create(TestCode,14.54,0.607);

            repository.Setup(x => x.GetQualityControlForTestCode(TestCode)).Returns(qualityControl);

            var response = handler.Handle(new GetQualityControl(TestCode));

            response.Status.Should().Be(CommandResult.Success);
            response.QualityControl.Should().NotBeNull();
            response.QualityControl.TestCode.Should().Be(TestCode);
            response.QualityControl.TargetValue.Should().Be(qualityControl.TargetValue);
            response.QualityControl.StandardDeviation.Should().Be(qualityControl.StandardDeviation);

        }        
    }
}
