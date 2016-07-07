using System;
using Application.Payloads;
using LabConfiguration.Application;
using LabConfiguration.Application.Commands;
using LabConfiguration.Application.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCConfiguration.Application;
using QCConfiguration.Application.Commands;
using QCConfiguration.Application.Responses;
using QCEvaluation.Application.Commands;
using QCEvaluation.Application.Commands.Handlers;
using QCEvaluation.Application.Events;
using QCEvaluation.Application.Events.Handlers;
using QCEvaluation.Domain;
using QCEvaluation.Domain.Events;
using QCEvaluation.Domain.Exceptions;
using QCEvaluation.Domain.Repositories;
using IQCResultsRepository = QCEvaluation.Domain.Repositories.IQCResultsRepository;
using QCResult = QCRoutine.Domain.QCResult;

namespace QCEvaluation.Application.Tests
{
    [TestClass]
    public class ValidateQCResultHandlerTests
    {
        private Mock<IQCApplicationRepository> applicationQCRepository;
        private QCResultReceivedHandler receivedHandler;
        private int testCode;
        private Guid applicationId;
        private Mock<ILabConfigurationServices> labConfigurationServices;
        private Mock<IEvaluationsRepository> evaluationsRepository;
        private Mock<IQCRuleRepository> qcruleRepository;
        private Mock<IQCResultsRepository> qcResultsRespository;
        private Mock<IQCConfigurationServices> qcConfigurationServices;

        [TestInitialize]
        public void TestInitialize()
        {
            applicationQCRepository = new Mock<IQCApplicationRepository>();
            labConfigurationServices = new Mock<ILabConfigurationServices>();
            evaluationsRepository = new Mock<IEvaluationsRepository>();
            qcruleRepository = new Mock<IQCRuleRepository>();

            applicationId = Guid.NewGuid();
            testCode = 16221;

            applicationQCRepository.Setup(x => x.GetApplicationByTestCode(testCode)).Returns(new ApplicationQC(new ApplicationCreated(applicationId,testCode)));

            labConfigurationServices.Setup(x => x.Handle(new GetApplicationCommand(testCode))).Returns(new GetApplicationResponse());

            qcResultsRespository = new Mock<IQCResultsRepository>();
            qcConfigurationServices = new Mock<IQCConfigurationServices>();

            var qualityControl = new QCConfiguration.Domain.QualityControl();
            qualityControl.Create(testCode,15.25,0.607);

            qcConfigurationServices.Setup(x => x.Handle(It.IsAny<GetQualityControl>()))
                .Returns(new GetQualityControlFound(new QualityControlPayload(qualityControl)));

            receivedHandler = new QCResultReceivedHandler(applicationQCRepository.Object, evaluationsRepository.Object, qcruleRepository.Object, qcResultsRespository.Object, qcConfigurationServices.Object);
        }

        [TestMethod]
        public void WhenTheQCResultIsReceiveTheApplicationQCIsLoaded()
        {
            var validateQcResult = new QCResultReceived(new QCResultPayload(new QCResult(testCode,17.39,DateTime.Now)));

            receivedHandler.Handle(validateQcResult);

            applicationQCRepository.Verify(x => x.GetApplicationByTestCode(testCode));
        }

        [TestMethod]
        public void WhenTheQCResultIsReceiveItIsStoredOnTheRepository()
        {
            var qcResultPayload = new QCResultPayload(new QCResult(testCode, 17.39, DateTime.Now));
            var validateQcResult = new QCResultReceived(qcResultPayload);

            receivedHandler.Handle(validateQcResult);

            qcResultsRespository.Verify(x => x.Add(It.Is<Domain.QCResult>(y => y.Id == qcResultPayload.Id && y.Result == qcResultPayload.Value && y.TestCode == qcResultPayload.TestCode)));
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationNotExistsException))]
        public void IfTheApplicationIsOnStateDeletedAnExceptionIsThrown()
        {
            var validateQcResult = new QCResultReceived(new QCResultPayload(new QCResult(testCode, 17.39, DateTime.Now)));

            var applicationQc = new ApplicationQC();
            applicationQc.Create(Guid.NewGuid(), testCode);
            applicationQc.Delete();

            applicationQCRepository.Setup(x => x.GetApplicationByTestCode(testCode)).Returns(applicationQc);

            receivedHandler.Handle(validateQcResult);

        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationNotExistsException))]
        public void IfTheApplicationQCDoesNotExistsAnExceptionIsThrown()
        {
            var validateQcResult = new QCResultReceived(new QCResultPayload(new QCResult(testCode, 17.39, DateTime.Now)));
           
            applicationQCRepository.Setup(x => x.GetApplicationByTestCode(testCode)).Returns(()=> null);

            receivedHandler.Handle(validateQcResult);
        }
        
        [TestMethod]
        public void WhenTheEvaluationIsPerformedAnEvaluationIsStored()
        {
            var validateQcResult = new QCResultReceived(new QCResultPayload(new QCResult(testCode,0.6,DateTime.Now)));

            receivedHandler.Handle(validateQcResult);

            evaluationsRepository.Verify(x => x.Add(It.IsAny<Evaluation>()));
        }

        [TestMethod]
        public void WhenTheResultIsReceivedTheQualityControlIsLoadedFromQCConfigurationService()
        {
            var validateQcResult = new QCResultReceived(new QCResultPayload(new QCResult(testCode, 0.6, DateTime.Now)));

            receivedHandler.Handle(validateQcResult);

            qcConfigurationServices.Verify(x=>x.Handle(It.Is<GetQualityControl>(y=>y.TestCode==testCode)));
        }

        [TestMethod]
        public void IfQualityControlIsNotFoundThenNoEvaluationIsStored()
        {
            qcConfigurationServices.Setup(x => x.Handle(It.IsAny<GetQualityControl>()))
                .Returns(new QualityControlNotFound(testCode));

            var validateQcResult = new QCResultReceived(new QCResultPayload(new QCResult(testCode, 0.6, DateTime.Now)));

            receivedHandler.Handle(validateQcResult);

            evaluationsRepository.Verify(x=>x.Add(It.Is<Evaluation>(y=>y.EvaluationResult==EvaluationResult.NotEvaluated)));
        }
    }
}
