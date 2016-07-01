using System;
using LabConfiguration.Application;
using LabConfiguration.Application.Commands;
using LabConfiguration.Application.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCEvaluation.Application;
using QCEvaluation.Application.Commands;
using QCEvaluation.Application.Events;
using QCRoutine.Application.Commands;
using QCRoutine.Application.Commands.Handlers;
using QCRoutine.Domain;

namespace QCRoutine.Application.Tests
{
    [TestClass]
    public class StoreQCResultHandlerTests
    {
        private Mock<IQCEvaluationServices> qcConfigurationServices;
        private StoreQCResultHandler handler;
        private Mock<IQCResultsRepository> qcresultsRepository;
        private Mock<ILabConfigurationServices> labConfigurationServices;

        [TestInitialize]
        public void TestInitialize()
        {
            qcConfigurationServices = new Mock<IQCEvaluationServices>();
            qcresultsRepository = new Mock<IQCResultsRepository>();
            labConfigurationServices = new Mock<ILabConfigurationServices>();


            labConfigurationServices.Setup(x => x.Handle(It.IsAny<GetApplicationCommand>())).Returns(new GetApplicationResponse());
            
            handler = new StoreQCResultHandler(qcConfigurationServices.Object,qcresultsRepository.Object,labConfigurationServices.Object);
        }

        [TestMethod]
        public void WhenTheResultIsStoredItIsSentForValidation()
        {
            var testCode = 15170;
            var result = 9.62;
            
            handler.Handle(new StoreQCResult(testCode,result,DateTime.Now));
           
            qcConfigurationServices.Verify(x=>x.Handle(It.Is<QCResultReceived>(y=>y.QCResult.TestCode==testCode && y.QCResult.Value==result)));
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationNotExistsException))]
        public void IfTestCodeDoesNotExistsAnExceptionIsThrown()
        {
            labConfigurationServices.Setup(x => x.Handle(new GetApplicationCommand(15182))).Returns(new GetApplicationResponse());

            handler.Handle(new StoreQCResult(15182, 3.6, DateTime.Now));
        }

        [TestMethod]
        public void WhenTheResultIsReceiveItIsStoredOnRepository()
        {
            var testCode = 15151;
            double result = 4.6201;
            var measuredDateTime = DateTime.Now.AddMinutes(-1);

            handler.Handle(new StoreQCResult(testCode, result, measuredDateTime));
            
            qcresultsRepository.Verify(
                x => x.Add(
                        It.Is<QCResult>(
                            y =>
                            y.TestCode == testCode &&
                                       y.Result == result &&
                                       y.MeasuredTime == measuredDateTime
                            )));
        }
    }
}
