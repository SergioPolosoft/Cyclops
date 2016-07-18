using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace QCEvaluation.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IQCEvaluationService" in both code and config file together.
    [ServiceContract]
    public interface IQCEvaluationService
    {
        [OperationContract]
        CreateStandardDeviationRuleResponse CreateStandardDeviationRule(CreateStandardDeviationRuleRequest request);
       
    }

    [DataContract]
    public class CreateStandardDeviationRuleRequest
    {
    }

    [DataContract]
    public class CreateStandardDeviationRuleResponse 
    {
    }
}
