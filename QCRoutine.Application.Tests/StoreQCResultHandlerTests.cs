using System;
using Application.Payloads;
using ApplicationServices;
using FluentAssertions;
using LabConfiguration.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCRoutine.Application.Commands;
using QCRoutine.Application.Commands.Handlers;
using QCRoutine.Application.Ports;
using QCRoutine.Application.Responses;
using QCRoutine.Domain;
using IQCEvaluationPort = QCRoutine.Application.Ports.IQCEvaluationPort;

namespace QCRoutine.Application.Tests
{
    [TestClass]
    public class StoreQCResultHandlerTests
    {
        private Mock<IQCEvaluationPort> qcConfigurationServices;
        private StoreQCResultHandler handler;
        private Mock<IQCResultsRepository> qcresultsRepository;
        private Mock<ILabConfigurationPort> labConfigurationServices;

        [TestInitialize]
        public void TestInitialize()
        {
            qcConfigurationServices = new Mock<IQCEvaluationPort>();
            qcresultsRepository = new Mock<IQCResultsRepository>();
            labConfigurationServices = new Mock<ILabConfigurationPort>();


            labConfigurationServices.Setup(x => x.GetApplicationByTestCode(It.IsAny<int>())).Returns(new ApplicationPayload(new ApplicationTest(10562)));
            
            handler = new StoreQCResultHandler(qcConfigurationServices.Object,qcresultsRepository.Object,labConfigurationServices.Object);
        }

        [TestMethod]
        public void WhenTheResultIsStoredItIsSentForValidation()
        {
            var testCode = 15170;
            var result = 9.62;
            
            handler.Handle(new StoreQCResult(testCode,result,DateTime.Now));
           
            qcConfigurationServices.Verify(x=>x.NotifyResultReceived(It.Is<QCResultPayload>(y=>y.TestCode==testCode && y.Value==result)));
        }

        [TestMethod]
        public void IfTestCodeDoesNotExistsAnUnableToProcessResponseIsReturned()
        {
            labConfigurationServices.Setup(x => x.GetApplicationByTestCode(15182)).Returns(() => null);

            var response = handler.Handle(new StoreQCResult(15182, 3.6, DateTime.Now));

            Assert.IsNotNull(response);
            var storeQCResultResponse = response as QCResultNotStoredResponse;

            Assert.IsNotNull(storeQCResultResponse);
            storeQCResultResponse.Status.Should().Be(CommandResult.Error);
            storeQCResultResponse.Reason.Should().Be(string.Format(QCRoutineMessages.ApplictionCodeDoesNotExists,15182));
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
