using System.ServiceModel;

namespace QCEvaluation.WCFService
{
    [ServiceContract]
    public interface IQCEvaluationService
    {
        [OperationContract]
        CreateStandardDeviationRuleResponse CreateStandardDeviationRule(CreateStandardDeviationRuleRequest request);

        [OperationContract]
        void NotifyApplicationInstalled(int testCode);
    }    
}