using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCEvaluation.Application.Events;
using QCEvaluation.Application.Events.Handlers;
using QCEvaluation.Domain;
using QCEvaluation.Domain.Events;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Application.Tests
{
    [TestClass]
    public class ApplicationInstalledTests
    {
        [TestMethod]
        public void WhenAnApplicationIsInstalledItIsStoredOnTheRepository()
        {
            var applicationRepository = new Mock<IQCApplicationRepository>();
            
            var handler = new ApplicationInstalledHandler(applicationRepository.Object);

            var applicationCode = 17302;

            handler.Handle(new ApplicationInstalled(applicationCode));
            
            applicationRepository.Verify(
                x => x.Save(It.Is<IList<AggreggateEvent>>(y=> ValidateApplicationCreated(y, applicationCode))));
        }

        private bool ValidateApplicationCreated(IList<AggreggateEvent> aggreggateEvents, int testCode)
        {
            return aggreggateEvents.OfType<ApplicationCreated>().Any(x => x.TestCode == testCode);
        }

        [TestMethod]
        public void IfTheApplicationExistsNothingIsDone()
        {
            var applicationRepository = new Mock<IQCApplicationRepository>();

            var applicationCode = 17302;

            applicationRepository.Setup(x => x.GetApplicationByTestCode(applicationCode))
                .Returns(new ApplicationQC(new ApplicationCreated(Guid.NewGuid(), applicationCode)));
            
            var handler = new ApplicationInstalledHandler(applicationRepository.Object);

            handler.Handle(new ApplicationInstalled(applicationCode));

            applicationRepository.Verify(
                x => x.Save(It.Is<IList<AggreggateEvent>>(y => y.Count==0)));
        }
    }
}
