using System.ServiceModel;
using System.Web.Caching;

namespace QCEvaluation.WCFService
{
    [ServiceContract]
    public interface IQCEvaluationService
    {
        [OperationContract]
        CreateStandardDeviationRuleResponse CreateStandardDeviationRule(CreateStandardDeviationRuleRequest request);

        [OperationContract]
        GetStandardDeviationRuleResponse GetStandardDeviationRuleByName(string ruleName);
    }    
}