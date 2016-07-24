using System;
using LabConfiguration.Application.Commands;
using LabConfiguration.Application.Commands.Handlers;
using LabConfiguration.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCEvaluation.Application;
using QCEvaluation.Application.Events;

namespace LabConfiguration.Application.Tests
{
    [TestClass]
    public class ConfirmApplicationInstallationTests
    {
        [TestMethod]
        public void WhenApplicationStoredTheEventIsSentToQCEvaluation()
        {
            var applicationRepository = new Mock<IApplicationRepository>();

            var qcEvaluationServices = new Mock<IQCEvaluationPort>();

            var handler = new ConfirmApplicationInstallationHandler(applicationRepository.Object, qcEvaluationServices.Object);

            var testCode = 17142;

            handler.Handle(new ConfirmApplicationInstallationCommand(new ApplicationDTO(testCode)));

            qcEvaluationServices.Verify(x => x.NotifyApplicationInstalled(testCode));
        }
    }
}
