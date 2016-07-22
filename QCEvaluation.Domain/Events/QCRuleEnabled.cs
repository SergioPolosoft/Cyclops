using System;

namespace QCEvaluation.Domain.Events
{
    public class QCRuleEnabled : ApplicationEvent
    {
        public QCRuleEnabled(Guid applicationId, Guid ruleId)
        {
            this.applicationId = applicationId;
            this.RuleId = ruleId;
        }

        public Guid RuleId { get; set; }
    }
}