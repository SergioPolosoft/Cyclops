﻿using System;
using Infrastructure.DTOs;
using Infrastructure.MessageQueue;
using Infrastructure.Repositories;
using InstrumentMessagesHandlerTests.Service_References.LabConfigurationServicesReference;
using LabConfiguration.Application;
using LabConfiguration.Application.Commands;
using LabConfiguration.Domain;
using QCConfiguration.Application;
using QCConfiguration.Application.Commands;
using QCConfiguration.Application.DTOs;
using QCEvaluation.Application;
using QCEvaluation.Application.Commands;
using QCEvaluation.Domain.Repositories;
using TechTalk.SpecFlow;

namespace InstrumentMessagesHandlerTests
{
    [Binding]
    class GivenSteps
    {
        [Given(@"a disconnected Data Manager")]
        public void GivenADisconnectedDataManager()
        {
            GivenAConnectedDataManager();
        }

        [Given(@"a connected Data Manager")]
        public void GivenAConnectedDataManager()
        {
        }

        [Given(@"a disconnected Main Unit")]
        public void GivenADisconnectedMainUnit()
        {

        }

        [Given(@"a connected Main Unit")]
        public void GivenAConnectedMainUnit()
        {
            MessageQueue.Instance.MessageReceived += Instance_MessageReceived;
        }

        private void Instance_MessageReceived(MessageDTO obj)
        {

        }

        [Given(@"a logged in user")]
        public void GivenALoggedInUser()
        {
            ScenarioContext.Current.Set(new User("DMDEV"));
        }

        [Given(@"a Standard Deviation rule existing in the system with the values")]
        public void GivenAStandardDeviationRuleExistingInTheSystemWithTheValues(Table table)
        {
            WhenSteps.CreateStandardDeviationRule(table);
        }

        [Given(@"the ""(.*)"" is deactivated")]
        public void GivenTheIsDeactivated(string ruleName)
        {
            var service = ScenarioContext.Current.Get<IQCEvaluationServices>();
            Guid ruleId = ScenarioContext.Current.Get<IQCRuleRepository>().GetRuleByName(ruleName).Id;
            service.Handle(new DeactivateQCRule(ruleId));
        }

        [Given(@"an existing ApplicationTest with test code ""(.*)""")]
        public void GivenAnExistingApplicationWithTestCode(int applicationTestCode)
        {
            var applicationRepository = ScenarioContext.Current.Get<IApplicationRepository>();
            applicationRepository.RemoveByApplicationCode(applicationTestCode);

            var applicationQCRepository = ScenarioContext.Current.Get<IQCApplicationRepository>() as MongoDBQCApplicationRepository;
            applicationQCRepository.DeleteApplication(applicationTestCode);

            var labConfigurationServices = ScenarioContext.Current.Get<LabConfigurationServiceClient>();
            var confirmApplicationInstallationRequest = new ConfirmApplicationInstallationRequest
            {
                ApplicationTestCode = applicationTestCode
            };
            labConfigurationServices.ConfirmApplicationInstallation(
                confirmApplicationInstallationRequest);            
        }

        [Given(@"the ApplicationTest ""(.*)"" is assigned to the qc rule ""(.*)""")]
        public void GivenTheApplicationIsAssignedToTheQcRule(int applicationCode, string ruleName)
        {
            CommonFunctionalities.EnableRuleForApplication(applicationCode, ruleName);
        }

        [Given(@"an existing ApplicationTest with the values")]
        public void GivenAnExistingApplicationWithTheValues(Table table)
        {
            /*"TestCode",
                        "1SD",
                        "2SD",
                        "3SD"*/
            int testCode = Convert.ToInt32(table.Rows[0]["TestCode"]);
            var sd1 = table.Rows[0]["1SD"];
            var sd2 = table.Rows[0]["2SD"];
            var sd3 = table.Rows[0]["3SD"];

            var labConfiguration= ScenarioContext.Current.Get<ILabConfigurationServices>();
            var application = new ApplicationDTO(testCode);
            application.SD1Value = sd1;
            application.SD2Value = sd2;
            application.SD3Value = sd3;

            labConfiguration.Handle(new ConfirmApplicationInstallationCommand(application));
        }

        [Given(@"an existing control for the test ""(.*)"" with the values")]
        public void GivenAnExistingControlForTheTestWithTheValues(int testCode, Table table)
        {
            var qcConfiguration = ScenarioContext.Current.Get<IQCConfigurationServices>();

            var control = new ControlDTO(testCode, Convert.ToDouble(table.Rows[0]["StandardDeviation"]),
                Convert.ToDouble(table.Rows[0]["TargetValue"]));

            qcConfiguration.Handle(new ConfirmControlInstallation(control));
        }

    }
}
