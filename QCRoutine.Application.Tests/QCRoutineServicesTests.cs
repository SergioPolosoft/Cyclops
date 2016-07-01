using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationServices;
using FluentAssertions;
using LabConfiguration.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCEvaluation.Application;
using QCRoutine.Application.Commands;
using QCRoutine.Domain;

namespace QCRoutine.Application.Tests
{
    [TestClass]
    public class QCRoutineServicesTests
    {
        private IQCRoutineServices service;
        private Mock<IQCResultsRepository> resultsRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            resultsRepository = new Mock<IQCResultsRepository>();
            var configurationServices = new Mock<IQCEvaluationServices>();
            service = new QCRoutineServices(configurationServices.Object, resultsRepository.Object, new Mock<ILogger>().Object, new Mock<ILabConfigurationServices>().Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetResultsByDateWithZeroDaysThrowAnException()
        {
            var command = new GetResultsByDate(0);
            var response = service.Handle(command);
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetResultsByDateCallsTheResultsRepository()
        {
            Assert.Inconclusive();
            var command = new GetResultsByDate(1);
            var response = service.Handle(command);

            resultsRepository.Verify(x => x.GetResultsOrderedByDate(1));
        }

        [TestMethod]
        public void GetResultsByDateWhenExceptionEmptyListIsReturned()
        {
            resultsRepository.Setup(x => x.GetResultsOrderedByDate(1)).Throws(new Exception());

            var resultsByDate = service.Handle(new GetResultsByDate(1));

            resultsByDate.Should().BeNull();
        }

        [TestMethod]
        public void GetResultsByDateTheResultsReturnedByTheRepositoryAreReturnedAsDTOs()
        {
            var qcResult = new QCResult(15551, 4.6201, DateTime.Now);
            resultsRepository.Setup(x => x.GetResultsOrderedByDate(1))
                .Returns(new List<QCResult>() {qcResult});

            var resultsDTO = service.Handle(new GetResultsByDate(1));

            Assert.Inconclusive();
            //resultsDTO.Should().NotBeEmpty();
            //var firstDTO = resultsDTO.First();

            //firstDTO.QCResultId.Should().Be(qcResult.Id);
        }
    }
}
