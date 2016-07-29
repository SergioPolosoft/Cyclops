using System;
using InstrumentCommunication.Application.Ports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoreQCResult = InstrumentCommunication.Application.Commands.StoreQCResult;
using StoreQCResultHandler = InstrumentCommunication.Application.Commands.Handlers.StoreQCResultHandler;

namespace InstrumentAdapter.Application.Tests
{
    [TestClass]
    public class StoreQCResultHandlerTests
    {
        [TestMethod]
        public void WhenAStoreQCResultIsReceivedItIsSentToTheQCRoutineModule()
        {
            var measuredDateTime = DateTime.Now;
            const int testCode = 17310;
            const double result = 8.06;
            
            var qcRoutineServices = new Mock<IQCRoutineServicePort>();
            
            var handler = new StoreQCResultHandler(qcRoutineServices.Object);

            var storeQcResult = new StoreQCResult(testCode,result,measuredDateTime);
            handler.Handle(storeQcResult);

            qcRoutineServices.Verify(x => x.StoreQCResult(It.Is<QCResultDTO>(y => y.TestCode == testCode && y.Result == result && y.MeasurementDate == measuredDateTime)));
        }
    }
}
