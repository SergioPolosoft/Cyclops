using QCEvaluation.Application;
using QCEvaluation.Application.Commands;
using QCEvaluation.Domain.Repositories;
using TechTalk.SpecFlow;

namespace InstrumentMessagesHandlerTests
{
    internal static class CommonFunctionalities
    {
        public static void EnableRuleForApplication(int applicationCode, string ruleName)
        {
            var application = ScenarioContext.Current.Get<IQCApplicationRepository>().GetApplicationByTestCode(applicationCode);
            var rule = ScenarioContext.Current.Get<IQCRuleRepository>().GetRuleByName(ruleName);
            var assignApplicationToRule = new EnableRuleForApplication(rule.Id, application.Id,applicationCode);

            ScenarioContext.Current.Get<IQCEvaluationServices>().Handle(assignApplicationToRule);
        }
    }
}