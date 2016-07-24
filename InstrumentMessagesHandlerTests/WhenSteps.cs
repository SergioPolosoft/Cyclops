using System;
using System.Xml;
using ApplicationServices;
using Infrastructure.Repositories;
using InstrumentAdapter.Domain;
using InstrumentCommunication.Application;
using InstrumentCommunication.Application.Commands;
using InstrumentCommunication.TsnAdapter;
using InstrumentCommunication.TsnAdapter.Events;
using LabConfiguration.Adapters.QCEvaluationServiceReference;
using LabConfiguration.Domain;

using Moq;
using QCConfiguration.Application;
using QCConfiguration.Application.Commands;
using QCConfiguration.Domain;
using QCConfiguration.Domain.Repositories;
using QCEvaluation.Application;
using QCEvaluation.Application.Commands;
using QCEvaluation.Domain.Repositories;
using QCRoutine.Application;
using QCRoutine.Application.Commands;
using TechTalk.SpecFlow;

namespace InstrumentMessagesHandlerTests
{
    [Binding]
    class WhenSteps
    {
        [When(@"the connection is established")]
        public void WhenTheConnectionIsEstablished()
        {
            ScenarioContext.Current.Get<InstrumentCommunicationServices>().EstablishCommunication();
        }
        
        [When(@"Main Unit sends a PL(.*) message")]
        public void WhenMainUnitSendsAPLMessage(int p0)
        {
            var instrumenCommunicationServices = ScenarioContext.Current.Get<InstrumentCommunicationServices>();
            instrumenCommunicationServices.Handle(new GetParameterListCommand());
        }

        [When(@"Data Manager receives the List of Parameters from TSNAgen")]
        public void WhenDataManagerReceivesTheListOfParametersFromTSNAgen()
        {
            ScenarioContext.Current.Get<Mock<ITSNAdapter>>().Raise(x => x.Publish += null, new ListOfParametersReceived(new XmlDocument()));
        }

        [When(@"Main Unit sends a PD(.*) message")]
        public void WhenMainUnitSendsAPDMessage(int p0)
        {
            var instrumenCommunicationServices = ScenarioContext.Current.Get<InstrumentCommunicationServices>();
            instrumenCommunicationServices.Handle(new GetParameterCommand("aParameterFileName.xml"));
        }

        [When(@"Data Manager receives the parameter from TSNAgent")]
        public void WhenDataManagerReceivesTheParameterFromTSNAgent()
        {
            ScenarioContext.Current.Get<Mock<ITSNAdapter>>().Raise(x => x.Publish += null, new ParameterReceived(new XmlDocument()));
        }

        [When(@"Main Unit sends an AP(.*) message")]
        public void WhenMainUnitSendsAnAPMessage(int p0)
        {
            var applicationsReposiroty = ScenarioContext.Current.Get<IApplicationRepository>();
            applicationsReposiroty.RemoveByApplicationCode(1028);

            var services = ScenarioContext.Current.Get<InstrumentCommunicationServices>();
            var xmlDocument = new XmlDocument();
            xmlDocument.Load("Files/Adde801Parameter.xml");
            services.Handle(new ParameterConfirmationCommand(xmlDocument));
        }

        [When(@"Main Unit sends an xml with the name Confirmation\.xml containing the parameter to confirm")]
        public void WhenMainUnitSendsAnXmlWithTheNameConfirmation_XmlContainingTheParameterToConfirm()
        {
            //To be done on further integration testing
        }

        [When(@"Data Manager receives an UR01 message")]
        public void WhenDataManagerReceivesAnMessage()
        {
            ScenarioContext.Current.Get<InstrumentCommunicationServices>().Handle(new UrgentInformationRequestCommand(new UrgentInformationMessage()));
        }

        [When(@"Data Manager receives the urgent information from TSNAgent as an xml file")]
        public void WhenDataManagerReceivesTheUrgentInformationFromTSNAgentAsAnXmlFile()
        {
            var tsnAgentAdapter = ScenarioContext.Current.Get<Mock<ITSNAdapter>>();
            tsnAgentAdapter.Raise(x => x.Publish += null, new UrgentInformationReceived(new XmlDocument()));
        }
        
