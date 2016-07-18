using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using QCEvaluation.Application;

namespace QCEvaluation.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "IqcEvaluationService" in both code and config file together.
    public class QCEvaluationService : IQCEvaluationService
    {
        
        public CreateStandardDeviationRuleResponse CreateStandardDeviationRule(CreateStandardDeviationRuleRequest request)
        {
            
            return new CreateStandardDeviationRuleResponse();
        }
    }
}
