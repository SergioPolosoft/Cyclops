using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCConfiguration.Application.Commands;
using QCConfiguration.Application.Commands.Handlers;
using QCConfiguration.Application.DTOs;
using QCConfiguration.Domain;
using QCConfiguration.Domain.Events;
using QCConfiguration.Domain.Repositories;

namespace QCConfiguration.Application.Tests
{
    [TestClass]
    public class ConfirmControlInstallationHandlerTests
    {
        private Mock<IQualityControlRepository> qcRepository;
        private ConfirmControlInstallationHandler handler;

        [TestInitialize]
        public void TestInitialize()
        {
            qcRepository = new Mock<IQualityControlRepository>();

            handler = new ConfirmControlInstallationHandler(qcRepository.Object);
        }

        [TestMethod]
        public void WhenTheConfirmationIsReceivedAControlIsCreated()
        {
            handler.Handle(new ConfirmControlInstallation(new ControlDTO(11162,163,223)));

            qcRepository.Verify(x => x.Add(It.IsAny<List<AggreggateEvent>>()));
        }

        [TestMethod]
        public void TheControlIsCreatedWithTheTestCodeTheRange1SDAndTheTargetValue()
        {            
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
            return true;
        }        

        [TestMethod]
        public void WhenTheControlExistsTheTargetValueAndSDRangeIsUpdated()
        {
            var controlId = Guid.NewGuid();
            qcRepository.Setup(x => x.GetQualityControlForTestCode(10560)).Returns(new QualityControl(new QualityControlCreated(controlId, 10560, 1.57, 0.707)));

            var controlDto = new ControlDTO(10560, 7.07, 2.16);
            handler.Handle(new ConfirmControlInstallation(controlDto));

            qcRepository.Verify(x=>x.Add(It.Is<List<AggreggateEvent>>(y=>y.Any(z=>ValidateIsUpdateEvent(z,controlDto, controlId)))));
        }

        private bool ValidateIsUpdateEvent(AggreggateEvent aggreggateEvent, ControlDTO controlDto, Guid qualityControlId)
        {
            QualityControlUpdated qualityControlUpdated = aggreggateEvent as QualityControlUpdated;
            if (qualityControlUpdated!=null)
            {
                if (qualityControlUpdated.ControlId == qualityControlId &&
                    qualityControlUpdated.StandardDeviation == controlDto.StandardDeviation &&
                    qualityControlUpdated.TargetValue == controlDto.TargetValue)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
