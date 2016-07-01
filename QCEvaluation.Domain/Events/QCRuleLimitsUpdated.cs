using System;

namespace QCEvaluation.Domain.Events
{
    public class QCRuleLimitsUpdated : QCRuleEvent
    {
        public QCRuleLimitsUpdated(int standardDeviationLimit, Guid qcRuleId) : base(qcRuleId)
        {
            this.StandardDeviationLimit = standardDeviationLimit;
        }

        public int StandardDeviationLimit { get; private set; }
    }
}