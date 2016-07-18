using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationServices;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCConfiguration.Application;
using QCEvaluation.Application.Commands;
using QCEvaluation.Domain;
using QCEvaluation.Domain.Events;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Application.Tests
{
    [TestClass]
    public class RulesManagementTests
    {
        private Mock<IQCRuleRepository> repository;
        private QCEvaluationServices handler;

        [TestInitialize]
        public void TestInitialize()
        {
            repository = new Mock<IQCRuleRepository>();

            handler = new QCEvaluationServices(repository.Object, new Mock<IQCApplicationRepository>().Object, new Mock<IEvaluationsRepository>().Object, new Mock<IQCResultsRepository>().Object, new Mock<IQCConfigurationServices>().Object);

        }

        [TestMethod]
        public void CreateNewStandardDeviationRule_StoresTheRuleInTheRepository()
        {
            const string ruleName = "1531";
            const string comment = "1531 comment";
            const int numberOfControls = 2;
            const int standardDeviationLimits = 6;

            handler.Handle(new CreateStandardDeviationRule(ruleName, true,comment,numberOfControls,standardDeviationLimits));
            
            repository.Verify(x=>x.Save(It.Is<IList<AggreggateEvent>>(y=>VerifyListOfChanges(y))));
        }

        private bool VerifyListOfChanges(IList<AggreggateEvent> domainEvents)
        {
            var ruleCreated = domainEvents.OfType<QCStandardDeviationRuleCreated>().First();
            ruleCreated.Name.Should().Be("1531");

            return true;
        }

        [TestMethod]
        public void ModifyStandardDeviationRuleValuesCreateTheEvents()
        {
            repository.Setup(x => x.GetStandardDeviationRuleByName("1657")).Returns(new StandardDeviationRule());
            
            const string ruleName = "1657";
            const string comment = "1657 comment";
            const int numberOfControls = 1;
            const int standardDeviationLimits = 6;

            var command = new ModifyStandardDeviationRule(ruleName, comment, numberOfControls, standardDeviationLimits);
            handler.Handle(command);
            
            repository.Verify(x => x.Save(It.Is<IList<AggreggateEvent>>(y => VerifyListOfUpdated(y,command))));
        }

        private bool VerifyListOfUpdated(IList<AggreggateEvent> domainEvents, ModifyStandardDeviationRule command)
        {
            domainEvents.OfType<QCRuleCommentUpdated>().First().Comment.Should().Be(command.NewComment);
            domainEvents.OfType<QCRuleLimitsUpdated>().First().StandardDeviationLimit.Should().Be(command.NewStandardDeviationLimits);
            domainEvents.OfType<QCRuleNumberOfControlsUpdated>().First().NumberOfControls.Should().Be(command.NewNumberOfControls);
            return true;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ModifyStandardDeviationRuleThatDoNotExistsThrowsAnException()
        {
            repository.Setup(x => x.ExistARuleWithName("1657")).Returns(false);
            
            const string ruleName = "1657";
            const string comment = "1657 comment";
            const int numberOfControls = 1;
            const int standardDeviationLimits = 6;

            handler.Handle(new ModifyStandardDeviationRule(ruleName,comment,numberOfControls,standardDeviationLimits));
        }

       
    }
}
