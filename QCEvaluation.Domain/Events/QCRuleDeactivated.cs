using System;

namespace QCEvaluation.Domain.Events
{
    public class QCRuleDeactivated : QCRuleEvent
    {
        public QCRuleDeactivated(Guid ruleId):base(ruleId)
        {
            
        }

    }
}