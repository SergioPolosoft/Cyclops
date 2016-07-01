using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCConfiguration.Application;
using QCEvaluation.Application.DTOs;
using QCEvaluation.Domain;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Application.Tests
{
    [TestClass]
    public class QCConfigurationServicesTests
    {
        private QCEvalutaionServices qcconfigurationServices;
        private Mock<IEvaluationsRepository> evaluationsRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            evaluationsRepository = new Mock<IEvaluationsRepository>();
            qcconfigurationServices = new QCEvalutaionServices(new Mock<IQCRuleRepository>().Object, new Mock<IQCApplicationRepository>().Object, evaluationsRepository.Object, new Mock<IQCResultsRepository>().Object, new Mock<IQCConfigurationServices>().Object);

        }

        [TestMethod]
        public void GetEvaluationForAQCResultIdTest()
        {
            Guid qcResultId = Guid.NewGuid();

            evaluationsRepository.Setup(x => x.GetEvaluationFor(qcResultId)).Returns(EvaluationResult.Ok);

            
            var evaluationDTO = qcconfigurationServices.GetEvaluationOf(qcResultId);

            evaluationDTO.Should().NotBeNull();
            evaluationDTO.Should().Be(EvaluationDTO.Ok);
        }

        [TestMethod]
        public void GetEvaluationIfNotExistNoEvaluationIsReturned()
        {
            Guid qcResultId = Guid.NewGuid();
            evaluationsRepository.Setup(x => x.GetEvaluationFor(qcResultId)).Returns(()=>EvaluationResult.NotEvaluated);
            
            var evaluationDTO = qcconfigurationServices.GetEvaluationOf(qcResultId);

            evaluationDTO.Should().NotBeNull();
            evaluationDTO.Should().Be(EvaluationDTO.NotEvaluated);
        }

        [TestMethod]
        public void GetEvaluationReturnsErrorFromRepository()
        {
            Guid qcResultId = Guid.NewGuid();
            evaluationsRepository.Setup(x => x.GetEvaluationFor(qcResultId)).Returns(() => EvaluationResult.Error);

            var evaluationDTO = qcconfigurationServices.GetEvaluationOf(qcResultId);

            evaluationDTO.Should().NotBeNull();
            evaluationDTO.Should().Be(EvaluationDTO.Error);
        }
    }
}
