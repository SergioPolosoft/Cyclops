using System;
using System.Security.Cryptography.X509Certificates;
using InstrumentCommunication.Application.Commands;
using InstrumentCommunication.Application.Commands.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCRoutine.Application;
using QCRoutine.Application.Commands;
using StoreQCResultHandler = InstrumentCommunication.Application.Commands.Handlers.StoreQCResultHandler;

namespace InstrumentAdapter.Application.Tests
{
    [TestClass]
    public class StoreQCResultHandlerTests
    {
        [TestMethod]
        public void WhenAStoreQCResultIsReceivedItIsSentToTheQCRoutineModule()
        {
            var qcRoutineServices = new Mock<IQCRoutineServices>();
            
            var handler = new StoreQCResultHandler(qcRoutineServices.Object);
            var storeQcResult = new StoreQCResult(17310,8.06,DateTime.Now);
            handler.Handle(storeQcResult);

            qcRoutineServices.Verify(x => x.Handle(storeQcResult));
        }
    }
}
