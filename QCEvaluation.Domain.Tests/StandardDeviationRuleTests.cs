using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Domain.Tests
{
    [TestClass]
    public class StandardDeviationRuleTests
    {
        private Mock<IQCResultsRepository> qcResultsRepository;
        private StandardDeviationRule standardDeviationRule;
        private QualityControl qualityControlPayload;

        [TestInitialize]
        public void TestInitialize()
        {
            qcResultsRepository = new Mock<IQCResultsRepository>();
          
            standardDeviationRule = new StandardDeviationRule();

            qualityControlPayload = new QualityControl();
        }

        [TestMethod]
        public void TheResultsAreLoadedFromTheRepository()
        {
            var qcResult = new QCResult();

            standardDeviationRule.UpdateNumberOfControls(2);

            standardDeviationRule.Evaluate(qcResult, qcResultsRepository.Object, qualityControlPayload);

            qcResultsRepository.Verify(x => x.GetLastQCResults(qcResult.TestCode,2));
        }

        [TestMethod]
        public void IfThereAreNotEnoughResultsTheEvaluationResultIsNotEnoughData()
        {
            var qcResult = new QCResult();
            qcResultsRepository.Setup(
                x => x.GetLastQCResults(qcResult.TestCode, 2))
                .Returns(new List<QCResult> {qcResult});
            
            standardDeviationRule.UpdateNumberOfControls(2);

            var result = standardDeviationRule.Evaluate(qcResult, qcResultsRepository.Object, qualityControlPayload);

            result.Should().Be(EvaluationResult.NotEnoughData);
        }

        [TestMethod]
        public void IfThereAreNoResultsTheEvaluationResultIsNotEnoughData()
        {
            var qcResult = new QCResult();
            qcResultsRepository.Setup(
                x => x.GetLastQCResults(qcResult.TestCode, 2))
                .Returns(()=>null);

            standardDeviationRule.UpdateNumberOfControls(2);

            var result = standardDeviationRule.Evaluate(qcResult, qcResultsRepository.Object, qualityControlPayload);

            result.Should().Be(EvaluationResult.NotEnoughData);
        }
        
        [TestMethod]
        public void WhenTheResultIsOutsideOfTheRangeOfTheSDTheResultStatusIsError()
        {
            var qcResult = new QCResult()
            {
                Id = Guid.NewGuid(),
                TestCode = 10620,
                Result = 55
            };

            qcResultsRepository.Setup(x => x.GetLastQCResults(qcResult.TestCode, 2)).Returns(new List<QCResult>()
            {
                qcResult,
                new QCResult()
                {
                    Id = Guid.NewGuid(),
                    TestCode = qcResult.TestCode,
                    Result = 66
                }
            });

            standardDeviationRule.UpdateNumberOfControls(2);
            standardDeviationRule.UpdateLimits(3);

            var evaluationResult = standardDeviationRule.Evaluate(qcResult, qcResultsRepository.Object, qualityControlPayload);

            evaluationResult.Should().Be(EvaluationResult.Error);
        }
    
        [TestMethod]
        public void WhenTheResultIsInsideOfTheRangeOfTheSDTheResultStatusIsOk()
        {
            var qcResult = new QCResult()
            {
                Id = Guid.NewGuid(),
                TestCode = 10620,
                Result = 11
            };

            qcResultsRepository.Setup(x => x.GetLastQCResults(qcResult.TestCode, 1)).Returns(new List<QCResult>()
            {
                qcResult
            });
            
            standardDeviationRule.UpdateNumberOfControls(1);
            standardDeviationRule.UpdateLimits(3);

            var qualityControl = new QualityControl
            {
                TestCode = 10620,
                TargetValue = 11,
                StandardDeviation = 7
            };

            var evaluationResult = standardDeviationRule.Evaluate(qcResult, qcResultsRepository.Object, qualityControl);

            evaluationResult.Should().Be(EvaluationResult.Ok);
        }

        [TestMethod]
        public void IfOneOfTheQCResultsIsInsideTheRangeThenEvaluationIsOk()
        {
            var qcResult = new QCResult()
            {
                Id = Guid.NewGuid(),
                TestCode = 10620,
                Result = 11
            };

            qcResultsRepository.Setup(x => x.GetLastQCResults(qcResult.TestCode, 2)).Returns(new List<QCResult>()
            {
                qcResult,
                new QCResult()
                {
                    Id = Guid.NewGuid(),
                    TestCode = qcResult.TestCode,
                    Result = 66
                }
            });

            standardDeviationRule.UpdateNumberOfControls(2);
            standardDeviationRule.UpdateLimits(3);

            var qualityControl = new QualityControl
            {
                TestCode = 10620,
                TargetValue = 60,
                StandardDeviation = 7
            };

            var evaluationResult = standardDeviationRule.Evaluate(qcResult, qcResultsRepository.Object, qualityControl);

            evaluationResult.Should().Be(EvaluationResult.Ok);
        }
    }
}
