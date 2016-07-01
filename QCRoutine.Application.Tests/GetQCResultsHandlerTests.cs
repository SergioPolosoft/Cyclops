using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationServices;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCEvaluation.Application;
using QCRoutine.Application.Commands;
using QCRoutine.Application.Commands.Handlers;
using QCRoutine.Domain;

namespace QCRoutine.Application.Tests
{
    [TestClass]
    public class GetQCResultsHandlerTests
    {
        private GetQCResultHandler handler;
        private Mock<IQCResultsRepository> resultsRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            resultsRepository = new Mock<IQCResultsRepository>();
            handler = new GetQCResultHandler(resultsRepository.Object);
        }

        [TestMethod]
        public void GetResulstsByDateWithZeroDaysReturnsAnOutOfRangeCommand()
        {
            var command = new GetResultsByDate(0);
            var response = handler.Handle(command);

            response.Status.Should().Be(CommandResult.Error);

            response.Message.Should().Be(QCRoutineMessages.InvalidNumberOfResults);
        }

        [TestMethod]
        public void GetResultsByDateCallsTheResultsRepository()
        {
            var command = new GetResultsByDate(1);
            
            handler.Handle(command);

            resultsRepository.Verify(x => x.GetResultsOrderedByDate(1));
        }

        [TestMethod]
        public void GetResultsByDateWhenExceptionEmptyListIsReturned()
        {
            resultsRepository.Setup(x => x.GetResultsOrderedByDate(1)).Throws(new Exception());

            var response = handler.Handle(new GetResultsByDate(1));

            response.Status.Should().Be(CommandResult.Error);
            response.Message.Should().Be(QCRoutineMessages.ErrorOcurredAccessingRepository);
        }

        [TestMethod]
        public void GetResultsByDateTheResultsReturnedByTheRepositoryAreReturnedAsDTOs()
        {
            var qcResult = new QCResult(15551, 4.6201, DateTime.Now);
            resultsRepository.Setup(x => x.GetResultsOrderedByDate(1))
                .Returns(new List<QCResult>() {qcResult});

            var response = handler.Handle(new GetResultsByDate(1));

            response.Status.Should().Be(CommandResult.Success);

            response.Results.Count.Should().Be(1);
            response.Results.First().Id.Should().Be(qcResult.Id);
        }
    }
}
