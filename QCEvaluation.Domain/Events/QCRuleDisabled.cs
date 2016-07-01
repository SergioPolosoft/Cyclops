using System;
using ApplicationServices;

namespace QCEvaluation.Domain.Events
{
    public class QCRuleDisabled : ApplicationEvent
    {
        public QCRuleDisabled(Guid applicationId, Guid ruleId) 
        {
            this.ApplicationId = applicationId;
            this.RuleId = ruleId;
        }

        public Guid RuleId { get; private set; }
    }
}