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
using QCEvaluation.Domain.Exceptions;
using QCEvaluation.Domain.Repositories;

namespace QCEvaluation.Application.Tests
{
    [TestClass]
    public class EnableRuleForApplicationTests
    {
        private Mock<IQCApplicationRepository> applicationRepository;
        private EnableRuleForApplicationHandler handler;
        private EnableRuleForApplication command;
        private Mock<IQCRuleRepository> rulesRepository;
        private Guid ruleId;
        private Guid applicationId;
        private int testCode = 12372;

        [TestInitialize]
        public void TestInitialize()
        {
            applicationId = Guid.NewGuid();
            ruleId = Guid.NewGuid();

            applicationRepository = new Mock<IQCApplicationRepository>();
            applicationRepository.Setup(x => x.GetApplication(applicationId))
                .Returns(new ApplicationQC(new ApplicationCreated(applicationId, testCode)));
            
            rulesRepository = new Mock<IQCRuleRepository>();
            rulesRepository.Setup(x => x.GetRuleById(ruleId)).Returns(new StandardDeviationRule(new List<QCRuleEvent> {new QCStandardDeviationRuleCreated(ruleId,false,string.Empty,string.Empty,null,null)}));

            handler = new EnableRuleForApplicationHandler(applicationRepository.Object, rulesRepository.Object);

            command = new EnableRuleForApplication(ruleId, applicationId, testCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationNotExistsException))]
        public void IfApplicationDoesNotExistsAnExceptionIsThrown()
        {
            applicationRepository.Setup(x => x.GetApplication(applicationId)).Returns(()=>null);
            
            handler.Handle(command);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationNotExistsException))]
        public void IfApplicationIsDeletedAnExceptionIsThrown()
        {
            applicationRepository.Setup(x => x.GetApplication(applicationId)).Returns(() => new ApplicationQC(new ApplicationCreated(applicationId,testCode), new ApplicationDeleted(applicationId)));

            handler.Handle(command);
        }

        [TestMethod]
        public void WhenRuleIsEnabledAnEventIsCreated()
        {
            handler.Handle(command);

            applicationRepository.Verify(x => x.Save(It.Is<IList<AggreggateEvent>>(y => y.Any(z => z.GetType() == typeof(QCRuleEnabled)))));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IfRuleDoesNotExistsAnApplicationIsThrown()
        {
            rulesRepository.Setup(x => x.GetRuleById(ruleId)).Returns(() => null);

            handler.Handle(command);
        }

        [TestMethod]
        public void IfRuleAlreadyAssignedNoEventIsCreated()
        {
            applicationRepository.Setup(x => x.GetApplication(applicationId))
                .Returns(new ApplicationQC(new ApplicationCreated(applicationId, 17202),
                    new QCRuleEnabled(applicationId, ruleId)));

            handler.Handle(command);

            applicationRepository.Verify(x => x.Save(It.Is<IList<AggreggateEvent>>(y => y.Count == 0)));
        }
    }
}
