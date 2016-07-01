using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationServices;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCEvaluation.Domain.Events;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Domain.Tests
{
    [TestClass]
    public class ApplicationQCTests
    {
        [TestMethod]
        public void WhenCallCreateItIsAssignedToTheChange()
        {
            var application = new ApplicationQC(new List<AggreggateEvent>());
            application.Create();

            application.Changes.Should().HaveCount(1);
            application.Changes.First().Should().BeOfType(typeof (ApplicationCreated));
        }

        [TestMethod]
        public void WhenDisableRuleTheChangeIsCreated()
        {
            var application = new ApplicationQC(new List<AggreggateEvent>());
            QCRule rule = new StandardDeviationRule();

            var rulesRepository = new Mock<IQCRuleRepository>();
            rulesRepository.Setup(x => x.ExistsRule(rule.Id)).Returns(true);

            application.EnableRule(rule);
            application.DisableRule(rule);

            application.Changes.OfType<QCRuleDisabled>().Should().HaveCount(1);
            var qcRuleDisabled = application.Changes.OfType<QCRuleDisabled>().First();
            qcRuleDisabled.RuleId.Should().Be(rule.Id);
            qcRuleDisabled.ApplicationId.Should().Be(application.Id);
        }

        [TestMethod]
        public void WhenDisablingARuleItIsNotOnTheListOfEnabledRules()
        {
            var application = new ApplicationQC(new List<AggreggateEvent>());
            QCRule ruleId = new StandardDeviationRule();

            var rulesRepository = new Mock<IQCRuleRepository>();
            rulesRepository.Setup(x => x.ExistsRule(ruleId.Id)).Returns(true);

            application.EnableRule(ruleId);
            application.DisableRule(ruleId);

            application.GetRulesEnabled(rulesRepository.Object).Contains(ruleId).Should().BeFalse();
        }

        [TestMethod]
        public void IfTheRuleIsNotAssignedToTheApplicationTheEventIsNotCreated()
        {
            var application = new ApplicationQC(new List<AggreggateEvent>());
            QCRule ruleId = new StandardDeviationRule();
            application.DisableRule(ruleId);

            application.Changes.Should().HaveCount(0);
        }

        [TestMethod]
        public void WhenTheApplicationIsCreatedItIsCreatedWithTheTestCode()
        {
            var application = new ApplicationQC(new List<AggreggateEvent>());
            QCRule ruleId = new StandardDeviationRule();
            application.Create(Guid.NewGuid(), 12312);

            application.TestCode.Should().Be(12312);
        }

        [TestMethod]
        public void WhenTheApplicationIsDeletedItGeneratesTheApplicationDeletedEvent()
        {
            var application = new ApplicationQC();
            application.Create(Guid.NewGuid(), 12312);
            application.Delete();

            application.Changes.Should().Contain(x => x.GetType() == typeof(ApplicationDeleted));
        }

        [TestMethod]
        public void IfTheApplicationIsNotCreatedCannotBeDeleted()
        {
            var application = new ApplicationQC(new List<AggreggateEvent>());
            
            application.Delete();

            application.Changes.Should().NotContain(x => x.GetType() == typeof (ApplicationDeleted));
        }

        [TestMethod]
        public void WhenTheApplicationIsDeletedItsStatusIsDeleted()
        {
            var application = new ApplicationQC();
            application.Create(Guid.NewGuid(), 12312);
            application.Delete();

            application.Status.Should().Be(ApplicationStatus.Deleted);
        }

        [TestMethod]
        public void WhenTheApplicationIsCreatedItsStatusIsInstalled()
        {
            var application = new ApplicationQC();
            application.Create(Guid.NewGuid(), 12312);
            application.Status.Should().Be(ApplicationStatus.Installed);
        }

        [TestMethod]
        public void IfApplicationInstalledCannotBeCreated()
        {
            var application = new ApplicationQC();
            application.Create(Guid.NewGuid(), 12222);

            application.Create(Guid.NewGuid(), 12232);

            application.Changes.Count.Should().Be(1);
            application.TestCode.Should().Be(12222);

        }
    }
}
