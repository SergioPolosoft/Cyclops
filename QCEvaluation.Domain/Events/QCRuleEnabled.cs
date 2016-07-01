using System;

namespace QCEvaluation.Domain.Events
{
    public class QCRuleEnabled : ApplicationEvent
    {
        private readonly Guid ruleId;

        public QCRuleEnabled(Guid applicationId, Guid ruleId)
        {
            this.applicationId = applicationId;
            this.ruleId = ruleId;
        }

        public Guid RuleId
        {
            get { return ruleId; }
        }
    }
}