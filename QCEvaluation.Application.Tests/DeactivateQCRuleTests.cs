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
    public class DeactivateQCRuleTests
    {
        [TestMethod]
        public void DeactivatesQCRuleStoresARuleDeactivatedEvent()
        {
            var ruleId = Guid.NewGuid();
            
            var repository = new Mock<IQCRuleRepository>();

            var qcRule = new StandardDeviationRule();
            qcRule.CreateStandardDeviationRule("Rule1603",true,String.Empty, null,null, repository.Object);
            repository.Setup(x => x.GetRuleById(ruleId)).Returns(qcRule);

            var handler = new DeactivateQCRuleHandler(repository.Object);

            var command = new DeactivateQCRule(ruleId);
            handler.Handle(command);

            repository.Verify(x=>x.Save(It.Is<List<AggreggateEvent>>(listOfEvents => listOfEvents.OfType<QCRuleDeactivated>().Any())));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void IfRuleDoesNotExistsAnExceptionIsThrown()
        {
            var ruleId = Guid.NewGuid();

            var repository = new Mock<IQCRuleRepository>();

            repository.Setup(x => x.GetRuleById(ruleId)).Returns(() => null);

            var handler = new DeactivateQCRuleHandler(repository.Object);

            var command = new DeactivateQCRule(ruleId);
            handler.Handle(command);
        }

        [TestMethod]
        public void IfRuleIsDeactivatedNoEventIsCreated()
        {
            var ruleId = Guid.NewGuid();

            var repository = new Mock<IQCRuleRepository>();
           
            var mockedRule = new StandardDeviationRule(new List<QCRuleEvent>()
            {
                new QCStandardDeviationRuleCreated(ruleId, false, String.Empty, "Rule2234",null,null),
                new QCRuleDeactivated(ruleId)
            });

            repository.Setup(x => x.GetRuleById(ruleId)).Returns(mockedRule);


            var handler = new DeactivateQCRuleHandler(repository.Object);
            
            var command = new DeactivateQCRule(ruleId);
            handler.Handle(command);

            repository.Verify(x => x.Save(It.Is<List<AggreggateEvent>>(list => list.Count==0)));
        }
    }
}
