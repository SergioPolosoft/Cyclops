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
    public class ReactivateQCRuleTests
    {
        [TestMethod]
        public void WhenARuleIsReactivatedAReactivateRuleEventIsStored()
        {
            var repository = new Mock<IQCRuleRepository>();
            Guid ruleId = Guid.NewGuid();
            var standardDeviationRule = new StandardDeviationRule();
            standardDeviationRule.CreateStandardDeviationRule("Rule2236", false, string.Empty, 1,3, repository.Object);
            standardDeviationRule.Deactivate();
            repository.Setup(x => x.GetRuleById(ruleId)).Returns(standardDeviationRule);

            var handler = new ReactivateQCRuleHandler(repository.Object);

            handler.Handle(new ReactivateQCRule(ruleId));

            repository.Verify(x=>x.Save(It.Is<List<AggreggateEvent>>(y=>y.Any(z=>z.GetType()==typeof(QCRuleReactivate)))));
        }
    }
}
