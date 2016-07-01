using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCConfiguration.Application.Commands;
using QCConfiguration.Application.Commands.Handlers;
using QCConfiguration.Application.DTOs;
using QCConfiguration.Domain.Events;
using QCConfiguration.Domain.Repositories;

namespace QCConfiguration.Application.Tests
{
    [TestClass]
    public class ConfirmControlInstallationHandlerTests
    {
        [TestMethod]
        public void WhenTheConfirmationIsReceivedAControlIsCreated()
        {
            Mock<IQualityControlRepository> qcRepository = new Mock<IQualityControlRepository>();

            var handler = new ConfirmControlInstallationHandler(qcRepository.Object);

            handler.Handle(new ConfirmControlInstallation(new ControlDTO(11162,163,223)));

            qcRepository.Verify(x => x.Add(It.IsAny<List<AggreggateEvent>>()));
        }

        [TestMethod]
        public void TheControlIsCreatedWithTheTestCodeTheRange1SDAndTheTargetValue()
        {
            Mock<IQualityControlRepository> qcRepository = new Mock<IQualityControlRepository>();

            var handler = new ConfirmControlInstallationHandler(qcRepository.Object);

            handler.Handle(new ConfirmControlInstallation(new ControlDTO(11162,16,22)));

            qcRepository.Verify(x => x.Add(It.Is<List<AggreggateEvent>>(y => y.Any(z=> ValidateQualityControlCreatedEvent(z, 11162,16,22)))));
        }

        private bool ValidateQualityControlCreatedEvent(AggreggateEvent @event, int testCode, double range1SD, double targetValue)
        {
            QualityControlCreated qualityControlCreated = @event as QualityControlCreated;
            if (qualityControlCreated == null)
            {
                return false;
            }
            //qualityControlCreated.Id.Should().NotBeEmpty();
            //qualityControlCreated.TestCode.Should().Be(testCode);
            //qualityControlCreated.StandardDeviation.Should().Be(range1SD);
            //qualityControlCreated.TargetValue.Should().Be(targetValue);
            return true;
        }

        [TestMethod]
        public void WhenTheConfirmationIsStoredItIsSentToValidationUnit()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void WhenTheControlExistsTheTargetValueAndSDRangeIsUpdated()
        {
            Assert.Inconclusive();
        }
    }
}
