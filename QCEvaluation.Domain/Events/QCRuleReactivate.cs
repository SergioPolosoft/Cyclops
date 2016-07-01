using System;

namespace QCEvaluation.Domain.Events
{
    public class QCRuleReactivate:QCRuleEvent
    {
        public QCRuleReactivate(Guid ruleId) : base(ruleId)
        {
        }
    }
}