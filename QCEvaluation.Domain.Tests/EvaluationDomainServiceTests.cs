using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCConfiguration.Domain;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Domain.Tests
{
    [TestClass]
    public class EvaluationDomainServiceTests
    {
        private Mock<IQCResultsRepository> qcResultsRepository;
        private EvaluationDomainService evaluationService;
        private List<QCRule> listOfRules;
        private int testCode = 17202;
        private QualityControlPayload qualityControlPayload;

        [TestInitialize]
        public void TestInitialize()
        {
            qcResultsRepository = new Mock<IQCResultsRepository>();

            evaluationService = new EvaluationDomainService(qcResultsRepository.Object);

            var rule2QCsOutOf2Limits = new StandardDeviationRule();
            rule2QCsOutOf2Limits.UpdateNumberOfControls(1);
            rule2QCsOutOf2Limits.UpdateLimits(3);

            var rule1QCsOutOf3Limits = new StandardDeviationRule();
            rule1QCsOutOf3Limits.UpdateNumberOfControls(2);
            rule1QCsOutOf3Limits.UpdateLimits(2);

            listOfRules = new List<QCRule>
            {
                rule2QCsOutOf2Limits, rule1QCsOutOf3Limits
            };

            var qualityControl = new QualityControl();
            qualityControl.Create(testCode, 2, 1);

            qualityControlPayload = new QualityControlPayload(qualityControl);
        }

        [TestMethod]
        public void IfOneRuleHasAnErrorStatusErrorIsReturned()
        {
            var qcResultToEvaluate = new QCResult(){TestCode = testCode,Id = Guid.NewGuid(),Result = 4.1};
            
            qcResultsRepository.Setup(x => x.GetLastQCResults(testCode, 1)).Returns(new List<QCResult>
            {
                qcResultToEvaluate
            });

            qcResultsRepository.Setup(x => x.GetLastQCResults(testCode, 2)).Returns(new List<QCResult>()
            {
                qcResultToEvaluate,
                new QCResult() {TestCode = testCode, Id = Guid.NewGuid(), Result = 5.1}
            });
            
            var evaluation = evaluationService.Evaluate(qcResultToEvaluate, listOfRules, qualityControlPayload);

            evaluation.Should().Be(EvaluationResult.Error);
        }


        [TestMethod]
        public void IfNoErrorsButAnyRuleWithNotEnoughDataErrorThenNotEnoughDataStatusIsReturned()
        {
            var evaluation = evaluationService.Evaluate(new QCResult() { TestCode = testCode, Id = Guid.NewGuid(), Result = 2.1 }, listOfRules, qualityControlPayload);

            evaluation.Should().Be(EvaluationResult.NotEnoughData);
        }

        [TestMethod]
        public void IfAllRulesAreOkStatusOkIsReturned()
        {
            var qcResultToEvaluate = new QCResult { TestCode = testCode, Id = Guid.NewGuid(), Result = 2.1 };

            qcResultsRepository.Setup(x => x.GetLastQCResults(testCode, 1)).Returns(new List<QCResult>
            {
                qcResultToEvaluate
            });

            qcResultsRepository.Setup(x => x.GetLastQCResults(testCode, 2)).Returns(new List<QCResult>
            {
                qcResultToEvaluate,
                new QCResult {TestCode = testCode, Id = Guid.NewGuid(), Result = 2.1}
            });
            
            var evaluation = evaluationService.Evaluate(qcResultToEvaluate, listOfRules, qualityControlPayload);

            evaluation.Should().Be(EvaluationResult.Ok);
        }
    }
}