        [When(@"user creates an standard deviation rule with the values")]
        public void WhenUserCreatesAnStandardDeviationRuleWithTheValues(Table table)
        {
            CreateStandardDeviationRule(table);
        }

        public static void CreateStandardDeviationRule(Table table)
        {
            var ruleName = table.Rows[0]["Name"];
            var withingControlValue = Convert.ToBoolean(table.Rows[0]["WithinControl"]);
            var comment = table.Rows[0]["Comment"];
            var numberOfControls = Convert.ToInt32(table.Rows[0]["NumberOfControls"]);
            var standardDeviationLimits = Convert.ToInt32(table.Rows[0]["StandardDeviationLimits"]);

            var qcrulesRepository = ScenarioContext.Current.Get<IQCRuleRepository>() as MongoDBQCRulesRepository;
            qcrulesRepository.DeleteRuleByName(ruleName);

            CreateStandardDeviationRuleRequest request = new CreateStandardDeviationRuleRequest
            {
                RuleName = ruleName,
                WithinControlValue = withingControlValue,
                Comments = comment,
                NumberOfControls = numberOfControls,
                StandardDeviationLimits = standardDeviationLimits
            };

            var qcConfigutationServices = ScenarioContext.Current.Get<QCEvaluationServiceClient>();
            qcConfigutationServices.CreateStandardDeviationRule(request);
        }

        [When(@"user modifies the values")]
        public void WhenUserModifiesTheValues(Table table)
        {
            var ruleName = table.Rows[0]["Name"];
            var comment = table.Rows[0]["Comment"];
            var numberOfControls = Convert.ToInt32(table.Rows[0]["NumberOfControls"]);
            var standardDeviationLimits = Convert.ToInt32(table.Rows[0]["StandardDeviationLimits"]);

            var command = new ModifyStandardDeviationRule(ruleName, comment, numberOfControls, standardDeviationLimits);

            var qcConfigutationServices = ScenarioContext.Current.Get<IQCEvaluationServices>();
            qcConfigutationServices.Handle(command);
        }

        [When(@"user deactivates the rule ""(.*)""")]
        public void WhenUserDeactivatesTheRule(string ruleName)
        {
            var repository = ScenarioContext.Current.Get<IQCRuleRepository>();
            var ruleId = repository.GetRuleByName(ruleName).Id;

            var command = new DeactivateQCRule(ruleId);

            var qcConfigurationServices = ScenarioContext.Current.Get<IQCEvaluationServices>();
            qcConfigurationServices.Handle(command);
        }


        [When(@"user reactivates the rule ""(.*)""")]
        public void WhenUserReactivatesTheRule(string ruleName)
        {
            var service = ScenarioContext.Current.Get<IQCEvaluationServices>();
            Guid ruleId = ScenarioContext.Current.Get<IQCRuleRepository>().GetRuleByName(ruleName).Id;
            service.Handle(new ReactivateQCRule(ruleId));
        }

        [When(@"the ApplicationTest ""(.*)"" is assigned to the qc rule ""(.*)""")]
        public void WhenTheApplicationIsAssignedToTheQcRule(int applicationCode, string ruleName)
        {
            CommonFunctionalities.EnableRuleForApplication(applicationCode, ruleName);
        }

        [When(@"the ApplicationTest ""(.*)"" is unassigned to the qc rule ""(.*)""")]
        public void WhenTheApplicationIsUnassignedToTheQcRule(int applicationCode, string ruleName)
        {
            var application = ScenarioContext.Current.Get<IQCApplicationRepository>().GetApplicationByTestCode(applicationCode);
            var rule = ScenarioContext.Current.Get<IQCRuleRepository>().GetRuleByName(ruleName);
            var assignApplicationToRule = new DisableRuleForApplication(rule, application.Id);

            ScenarioContext.Current.Get<IQCEvaluationServices>().Handle(assignApplicationToRule);
        }

        [When(@"a qc result with for the test ""(.*)"" with value (.*) is received")]
        public void WhenAQcResultWithForTheTestWithValueIsReceived(int testCode, double result)
        {
            var instrumentCommunicationServices = ScenarioContext.Current.Get<InstrumentCommunicationServices>();
            ICommand command = new StoreQCResult(testCode,result,DateTime.Now.AddMinutes(-1));

            instrumentCommunicationServices.Handle(command);

        }
    }
}
