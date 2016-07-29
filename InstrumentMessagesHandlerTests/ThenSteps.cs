using System;
using System.Linq;
using FluentAssertions;
using Infrastructure.DTOs;
using Infrastructure.Repositories;
using InstrumentCommunication.Application;
using InstrumentCommunication.Application.Commands;
using InstrumentCommunication.Application.Ports;
using InstrumentCommunication.Sender;
using InstrumentCommunication.TsnAdapter;
using InstrumentCommunication.TsnAdapter.Commands;
using LabConfiguration.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QCEvaluation.Application;
using QCEvaluation.Application.Commands;
using QCEvaluation.Application.DTOs;
using QCEvaluation.Application.Responses;
using QCEvaluation.Domain;
using QCEvaluation.Domain.Repositories;
using TechTalk.SpecFlow;

namespace InstrumentMessagesHandlerTests
{
    [Binding]
    public class ThenSteps
    {
        [Then(@"Data Manager sends a CI11 message")]
        public void ThenDataManagerSendsACIMessage()
        {
            ScenarioContext.Current.Get<Mock<IMessageSender>>().Verify(x => x.SendMessage(It.IsAny<FTPCredentialsMessageDTO>()));
        }

        [Then(@"the CI(.*) message contains the FTP user and FTP databasket information from DM")]
        public void ThenTheCIMessageContainsTheFTPUserAndFTPDatabasketInformationFromDM(int p0)
        {
            ScenarioContext.Current.Get<Mock<IMessageSender>>().Verify(x => x.SendMessage(It.Is<FTPCredentialsMessageDTO>(p => p.User == "DMFTPUser" && p.DataBasket == "DMFTPBasket")));
        }

        [Then(@"Data Manager expects to receive a RO(.*) to confirm the reception")]
        public void ThenDataManagerExpectsToReceiveAROToConfirmTheReception(int p0)
        {
            var status = ScenarioContext.Current.Get<InstrumentCommunicationServices>().GetStatus();
            Assert.AreEqual(CommunicationStatusDTO.WaitingForAcknowledge, status);
        }

        [Then(@"Data Manager expects to receive a CI(.*) message")]
        public void ThenDataManagerExpectsToReceiveACIMessage(int p0)
        {
            var status = ScenarioContext.Current.Get<InstrumentCommunicationServices>().GetStatus();
            Assert.AreEqual(CommunicationStatusDTO.WaitingForMessages, status);
        }

        [Then(@"the CI(.*) message contains the FTP user and FTP databasket information from the Main Unit")]
        public void ThenTheCIMessageContainsTheFTPUserAndFTPDatabasketInformationFromTheMainUnit(int p0)
        {
            Assert.Inconclusive();
        }

        [Then(@"Data Manager sends an RO(.*) message to confirm the reception")]
        public void ThenDataManagerSendsAnROMessageToConfirmTheReception(int p0)
        {
            ScenarioContext.Current.Get<Mock<IMessageSender>>().Verify(x => x.SendMessage(It.IsAny<ConfirmationMessageDTO>()));
        }

        [Then(@"the Main Unit sends an RO(.*) Message to confirm the reception")]
        public void ThenTheMainUnitSendsAnROMessageToConfirmTheReception(int p0)
        {
            ScenarioContext.Current.Get<InstrumentCommunicationServices>().Handle(new ConfirmMessageReceptionCommand());
        }

        [Then(@"the Main Unit sends an CI(.*) Message to confirm the reception")]
        public void ThenTheMainUnitSendsAnCIMessageToConfirmTheReception(int p0)
        {
            var credentialsMessageDto = new StoreMainUnitFTPCredentialsCommand("MainUnitFTPUser", "MainUnitFTPBasket", "MainUnitFTPPassword");
            ScenarioContext.Current.Get<InstrumentCommunicationServices>().Handle(credentialsMessageDto);
        }


        [Then(@"Data Manager requests to download the urgent information to TSNAgent")]
        public void ThenDataManagerRequestsToDownloadTheUrgentInformationToTSNAgent()
        {
            var tsnAgentAdapter = ScenarioContext.Current.Get<Mock<ITSNAdapter>>();
            tsnAgentAdapter.Verify(x => x.Handle(It.IsAny<DownloadUrgentInformation>()), Times.Once());
        }

        [Then(@"Data Manager sends an UR(.*) message to the Main Unit with the xml file")]
        public void ThenDataManagerSendsAnURMessageToTheMainUnitWithTheXmlFile(int p0)
        {
            ScenarioContext.Current.Get<Mock<IMessageSender>>().Verify(x => x.SendMessage(It.IsAny<UrgentInformationDownloadDTO>()));
        }
        [Then(@"Data Manager sends an RO(.*) confirmation message")]
        public void ThenDataManagerSendsAnROConfirmationMessage(int p0)
        {
            ScenarioContext.Current.Get <Mock<IMessageSender>>().Verify(x => x.SendMessage(It.IsAny<ConfirmationMessageDTO>()));
        }
        
        [Then(@"Data Manager requests to download the List of Parameters to TSNAgent")]
        public void ThenDataManagerRequestsToDownloadTheListOfParametersToTSNAgent()
        {
            ScenarioContext.Current.Get<Mock<ITSNAdapter>>().Verify(x => x.Handle(It.IsAny<DownloadListOfParameters>()));
        }
        
        [Then(@"Data Manager sends a PL(.*) message to the Main Unit")]
        public void ThenDataManagerSendsAPLMessageToTheMainUnit(int p0)
        {
            ScenarioContext.Current.Get<Mock<IMessageSender>>().Verify(x => x.SendMessage(It.IsAny<ParametersListMessageDTO>()));
        }
        
