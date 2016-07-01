using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCEvaluation.Application.Commands;
using QCEvaluation.Application.Commands.Handlers;
using QCEvaluation.Domain;
using QCEvaluation.Domain.Events;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Application.Tests
{
    [TestClass]
    public class DisableRuleForApplicatationTests
    {
        [TestMethod]
        public void WhenTheRuleIsDisabledAnEventIsStored()
        {
            var applicationId = Guid.NewGuid();
            var rule = new StandardDeviationRule();
                        
            var applicationsRepository = new Mock<IQCApplicationRepository>();
            applicationsRepository.Setup(x => x.GetApplication(applicationId)).Returns(new ApplicationQC(new List<AggreggateEvent>(){new ApplicationCreated(applicationId,17202),new QCRuleEnabled(applicationId, rule.Id)}));

            var handler = new DisableRuleForApplicationHandler(applicationsRepository.Object);
            handler.Handle(new DisableRuleForApplication(rule,applicationId));
            
            applicationsRepository.Verify(
                x => x.Save(It.Is<IList<AggreggateEvent>>(y => y.Any(z => z.GetType() == typeof(QCRuleDisabled) && ((QCRuleDisabled)z).ApplicationId == applicationId && ((QCRuleDisabled)z).RuleId == rule.Id))));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenApplicationNotExistsAnExceptionIsThrwon()
        {
            var applicationId = Guid.NewGuid();
            
            var applicationsRepository = new Mock<IQCApplicationRepository>();
            applicationsRepository.Setup(x => x.GetApplication(applicationId)).Returns(() => null);

            var handler = new DisableRuleForApplicationHandler(applicationsRepository.Object);

            QCRule ruleId = new StandardDeviationRule();
            handler.Handle(new DisableRuleForApplication(ruleId, applicationId));
        }        
    }
}
