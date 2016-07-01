using System;

namespace QCEvaluation.Domain.Events
{
    public class QCRuleNumberOfControlsUpdated : QCRuleEvent
    {
        public QCRuleNumberOfControlsUpdated(int numberOfControls, Guid ruleId):base(ruleId)
        {
            this.NumberOfControls = numberOfControls;
        }

        public int NumberOfControls { get; private set; }
    }
}