        [Then(@"Data Manager sends an xml file with th name ListOfApp\.xml with all the parameters")]
        public void ThenDataManagerSendsAnXmlFileWithThNameListOfApp_XmlWithAllTheParameters()
        {
            var messageSender = ScenarioContext.Current.Get<Mock<IMessageSender>>();
            messageSender.Verify(x=>x.SendMessage(It.Is<ParametersListMessageDTO>(p=>p.XmlMessage!=null && p.XmlName == "ListOfApp.xml")));
        }
        
        [Then(@"Data Manager requests to download the parameter to TSNAgent")]
        public void ThenDataManagerRequestsToDownloadTheParameterToTSNAgent()
        {
            ScenarioContext.Current.Get<Mock<ITSNAdapter>>().Verify(x => x.Handle(It.IsAny<DownloadParameterCommand>()));
        }
        
        [Then(@"Data Manager sends a PD(.*) message to the Main Unit")]
        public void ThenDataManagerSendsAPDMessageToTheMainUnit(int p0)
        {
            ScenarioContext.Current.Get<Mock<IMessageSender>>().Verify(x=>x.SendMessage(It.IsAny<ParameterDTO>()));
        }
        
        [Then(@"Data Manager sends an xml file with th name Application\.xml with the parameters")]
        public void ThenDataManagerSendsAnXmlFileWithThNameApplication_XmlWithTheParameters()
        {
            var messageSender = ScenarioContext.Current.Get<Mock<IMessageSender>>();
            messageSender.Verify(x => x.SendMessage(It.Is<ParameterDTO>(p => p.XmlMessage != null && p.XmlName == "ApplicationTest.xml")));
        }
        
        [Then(@"Data Manager stores the parameter")]
        public void ThenDataManagerStoresTheParameter()
        {
            var applicationRepository = ScenarioContext.Current.Get<IApplicationRepository>();
            var applicationExists = applicationRepository.ApplicationCodeExists(1028);

            Assert.IsTrue(applicationExists);
        }

        [Then(@"the rule is saved on the system with the values")]
        public void ThenTheRuleIsSavedOnTheSystemWithTheValues(Table table)
        {
            var ruleName = table.Rows[0]["Name"];
            var withingControlValue = Convert.ToBoolean(table.Rows[0]["WithinControl"]);
            var comment = table.Rows[0]["Comment"];
            var numberOfControls = Convert.ToInt32(table.Rows[0]["NumberOfControls"]);
            var standardDeviationLimits = Convert.ToInt32(table.Rows[0]["StandardDeviationLimits"]);


            var qcRuleRespository = new MongoDBQCRulesRepository();
            var qcRule = qcRuleRespository.GetStandardDeviationRuleByName(ruleName);
            
            Assert.AreEqual(ruleName, qcRule.Name);
            Assert.AreEqual(withingControlValue, qcRule.WithinControl);
            Assert.AreEqual(comment, qcRule.Comment);
            Assert.AreEqual(numberOfControls, qcRule.NumberOfControls);
            Assert.AreEqual(standardDeviationLimits, qcRule.StandardDeviationLimit);
        }

        [Then(@"the rule ""(.*)"" is deactivated")]
        public void ThenTheRuleIsDeactivated(string ruleName)
        {
            var ruleRepository = ScenarioContext.Current.Get<IQCRuleRepository>();

            var qcRule = ruleRepository.GetRuleByName(ruleName);

            qcRule.Active.Should().BeFalse();
        }

        [Then(@"the rule ""(.*)"" is activated")]
        public void ThenTheRuleIsActivated(string ruleName)
        {
            var ruleRepository = ScenarioContext.Current.Get<IQCRuleRepository>();

            var qcRule = ruleRepository.GetRuleByName(ruleName);

            qcRule.Active.Should().BeTrue();
        }


        [Then(@"the ApplicationTest ""(.*)"" and the qc rule ""(.*)"" are linked")]
        public void ThenTheApplicationAndTheQcRuleAreLinked(int applicationCode, int ruleName)
        {
            var applicationQC = GetApplicationQC(applicationCode);

            var rule = GetQCRule(ruleName);

            applicationQC.HasRuleAssigned(rule).Should().BeTrue();
        }

        private static QCRule GetQCRule(int ruleName)
        {
            var rulesRepository = ScenarioContext.Current.Get<IQCRuleRepository>();
            var rule = rulesRepository.GetRuleByName(ruleName.ToString());
            return rule;
        }

        private static ApplicationQC GetApplicationQC(int applicationCode)
        {
            var applicationRepository = ScenarioContext.Current.Get<IQCApplicationRepository>();
            return applicationRepository.GetApplicationByTestCode(applicationCode);          
        }

        [Then(@"the ApplicationTest ""(.*)"" and the qc rule ""(.*)"" are not linked")]
        public void ThenTheApplicationAndTheQcRuleAreNotLinked(int applicationCode, int ruleName)
        {
            var applicationQC = GetApplicationQC(applicationCode);

            var rule = GetQCRule(ruleName);

            applicationQC.HasRuleAssigned(rule).Should().BeFalse();
        }

        [Then(@"the qc result is succesfully evaluated")]
        public void ThenTheQcResultIsSuccesfullyEvaluated()
        {
            var qcServices = ScenarioContext.Current.Get<IQCEvaluationServices>();
            var results = ScenarioContext.Current.Get<QCRoutine.Domain.IQCResultsRepository>().GetResultsOrderedByDate(1);
            
            Assert.IsNotNull(results);
            results.Should().NotBeEmpty();

            var qcResultId = results.First().Id;
            var response = qcServices.Handle(new GetEvaluation(qcResultId));

            var evaluation = response as GetEvaluationResponse;
            Assert.IsNotNull(evaluation);

            evaluation.EvaluationResult.Should().Be(EvaluationDTO.Ok);
        }

    }
}
