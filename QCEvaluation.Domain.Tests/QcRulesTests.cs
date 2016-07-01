using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCEvaluation.Domain.Events;
using QCEvaluation.Domain.Exceptions;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Domain.Tests
{
    [TestClass]
    public class QcRulesTests
    {
        private Mock<IQCRuleRepository> rulesRepository;

        [TestInitialize]
        public void Initialize()
        {
            rulesRepository = new Mock<IQCRuleRepository>();
        }

        [TestMethod]
        public void WhenRuleIsCreatedTheEventRuleCreatedIsOnTheChangesCollection()
        {
            var rule = new StandardDeviationRule();
            rule.CreateStandardDeviationRule("Rule1605", true, string.Empty, null, null, rulesRepository.Object);

            var qcRuleCreatedEvent = rule.Changes.First(x => x.GetType() == typeof (QCStandardDeviationRuleCreated)) as QCStandardDeviationRuleCreated;
            Assert.IsNotNull(qcRuleCreatedEvent);
            qcRuleCreatedEvent.QCRuleId.Should().NotBeEmpty();
            qcRuleCreatedEvent.Name.Should().Be("Rule1605");
            qcRuleCreatedEvent.IsWithinControl.Should().BeTrue();           
        }

        [TestMethod]
        public void WhenRuleIsCreatedIsActiveByDefault()
        {
            var rule = new StandardDeviationRule();
            rule.CreateStandardDeviationRule("Rule1605", true, string.Empty, null, null, rulesRepository.Object);

            rule.Active.Should().BeTrue();
        }

        [TestMethod]
        public void WhenRuleIsCreatedItJHasTheCreatedFlagToTrue()
        {
            var rule = new StandardDeviationRule();
            rule.CreateStandardDeviationRule("Rule1605", true, string.Empty, null, null, rulesRepository.Object);

            rule.IsCreated.Should().BeTrue();
        }

        [TestMethod]
        [ExpectedException(typeof(QCRuleNameExistsException))]
        public void ThereCannotBe2RulesWithTheSameName()
        {
            var rule = new StandardDeviationRule();
            
            Mock<IQCRuleRepository> rulesRepository = new Mock<IQCRuleRepository>();
            rulesRepository.Setup(x => x.ExistARuleWithName("Rule1605")).Returns(true);

            rule.CreateStandardDeviationRule("Rule1605", true, string.Empty, null, null, rulesRepository.Object);
        }

        [TestMethod]
        public void TheQCRuleIsCreatedWithTheStandardDeviationMethod()
        {
            var rule = new StandardDeviationRule();
            var numberOfControls = 2;
            int standardDeviationLimit = 8;
            rule.CreateStandardDeviationRule("rule1548", false, string.Empty, numberOfControls, standardDeviationLimit, rulesRepository.Object);

            var qcRuleCreatedEvent = rule.Changes.First(x => x.GetType() == typeof(QCStandardDeviationRuleCreated)) as QCStandardDeviationRuleCreated;
            Assert.IsNotNull(qcRuleCreatedEvent);
            qcRuleCreatedEvent.NumberOfControls.Should().Be(numberOfControls);
            qcRuleCreatedEvent.StandardDeviationLimit.Should().Be(standardDeviationLimit);
        }

        [TestMethod]
        public void OnceAQCRuleStandardDeviationIsCreatedItsCommentCanBeChanged()
        {
            var rule = new StandardDeviationRule();
            rule.CreateStandardDeviationRule("rule1603", true, string.Empty, 1, 5, rulesRepository.Object);

            rule.UpdateComment("Comment16");

            var qcRuleEvent = rule.Changes.First(x => x.GetType() == typeof (QCRuleCommentUpdated)) as QCRuleCommentUpdated;
            qcRuleEvent.Comment.Should().Be("Comment16");
        }

        [TestMethod]
        public void OnceAQCRuleStandardDeviationIsCreatedItsNumberOfControlsCanBeChanged()
        {
            var rule = new StandardDeviationRule();
            rule.CreateStandardDeviationRule("rule1603", true, string.Empty, 1, 5, rulesRepository.Object);

            rule.UpdateNumberOfControls(3);

            var qcRuleEvent = rule.Changes.First(x => x.GetType() == typeof(QCRuleNumberOfControlsUpdated)) as QCRuleNumberOfControlsUpdated;
            qcRuleEvent.NumberOfControls.Should().Be(3);

            rule.NumberOfControls.Should().Be(3);
        }

        [TestMethod]
        public void OnceAQCRuleStandardDeviationIsCreatedItsStandardDeviationLimitCanBeChanged()
        {
            var rule = new StandardDeviationRule();
            rule.CreateStandardDeviationRule("rule1603", true, string.Empty, 1, 5, rulesRepository.Object);

            rule.UpdateLimits(1);

            var qcRuleEvent = rule.Changes.First(x => x.GetType() == typeof(QCRuleLimitsUpdated)) as QCRuleLimitsUpdated;
            qcRuleEvent.StandardDeviationLimit.Should().Be(1);

            rule.StandardDeviationLimit.Should().Be(1);
        }

        [TestMethod]
        public void WhenRuleIsDeactivatedTheEventIsAddedToTheChanges()
        {
            var rule = new StandardDeviationRule();
            rule.CreateStandardDeviationRule("Rule1559", true, String.Empty, null, null, rulesRepository.Object);

            rule.Deactivate();

            rule.Changes.Count.Should().Be(2);
            rule.Changes.Last().Should().BeOfType(typeof (QCRuleDeactivated));
        }

        [TestMethod]
        public void WhenRuleIsDeactivatedWhenDeactivatedAgainNoNewEventIsAddedToTheChanges()
        {
            var rule = new StandardDeviationRule();
            rule.CreateStandardDeviationRule("Rule1559", true, String.Empty, null, null, rulesRepository.Object);

            rule.Deactivate();

            var numberOfEvents = rule.Changes.Count;

            rule.Deactivate();

            rule.Changes.Count.Should().Be(numberOfEvents);
        }

        [TestMethod]
        public void WhenARuleIsDeactivedItCanBeReactivated()
        {
            var rule = new StandardDeviationRule();
            rule.CreateStandardDeviationRule("Rule2220", false, "Comment", 3, 0, rulesRepository.Object);

            rule.Deactivate();

            rule.Reactivate();

            rule.Active.Should().BeTrue();
            rule.Changes.Last().Should().BeOfType(typeof (QCRuleReactivate));
        }

        [TestMethod]
        public void WhenARuleIsActiveItCannotBeReactivatedAgain()
        {
            var rule = new StandardDeviationRule();
            rule.CreateStandardDeviationRule("Rule2220", false, "Comment", 3, 0, rulesRepository.Object);
           
            rule.Reactivate();

            rule.Active.Should().BeTrue();
            rule.Changes.Count.Should().Be(1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenDeactivatingANotExistingRuleAnExceptionIsThrown()
        {
            var rule = new StandardDeviationRule();

            rule.Deactivate();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenReactivatingANotExistingRuleAnExceptionIsThrown()
        {
            var rule = new StandardDeviationRule();

            rule.Reactivate();
        }
    }
}
